using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            }
            catch (JsonException ex)
            {
                log.LogError(ex, "Invalid JSON input.");
                return new BadRequestObjectResult(new { success = false, error = "Invalid JSON format." });
            }

            var rng = new Random();
            int roll = rng.Next(1, 21); // Simulate d20
            int total = roll + input.Modifier;
            bool success = total >= input.Dc;

            log.LogInformation("Skill check roll: {Roll}, Modifier: {Modifier}, Total: {Total}, DC: {DC}, Success: {Success}",
                roll, input.Modifier, total, input.Dc, success);

            return new OkObjectResult(new
            {
                success = true,
                data = new
                {
                    roll,
                    modifier = input.Modifier,
                    dc = input.Dc,
                    total,
                    success
                }
            });
        }
    }
}
