using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib;
using System;
using System.Threading.Tasks;
using CloudDragonApi.Utils;

namespace CloudDragonApi
{
    public static class RollForStatsFunction
    {
        [FunctionName("RollStats")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "roll-stats")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("RollStats endpoint triggered.");

            try
            {
                var roller = new Character_Stats_Dice();
                var statBlock = roller.RollStats();

                DebugLogger.Log($"Generated stat block: {string.Join(",", statBlock)}");

                log.LogInformation("Stats rolled successfully.");

                return new OkObjectResult(new
                {
                    success = true,
                    data = statBlock
                });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An error occurred while rolling stats.");
                DebugLogger.Log($"Error rolling stats: {ex.Message}");
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = $"Failed to roll stats: {ex.Message}"
                });
            }
        }
    }
}
