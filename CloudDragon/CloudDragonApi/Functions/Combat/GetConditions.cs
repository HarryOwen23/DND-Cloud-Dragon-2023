using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Returns a list of common combat conditions.
    /// </summary>
    public static class GetConditionsFunction
    {
        /// <summary>
        /// HTTP trigger that returns available combat conditions.
        /// </summary>
        /// <param name="req">The incoming HTTP request.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Collection of conditions.</returns>
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

    /// <summary>
    /// Representation of a combat condition and its basic effects.
    /// </summary>
    public class Condition
    {
        /// <summary>Name of the condition.</summary>
        public string Name { get; set; }
        /// <summary>Short description of the effect.</summary>
        public string Effect { get; set; }
        /// <summary>Whether the condition automatically ends at turn end.</summary>
        public bool EndsOnTurnEnd { get; set; }
    }
}
