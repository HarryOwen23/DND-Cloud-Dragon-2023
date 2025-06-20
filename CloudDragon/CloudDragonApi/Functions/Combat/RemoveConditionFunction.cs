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
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using CloudDragon.CloudDragonApi.Functions.Combat;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Removes a specified condition from a combatant.
    /// </summary>
    public static class RemoveConditionFunction
    {
        /// <summary>
        /// Removes the condition if it exists on the specified combatant.
        /// </summary>
        [FunctionName("RemoveCondition")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{sessionId}/combatant/{combatantId}/remove-condition")] HttpRequest req,
            string sessionId,
            string combatantId,
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
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(RemoveCondition));
            DebugLogger.Log($"Removing condition from {combatantId} in session {sessionId}");

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

            CombatConditionsService.RemoveCondition(combatant, condition);

            await sessionOut.AddAsync(session);

            return new OkObjectResult(new { success = true, message = $"{condition} removed from {combatant.Name}" });
        }
    }
}
