using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions
{
    /// <summary>
    /// Azure Function to retrieve a race record by its name.
    /// </summary>
    public static class GetRaceByName
    {
        /// <summary>
        /// Handles GET requests for race lookup by name.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="name">Race name.</param>
        /// <param name="context">Function execution context.</param>
        /// <returns>HTTP response containing the race info or a 404 error.</returns>
        [Function("GetRaceByName")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(Microsoft.Azure.Functions.Worker.AuthorizationLevel.Function, "get", Route = "races/{name}")]
            HttpRequestData req,
            string name,
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(GetRaceByName));
            DebugLogger.Log($"GetRaceByName called with {name}");

            // Load race data
            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json");
            DebugLogger.Log($"Loaded {races.Count} races searching for {name}");

            // Search for the matching race
            var match = races.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            HttpResponseData response;
            if (match == null)
            {
                log.LogWarning("Race not found: {Name}", name);
                DebugLogger.Log($"Race {name} not found");

                response = req.CreateResponse(HttpStatusCode.NotFound);
                await response.WriteAsJsonAsync(new { success = false, error = "Race not found." });
                return response;
            }

            log.LogInformation("Returning race {Race}", match.Name);
            DebugLogger.Log($"Returning race {match.Name}");

            response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new { success = true, race = match });
            return response;
        }
    }
}
