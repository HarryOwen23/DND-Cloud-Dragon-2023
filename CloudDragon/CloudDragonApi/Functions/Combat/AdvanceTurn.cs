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

    [FunctionName("CreateCombatSession")]
    public static async Task<IActionResult> CreateCombatSession(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat")] HttpRequest req,
        [CosmosDB(
            databaseName: "CloudDragonDB",
            containerName: "CombatSessions",
            Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
        ILogger log)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        CombatSession session;

        try
        {
            session = JsonConvert.DeserializeObject<CombatSession>(body);
            if (session == null || string.IsNullOrWhiteSpace(session.Name))
                return new BadRequestObjectResult(new { success = false, error = "Invalid session data." });

            session.Combatants.ForEach(c =>
            {
                c.Initiative = Random.Shared.Next(1, 21) + c.InitiativeModifier;
            });

            session.Combatants = session.Combatants
                .OrderByDescending(c => c.Initiative)
                .ToList();

            await sessionOut.AddAsync(session);
            return new OkObjectResult(new { success = true, id = session.Id });
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error creating combat session.");
            return new BadRequestObjectResult(new { success = false, error = ex.Message });
        }
    }

    [FunctionName("EndCombatSession")]
    public static async Task<IActionResult> EndCombatSession(
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
        if (session == null)
            return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

        session.Name += " (ENDED)";
        session.Combatants.ForEach(c => c.Conditions.Add("Archived"));

        await sessionOut.AddAsync(session);

        return new OkObjectResult(new
        {
            success = true,
            message = "Combat session marked as ended.",
            sessionId = session.Id
        });
    }
}
