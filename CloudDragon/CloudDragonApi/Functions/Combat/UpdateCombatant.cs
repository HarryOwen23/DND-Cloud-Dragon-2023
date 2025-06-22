using System;
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
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Updates an existing combatant within a combat session document.
    /// </summary>
    public static class UpdateCombatantFunction
    {
        /// <summary>
        /// Handles HTTP PATCH requests to update a combatant's data.
        /// </summary>
        /// <param name="req">Incoming HTTP request containing updated values.</param>
        /// <param name="session">Existing combat session loaded from Cosmos DB.</param>
        /// <param name="sessionOut">Output binding used to persist the session.</param>
        /// <param name="sessionId">Identifier for the combat session.</param>
        /// <param name="name">Combatant name to update.</param>
        /// <param name="log">Function logger instance.</param>
        /// <returns>Result detailing success or failure of the update.</returns>
        [FunctionName("UpdateCombatant")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "combat/{sessionId}/combatant/{name}")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{sessionId}",
                PartitionKey = "{sessionId}")] CombatSession session,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            string sessionId,
            string name,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(UpdateCombatant));
            DebugLogger.Log($"Updating combatant {name} in session {sessionId}");

            if (session == null)
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

            var combatant = session.Combatants.FirstOrDefault(c =>
                string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

            if (combatant == null)
                return new NotFoundObjectResult(new { success = false, error = $"Combatant '{name}' not found in session." });

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var update = JsonConvert.DeserializeObject<Combatant>(body);

            if (update == null)
                return new BadRequestObjectResult(new { success = false, error = "Invalid update payload." });

            // Only update fields if explicitly provided
            if (update.HP != default) combatant.HP = update.HP;
            if (update.AC != default) combatant.AC = update.AC;
            if (update.Conditions != null && update.Conditions.Any())
                combatant.Conditions = update.Conditions;

            await sessionOut.AddAsync(session);

            return new OkObjectResult(new
            {
                success = true,
                updated = combatant
            });
        }
    }
}
