using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib;
using System;
using System.Threading.Tasks;

namespace CloudDragonApi
{
    public static class RollForStatsFunction
    {
        [FunctionName("RollStats")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "roll-stats")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("RollForStatsFunction triggered.");

            try
            {
                var roller = new Character_Stats_Dice();
                var statBlock = roller.RollStats();

                log.LogInformation("Stat block rolled successfully.");

                return new OkObjectResult(new
                {
                    success = true,
                    data = statBlock
                });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error rolling stats.");
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = "Failed to roll stats. " + ex.Message
                });
            }
        }
    }
}
