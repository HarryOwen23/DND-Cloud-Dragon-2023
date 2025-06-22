using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Input payload for the point buy character stat generator.
    /// </summary>
    public class PointBuyInput
    {
        /// <summary>Requested ability scores.</summary>
        public Dictionary<string, int> Stats { get; set; }
    }

    /// <summary>
    /// Azure Function that calculates ability scores using the point buy system.
    /// </summary>
    public static class PointBuyFunction
    {
        /// <summary>
        /// Executes the point buy calculation for the provided stats.
        /// </summary>
        /// <param name="req">Incoming HTTP request.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result with generated ability scores or an error.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("PointBuy")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "point-buy")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("PointBuyFunction triggered");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                log.LogWarning("Request body is empty.");
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = "Request body is empty."
                });
            }

            PointBuyInput input;
            try
            {
                input = JsonConvert.DeserializeObject<PointBuyInput>(requestBody);
            }
            catch (JsonException ex)
            {
                log.LogError(ex, "Failed to deserialize PointBuy input.");
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = "Invalid JSON format."
                });
            }

            if (input?.Stats == null || input.Stats.Count == 0)
            {
                log.LogWarning("No stats provided in input.");
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = "No stats provided."
                });
            }

            try
            {
                var builder = new CharacterStatsPointBuy();
                var stats = builder.GenerateStats(input.Stats); // Ensure this method exists and is public

                log.LogInformation("Stats successfully generated via point buy.");
                return new OkObjectResult(new
                {
                    success = true,
                    data = stats
                });
            }
            catch (ArgumentException ex)
            {
                log.LogWarning(ex, "Validation failed in point buy.");
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}
