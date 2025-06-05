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
using CloudDragonApi;
using Newtonsoft.Json;
using CloudDragonApi.Models;

namespace CloudDragonApi.Functions.Combat
{
    public static class CreateCombatSessionFunction
    {
        [FunctionName("CreateCombatSession")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "CombatSessions",
                Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(CreateCombatSession));

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogDebug("Request Body: {Body}", body);
            CombatSession session;

            try
            {
                session = JsonConvert.DeserializeObject<CombatSession>(body);
                if (session == null || string.IsNullOrWhiteSpace(session.Name))
                    return new BadRequestObjectResult(new { success = false, error = "Invalid session data." });

                // Roll initiative for each combatant
                var rng = new Random();
                session.Combatants ??= new List<Combatant>();
                foreach (var c in session.Combatants)
                {
                    c.Initiative = rng.Next(1, 21) + c.InitiativeModifier;
                    c.Conditions ??= new List<string>();
                }

                // Sort combatants
                session.Combatants = session.Combatants
                    .OrderByDescending(c => c.Initiative)
                    .ToList();

                await sessionOut.AddAsync(session);
                log.LogInformation("Combat session {Id} created with {Count} combatants", session.Id, session.Combatants.Count);
                return new OkObjectResult(new { success = true, id = session.Id });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error creating combat session.");
                return new BadRequestObjectResult(new { success = false, error = ex.Message });
            }
        }
    }
}
