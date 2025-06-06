using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi;
using CloudDragonApi.Utils;

namespace CloudDragonApi.Functions.Conditions
{
    public static class GetConditionsFunction
    {
        [FunctionName("GetConditions")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "conditions")] HttpRequest req,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(GetConditions));
            DebugLogger.Log("Retrieving conditions list");

            var conditions = new List<Condition>
            {
                new Condition
                {
                    Name = "Blinded",
                    Effect = "Cannot see. Disadvantage on attack rolls.",
                    EndsOnTurnEnd = false
                },
                new Condition
                {
                    Name = "Poisoned",
                    Effect = "Disadvantage on attack rolls and ability checks.",
                    EndsOnTurnEnd = false
                },
                new Condition
                {
                    Name = "Stunned",
                    Effect = "Incapacitated. Can’t move. Fails STR/DEX saves.",
                    EndsOnTurnEnd = true
                }
            };

            return new OkObjectResult(new { success = true, data = conditions });
        }
    }

    public class Condition
    {
        public string Name { get; set; }
        public string Effect { get; set; }
        public bool EndsOnTurnEnd { get; set; }
    }
}
