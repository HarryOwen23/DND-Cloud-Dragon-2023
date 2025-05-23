using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonApi.Models;

namespace CloudDragonApi.Models
{
    public class CombatSession
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public List<Combatant> Combatants { get; set; } = new();

        public int Round { get; set; } = 1;
        public int TurnIndex { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<string> Log { get; set; } = new();
    }
    public static class CreateCombatSessionFunction
    {
        [FunctionName("CreateCombatSession")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            CombatSession session;

            try
            {
                session = JsonConvert.DeserializeObject<CombatSession>(body);
                if (session == null || string.IsNullOrWhiteSpace(session.Name))
                {
                    return new BadRequestObjectResult(new { success = false, error = "Invalid session data." });
                }

                var rng = new Random();
                foreach (var c in session.Combatants)
                {
                    c.Initiative = rng.Next(1, 21) + c.InitiativeModifier;
                }

                session.Combatants = session.Combatants
                    .OrderByDescending(c => c.Initiative)
                    .ToList();

                await sessionOut.AddAsync(session);

                return new OkObjectResult(new { success = true, id = session.Id });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error creating combat session.");
                return new BadRequestObjectResult(new { success = false, error = ex.Message });
            }
        }
    }
}
