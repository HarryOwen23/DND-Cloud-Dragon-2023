using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Retrieves the initiative order for a combat session.
    /// </summary>
    public static class GetInitiativeOrder
    {
        /// <summary>
        /// HTTP GET endpoint returning combatants sorted by initiative.
        /// </summary>
        /// <param name="req">Incoming HTTP request.</param>
        /// <param name="sessionId">Combat session identifier.</param>
        /// <param name="session">Session document from Cosmos DB.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result with the initiative order list.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("GetInitiativeOrder")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "combat/{sessionId}/initiative")] HttpRequest req,
            string sessionId,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{sessionId}",
                PartitionKey = "{sessionId}")] CombatSession session,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(GetInitiativeOrder));
            DebugLogger.Log($"Retrieving initiative order for session {sessionId}");

            if (session == null)
            {
                log.LogWarning($"Combat session not found: {sessionId}");
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });
            }

            if (session.Combatants == null || session.Combatants.Count == 0)
            {
                return new OkObjectResult(new
                {
                    success = true,
                    initiativeOrder = new object[0],
                    message = "No combatants in session."
                });
            }

            var sorted = session.Combatants
                .OrderByDescending(c => c.Initiative)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Initiative
                });

            return new OkObjectResult(new
            {
                success = true,
                initiativeOrder = sorted
            });
        }
    }
}
