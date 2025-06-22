using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Functions for determining combat initiative order.
    /// </summary>
    public static class RollInitiative
    {
        /// <summary>
        /// Rolls initiative for every combatant in the session.
        /// </summary>
        [Microsoft.Azure.WebJobs.FunctionName("RollInitiative")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/roll-initiative")] HttpRequest req,
            string sessionId,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{sessionId}",
                PartitionKey = "{sessionId}")] CombatSession session,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(RollInitiative));
            DebugLogger.Log($"Rolling initiative for session {sessionId}");

            if (session == null)
            {
                log.LogWarning("Combat session not found: {SessionId}", sessionId);
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });
            }

            if (session.Combatants == null || session.Combatants.Count == 0)
            {
                log.LogInformation("No combatants in session: {SessionId}", sessionId);
                return new OkObjectResult(new { success = false, message = "No combatants available to roll initiative." });
            }

            foreach (var combatant in session.Combatants)
            {
                int dexMod = combatant.Stats?.ContainsKey("Dexterity") == true
                    ? (int)Math.Floor((combatant.Stats["Dexterity"] - 10) / 2.0)
                    : 0;

                combatant.Initiative = Random.Shared.Next(1, 21) + dexMod;
            }

            session.Combatants = session.Combatants
                .OrderByDescending(c => c.Initiative)
                .ToList();

            await sessionOut.AddAsync(session);

            log.LogInformation("Initiative rolled for session {SessionId}", sessionId);

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
