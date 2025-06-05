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

public static class CombatFunctions
{
    [FunctionName("AdvanceTurn")]
    public static async Task<IActionResult> AdvanceTurn(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{id}/advance")] HttpRequest req,
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
        if (session == null)
            return new NotFoundObjectResult(new { success = false, error = "Session not found." });

        if (session.Combatants == null || session.Combatants.Count == 0)
            return new BadRequestObjectResult(new { success = false, error = "No combatants available." });

        if (session.TurnIndex < 0 || session.TurnIndex >= session.Combatants.Count)
            session.TurnIndex = 0;

        var currentCombatant = session.Combatants[session.TurnIndex];
        session.TurnIndex++;
        if (session.TurnIndex >= session.Combatants.Count)
        {
            session.TurnIndex = 0;
            session.Round++;
        }

        session.Log.Add($"Turn {session.TurnIndex + 1}: {currentCombatant.Name}'s turn began.");

        await sessionOut.AddAsync(session);
        return new OkObjectResult(new
        {
            success = true,
            nextTurn = session.Combatants[session.TurnIndex].Name,
            round = session.Round
        });
    }

    // The CreateCombatSession and EndCombatSession functions were moved to
    // dedicated files.  Keeping only AdvanceTurn here avoids duplicate
    // FunctionName attributes which caused runtime indexing errors.
}
