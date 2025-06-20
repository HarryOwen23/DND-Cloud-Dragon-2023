using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions
{
    /// <summary>
    /// Retrieves a race record by its name.
    /// </summary>
    public static class GetRaceByName
    {
        /// <summary>
        /// Looks up a race by name and returns it if found.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="name">Race name to search for.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>The matching race or 404.</returns>
        [FunctionName("GetRaceByName")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "races/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(GetRaceByName));
            DebugLogger.Log($"GetRaceByName called with {name}");

            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json");
            DebugLogger.Log($"Loaded {races.Count} races searching for {name}");
            var match = races.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                log.LogWarning("Race not found: {Name}", name);
                DebugLogger.Log($"Race {name} not found");
                return new NotFoundObjectResult(new { success = false, error = "Race not found." });
            }
            log.LogInformation("Returning race {Race}", match.Name);
            DebugLogger.Log($"Returning race {match.Name}");
            return new OkObjectResult(new { success = true, race = match });
        }
    }
}
