using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Functions.Combat;
using System.Linq;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    public static class EndCombatSessionFunction
    {
        [FunctionName("EndCombatSession")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "combat/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CombatSession session,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            string id,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(EndCombatSession));
            DebugLogger.Log($"Ending combat session {id}");

            if (session == null)
            {
                log.LogWarning("Combat session {Id} not found", id);
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });
            }

            // Soft delete: flag or rename it
            session.Name += " (ENDED)";
            foreach (var combatant in session.Combatants)
            {
                if (combatant.Conditions == null)
                {
                    combatant.Conditions = new System.Collections.Generic.List<string>();
                }
                combatant.Conditions.Add("Archived");
            }

            await sessionOut.AddAsync(session);

            log.LogInformation("Combat session {Id} ended", session.Id);
            return new OkObjectResult(new
            {
                success = true,
                message = "Combat session marked as ended.",
                sessionId = session.Id
            });
        }
    }
}
