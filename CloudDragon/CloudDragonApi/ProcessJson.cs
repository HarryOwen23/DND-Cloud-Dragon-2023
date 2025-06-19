using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using CloudDragonLib;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi
{
    /// <summary>
    /// Azure Function that processes JSON payloads for various character stat generation
    /// operations such as point buy and rolling for stats.
    /// </summary>
    public static class ProcessJsonFunction
    {
        /// <summary>
        /// Entry point for the <c>ProcessJson</c> function.
        /// </summary>
        /// <param name="req">HTTP request containing the JSON payload.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Action result describing the outcome.</returns>
        [FunctionName("ProcessJson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "process-json")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ProcessJson triggered");
            DebugLogger.Log("ProcessJson received a request");

            if (!ApiRequestHelper.IsAuthorized(req, log))
            {
                return new UnauthorizedResult();
            }

            var payload = await ApiRequestHelper.ReadJsonAsync<JObject>(req, log);
            if (payload == null)
                return new BadRequestObjectResult(new { success = false, error = "Invalid JSON format." });

            DebugLogger.Log("Payload parsed successfully");

            string type = payload?["type"]?.ToString()?.ToLowerInvariant();
            if (string.IsNullOrEmpty(type))
            {
                log.LogWarning("Missing 'type' in JSON payload.");
                DebugLogger.Log("Missing 'type' in request payload");
                return new BadRequestObjectResult(new { success = false, error = "Missing 'type' in JSON payload." });
            }

            try
            {
                switch (type)
                {
                    case "point-buy":
                        var stats = payload["stats"]?.ToObject<Dictionary<string, int>>();
                        if (stats == null) throw new ArgumentException("Missing or invalid 'stats' for point-buy.");

                        var builder = new CharacterStatsPointBuy();
                        var resultStats = builder.GenerateStats(stats);
                        DebugLogger.Log("Point-buy stats generated");
                        return new OkObjectResult(new { success = true, data = resultStats });

                    case "roll-stats":
                        var diceStats = CharacterStatsDice.Generate();
                        var statBlock = new[]
                        {
                            diceStats.Strength,
                            diceStats.Dexterity,
                            diceStats.Constitution,
                            diceStats.Intelligence,
                            diceStats.Wisdom,
                            diceStats.Charisma
                        };
                        DebugLogger.Log("Stats rolled successfully");
                        return new OkObjectResult(new { success = true, data = statBlock });

                    default:
                        return new BadRequestObjectResult(new
                        {
                            success = false,
                            error = $"Unknown type '{type}'. Expected one of: point-buy, roll-stats."
                        });
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error during processing.");
                DebugLogger.Log($"ProcessJson failed: {ex.Message}");
                return new BadRequestObjectResult(new { success = false, error = ex.Message });
            }
        }
    }
}
