using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi.Models;
using System.Linq;
using CloudDragonApi;
using CloudDragonApi.Utils;

public static class GetCombatStateFunction
{
    [FunctionName("GetCombatState")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "combat/{id}")] HttpRequest req,
        [CosmosDB(
            databaseName: "CloudDragonDB",
            containerName: "CombatSessions",
            Connection = "CosmosDBConnection",
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
// This function retrieves the current state of a combat session in a D&D game.
