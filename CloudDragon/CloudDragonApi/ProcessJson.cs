using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Functions.Worker.Http;
using CloudDragonLib;
using System;
using System.Collections.Generic;

namespace CloudDragonApi
{
    public static class ProcessJson
    {
        [FunctionName("ProcessJson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "process-json")] HttpRequestData req,
            ILogger log)
        {
            log.LogInformation("ProcessJson triggered");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                log.LogWarning("Request body is empty.");
                return new BadRequestObjectResult(new { success = false, error = "Request body is empty." });
            }

            JObject payload;
            try
            {
                payload = JsonConvert.DeserializeObject<JObject>(requestBody);
            }
            catch (JsonException ex)
            {
                log.LogError(ex, "Invalid JSON format.");
                return new BadRequestObjectResult(new { success = false, error = "Invalid JSON format." });
            }

            string type = payload?["type"]?.ToString()?.ToLowerInvariant();
            if (string.IsNullOrEmpty(type))
            {
                log.LogWarning("Missing 'type' in JSON payload.");
                return new BadRequestObjectResult(new { success = false, error = "Missing 'type' in JSON payload." });
            }

            try
            {
                switch (type)
                {
                    case "point-buy":
                        var stats = payload["stats"]?.ToObject<Dictionary<string, int>>();
                        if (stats == null) throw new ArgumentException("Missing or invalid 'stats' for point-buy.");

                        var builder = new Character_Stats_Point_Buy();
                        var resultStats = builder.GenerateStats(stats);
                        return new OkObjectResult(new { success = true, data = resultStats });

                    case "roll-stats":
                        var roller = new Character_Stats_Dice();
                        var statBlock = roller.RollStats();
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
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}
