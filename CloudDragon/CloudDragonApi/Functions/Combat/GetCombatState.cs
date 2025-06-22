using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Retrieves the full combat state for a given session.
    /// </summary>
    public static class GetCombatStateFunction
    {
        /// <summary>
        /// HTTP GET endpoint to fetch combat session details.
        /// </summary>
        /// <param name="req">The incoming HTTP request.</param>
        /// <param name="session">Session loaded from Cosmos DB.</param>
        /// <param name="id">Session identifier.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Action result with the combat state.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("GetCombatState")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "combat/{id}")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
            CollectionName = "CombatSessions",
            ConnectionStringSetting = "CosmosDBConnection",
            Id = "{id}",
            PartitionKey = "{id}")] CombatSession session,
        string id,
        ILogger log)
    {
        log.LogRequestDetails(req, nameof(GetCombatState));
        DebugLogger.Log($"Retrieving combat state for session {id}");

        if (session == null)
        {
            log.LogWarning($"Combat session not found: {id}");
            return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });
        }

        var current = session.Combatants != null && session.TurnIndex < session.Combatants.Count
            ? session.Combatants[session.TurnIndex]
            : null;

        return new OkObjectResult(new
        {
            success = true,
            data = session,
            currentTurn = current?.Name ?? "No active combatant",
            round = session.Round
        });
    }
}
}
