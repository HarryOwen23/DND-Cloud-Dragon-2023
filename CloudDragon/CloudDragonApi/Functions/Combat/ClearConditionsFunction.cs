using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi.Services;
using CloudDragonApi.Models;
using CloudDragonApi;
using CloudDragonApi.Utils;

namespace CloudDragonApi.CombatConditions
{
    public static class ClearConditionsFunction
    {
        [FunctionName("ClearConditions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/combatant/{combatantId}/clear-conditions")] HttpRequest req,
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
            log.LogRequestDetails(req, nameof(ClearConditions));
            DebugLogger.Log($"Clearing conditions for {combatantId} in session {sessionId}");

            if (session == null)
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

            var combatant = session.Combatants.FirstOrDefault(c => c.Id == combatantId);
            if (combatant == null)
                return new NotFoundObjectResult(new { success = false, error = "Combatant not found." });

            CombatConditionsService.ClearAllConditions(combatant);

            await sessionOut.AddAsync(session);

            return new OkObjectResult(new { success = true, message = $"{combatant.Name}'s conditions cleared." });
        }
    }
}
