using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using Newtonsoft.Json;

namespace CloudDragonApi.Combat
{
    public static class RollInitiative
    {
        [FunctionName("RollInitiative")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/roll-initiative")] HttpRequest req,
            string sessionId,
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
            if (session == null)
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

            var rng = new Random();

            foreach (var combatant in session.Combatants)
            {
                int dexMod = combatant.Stats?.ContainsKey("Dexterity") == true
                    ? (int)Math.Floor((combatant.Stats["Dexterity"] - 10) / 2.0)
                    : 0;

                combatant.Initiative = rng.Next(1, 21) + dexMod;
            }

            session.Combatants = session.Combatants
                .OrderByDescending(c => c.Initiative)
                .ToList();

            await sessionOut.AddAsync(session);

            return new OkObjectResult(new
            {
                success = true,
                message = "Initiative rolled.",
                order = session.Combatants.Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Initiative
                })
            });
        }
    }
}
