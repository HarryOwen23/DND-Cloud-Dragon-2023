using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

namespace CloudDragonApi.Combat
{
    public static class UseCombatAction
    {
        [FunctionName("UseCombatAction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/combatant/{combatantId}/action")] HttpRequest req,
            string sessionId,
            string combatantId,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection",
                Id = "{sessionId}",
                PartitionKey = "{sessionId}")] CombatSession session,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            ILogger log)
        {
            log.LogInformation($"Combatant {combatantId} is taking an action in session {sessionId}.");

            if (session == null)
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<dynamic>(requestBody);

            string actionType = input?.action;
            string targetId = input?.targetId;
            string ability = input?.ability ?? "Strength";

            if (string.IsNullOrEmpty(actionType))
                return new BadRequestObjectResult(new { success = false, error = "Missing action type." });

            var attacker = session.Combatants.FirstOrDefault(c => c.Id == combatantId);
            if (attacker == null)
                return new NotFoundObjectResult(new { success = false, error = "Attacker not found in session." });

            switch ((string)actionType)
            {
                case "attack":
                    var target = session.Combatants.FirstOrDefault(c => c.Id == (string)targetId);
                    if (target == null)
                        return new NotFoundObjectResult(new { success = false, error = "Target not found in session." });

                    // Get ability mod
                    int abilityMod = 0;
                    if (attacker.Stats?.TryGetValue((string)ability, out var statScore) == true)
                        abilityMod = (int)Math.Floor((statScore - 10) / 2.0);

                    // Get equipped weapon
                    var weapon = attacker.Equipped?.Values.FirstOrDefault(e => e.Type == "Weapon");
                    if (weapon == null)
                        return new BadRequestObjectResult(new { success = false, error = "No weapon equipped." });

                    // Roll to hit
                    var rng = new Random();
                    int roll = rng.Next(1, 21);
                    int totalToHit = roll + abilityMod;

                    bool hit = totalToHit >= target.AC;

                    // Damage (if hit)
                    string damageRoll = weapon.Damage ?? "1d4";
                    int damage = hit ? RollDamage(damageRoll, rng) : 0;

                    // Optional: Apply damage to target HP (if tracked)

                    await sessionOut.AddAsync(session);

                    return new OkObjectResult(new
                    {
                        success = true,
                        action = "attack",
                        attacker = attacker.Name,
                        target = target.Name,
                        roll,
                        modifier = abilityMod,
                        totalToHit,
                        targetAC = target.AC,
                        hit,
                        damage,
                        weapon = weapon.Name
                    });

                case "dodge":
                    attacker.Conditions.Add("Dodging");
                    await sessionOut.AddAsync(session);
                    return new OkObjectResult(new { success = true, message = $"{attacker.Name} is dodging." });

                case "cast":
                    return new OkObjectResult(new { success = true, message = $"{attacker.Name} casts a spell." });

                case "custom":
                    string description = input?.description ?? "Performs an action.";
                    return new OkObjectResult(new { success = true, message = $"{attacker.Name} does a custom action: {description}" });

                default:
                    return new BadRequestObjectResult(new { success = false, error = $"Unknown action: {actionType}" });
            }
        }

        private static int RollDamage(string diceNotation, Random rng)
        {
            // Basic "NdX" parser, e.g. 2d6 or 1d8
            var parts = diceNotation.ToLower().Split('d');
            if (parts.Length != 2 || !int.TryParse(parts[0], out var numDice) || !int.TryParse(parts[1], out var dieSize))
                return rng.Next(1, 5); // fallback to 1d4

            int total = 0;
            for (int i = 0; i < numDice; i++)
                total += rng.Next(1, dieSize + 1);

            return total;
        }
    }
}
