using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi
{
    /// <summary>
    /// Input payload for performing a skill check.
    /// </summary>
    public class SkillCheckInput
    {
        /// <summary>Bonus modifier applied to the d20 roll.</summary>
        public int Modifier { get; set; } = 0;

        /// <summary>Difficulty class that must be met or exceeded.</summary>
        public int Dc { get; set; } = 10;
    }

    /// <summary>
    /// Azure Function that simulates a single skill check roll.
    /// </summary>
    public static class RollForSkillCheck
    {
        /// <summary>
        /// Performs the skill check using the provided modifiers.
        /// </summary>
        /// <param name="req">HTTP request containing <see cref="SkillCheckInput"/>.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Roll result data.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("RollForSkillCheck")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "skill-check")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("RollForSkillCheck triggered");

            if (!ApiRequestHelper.IsAuthorized(req, log))
            {
                return new UnauthorizedResult();
            }

            var input = await ApiRequestHelper.ReadJsonAsync<SkillCheckInput>(req, log);
            if (input == null)
            {
                return new BadRequestObjectResult(new { success = false, error = "Invalid JSON format." });
            }

            int roll = Random.Shared.Next(1, 21); // d20
            int total = roll + input.Modifier;
            bool passed = total >= input.Dc;

            DebugLogger.Log($"Skill check roll={roll}, modifier={input.Modifier}, dc={input.Dc}, total={total}, passed={passed}");

            log.LogInformation("Skill check: roll={Roll}, modifier={Modifier}, total={Total}, dc={Dc}, success={Success}",
                roll, input.Modifier, total, input.Dc, passed);

            return new OkObjectResult(new
            {
                success = true,
                data = new
                {
                    roll,
                    modifier = input.Modifier,
                    dc = input.Dc,
                    total,
                    passed
                }
            });
        }
    }
}
