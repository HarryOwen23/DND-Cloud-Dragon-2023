using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi.Models;
using System.Linq;

namespace CloudDragonApi.Functions.Combat
{
    public static class EndCombatSessionFunction
    {
        [FunctionName("EndCombatSession")]
        public static async Task<IActionResult> Run(
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

            // Soft delete: flag or rename it
            session.Name += " (ENDED)";
            foreach (var combatant in session.Combatants)
            {
                if (combatant.Conditions == null)
                {
                    combatant.Conditions = new System.Collections.Generic.List<string>();
                }
                combatant.Conditions.Add("Archived");
            }

            await sessionOut.AddAsync(session);

            return new OkObjectResult(new
            {
                success = true,
                message = "Combat session marked as ended.",
                sessionId = session.Id
            });
        }
    }
}