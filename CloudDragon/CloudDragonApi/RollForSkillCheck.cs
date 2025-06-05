using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonApi.Utils;

namespace CloudDragonApi
{
    public class SkillCheckInput
    {
        public int Modifier { get; set; } = 0;
        public int Dc { get; set; } = 10;
    }

    public static class RollForSkillCheck
    {
        [FunctionName("RollForSkillCheck")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "skill-check")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("RollForSkillCheck triggered");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                log.LogWarning("Empty request body.");
                return new BadRequestObjectResult(new { success = false, error = "Request body is empty." });
            }

            SkillCheckInput input;
            try
            {
                input = JsonConvert.DeserializeObject<SkillCheckInput>(requestBody);
                if (input == null)
                    throw new JsonException("Deserialized input is null.");
            }
            catch (JsonException ex)
            {
                log.LogError(ex, "Invalid JSON input.");
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
