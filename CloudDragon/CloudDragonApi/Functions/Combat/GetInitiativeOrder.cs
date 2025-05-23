using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi.Models;

namespace CloudDragonApi.Combat
{
    public static class GetInitiativeOrder
    {
        [FunctionName("GetInitiativeOrder")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "combat/{sessionId}/initiative")] HttpRequest req,
            string sessionId,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection",
                Id = "{sessionId}",
                PartitionKey = "{sessionId}")] CombatSession session,
            ILogger log)
        {
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
