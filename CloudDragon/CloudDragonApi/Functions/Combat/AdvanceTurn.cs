using System;
using System.Collections.Generic;
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
using CloudDragon.CloudDragonApi.Functions.Combat;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Azure Functions handling combat session actions.
    /// This file contains the function for advancing the turn order.
    /// </summary>
    public static class CombatFunctions
    {
        /// <summary>
        /// Advances the current combat session to the next turn.
        /// </summary>
        /// <param name="req">HTTP request triggering the function.</param>
        /// <param name="session">Current session loaded from Cosmos DB.</param>
        /// <param name="sessionOut">Output binding to persist updates.</param>
        /// <param name="id">Session identifier.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Details about the next turn and round.</returns>
        [FunctionName("AdvanceTurn")]
        public static async Task<IActionResult> AdvanceTurn(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{id}/advance")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection",
            Id = "{id}",
            PartitionKey = "{id}")] CombatSession session,
        [CosmosDB(
            DatabaseName = "CloudDragonDB",
            ContainerName = "CombatSessions",
            ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            string id,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(AdvanceTurn));
            DebugLogger.Log($"Advancing turn for session {id}");

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
        DebugLogger.Log($"Turn advanced to {session.TurnIndex + 1} (Round {session.Round})");

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
}
