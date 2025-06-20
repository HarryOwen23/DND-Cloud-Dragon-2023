using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using CloudDragon.CloudDragonApi.Functions.Combat;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Applies a combat condition to a specific combatant.
    /// </summary>
    public static class ApplyConditionFunction
    {
        /// <summary>
        /// Adds the given condition to the combatant if present.
        /// </summary>
        [FunctionName("ApplyCondition")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/combatant/{combatantId}/apply-condition")] HttpRequest req,
            string sessionId,
            string combatantId,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{sessionId}",
                PartitionKey = "{sessionId}")] CombatSession session,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "CombatSessions",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(ApplyCondition));
            DebugLogger.Log($"Applying condition to {combatantId} in session {sessionId}");

            if (session == null)
                return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

            var combatant = session.Combatants.FirstOrDefault(c => c.Id == combatantId);
            if (combatant == null)
                return new NotFoundObjectResult(new { success = false, error = "Combatant not found." });

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string condition = input?.condition;

            if (string.IsNullOrWhiteSpace(condition))
                return new BadRequestObjectResult(new { success = false, error = "Missing condition name." });

            CombatConditionsService.ApplyCondition(combatant, condition);

            await sessionOut.AddAsync(session);

            return new OkObjectResult(new { success = true, message = $"{combatant.Name} now has condition: {condition}" });
        }
    }
}
