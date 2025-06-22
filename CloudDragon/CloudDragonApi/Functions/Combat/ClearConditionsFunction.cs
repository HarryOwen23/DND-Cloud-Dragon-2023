using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Removes all combat conditions from a combatant within a session.
    /// </summary>
    public static class ClearConditionsFunction
    {
        /// <summary>
        /// HTTP trigger that clears every condition from a combatant.
        /// </summary>
        /// <param name="req">Incoming HTTP request.</param>
        /// <param name="sessionId">Identifier of the combat session.</param>
        /// <param name="combatantId">Combatant id to clear conditions for.</param>
        /// <param name="session">Loaded combat session document.</param>
        /// <param name="sessionOut">Output binding for persisting updates.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result object describing success or failure.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("ClearConditions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/combatant/{combatantId}/clear-conditions")] HttpRequest req,
            string sessionId,
            string combatantId,
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
