using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using CloudDragon.CloudDragonApi.Functions.Combat;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CharacterModel = CloudDragon.Models.Combatant;

namespace CloudDragon.CloudDragonApi.Functions.Combat
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
            log.LogRequestDetails(req, nameof(UseCombatAction));
            DebugLogger.Log($"UseCombatAction called by {combatantId} in session {sessionId}");
            log.LogInformation($"Combatant {combatantId} is taking an action in session {sessionId}.");

            if (session == null)
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<dynamic>(requestBody);

            string actionType = input?.action;
            string targetId = input?.targetId;
            string ability = input?.ability ?? "Strength";
            string coverType = input?.coverType;

            if (string.IsNullOrEmpty(actionType))
                return new BadRequestObjectResult(new { success = false, error = "Missing action type." });

            var attacker = session.Combatants.FirstOrDefault(c => c.Id == combatantId);
            if (attacker == null)
                return new NotFoundObjectResult(new { success = false, error = "Attacker not found in session." });

            switch ((string)actionType)
            {
                case "attack":
                    {
                        if (string.IsNullOrEmpty(targetId))
                            return new BadRequestObjectResult(new { success = false, error = "Missing target for attack." });

                        var defender = session.Combatants.FirstOrDefault(c => c.Id == (string)targetId);
                        if (defender == null)
                            return new NotFoundObjectResult(new { success = false, error = "Target not found in session." });

                        int attackModifier = GetAbilityModifier(attacker, ability);

                        var (hit, roll, total) = CombatActionService.ResolveAttackRoll(attacker, defender, attackModifier);

                        int damage = 0;
                        if (hit)
                        {
                            var weapon = attacker.Equipped?.Values.FirstOrDefault(e => e.Type == "Weapon");
                            string damageDice = weapon?.Damage ?? "1d4";
                            damage = CombatRollService.RollDamage(damageDice);
                            CombatActionService.ApplyDamage(defender, damage);
                        }

                        await sessionOut.AddAsync(session);

                        return new OkObjectResult(new
                        {
                            success = true,
                            action = "attack",
                            attacker = attacker.Name,
                            defender = defender.Name,
                            roll,
                            total,
                            attackModifier,
                            targetAC = defender.AC,
                            hit,
                            damage
                        });
                    }

                case "dodge":
                    {
                        CombatActionService.HandleDodge(attacker);
                        await sessionOut.AddAsync(session);
                        return new OkObjectResult(new { success = true, message = $"{attacker.Name} is dodging." });
                    }

                case "take-cover":
                    {
                        if (string.IsNullOrEmpty(coverType))
                            return new BadRequestObjectResult(new { success = false, error = "Cover type missing." });

                        CombatActionService.ApplyCoverBonus(attacker, coverType);
                        await sessionOut.AddAsync(session);
                        return new OkObjectResult(new { success = true, message = $"{attacker.Name} takes {coverType} cover." });
                    }

                case "custom":
                    {
                        string description = input?.description ?? "Performs a custom action.";
                        return new OkObjectResult(new { success = true, message = $"{attacker.Name} does: {description}" });
                    }

                default:
                    return new BadRequestObjectResult(new { success = false, error = $"Unknown action: {actionType}" });
            }
        }

        private static int GetAbilityModifier(CharacterModel character, string ability)
        {
            character.Stats ??= new Dictionary<string, int>();

            if (character.Stats.TryGetValue(ability, out var statScore))
                return (int)Math.Floor((statScore - 10) / 2.0);

            return 0;
        }
    }
}
