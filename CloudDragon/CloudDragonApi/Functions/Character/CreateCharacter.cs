using System;
using System.IO;
using.System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CloudDragonApi.Utils;

namespace CloudDragonApi.Functions.Character
{
    public static class CreateCharacterFunction
    {
        [FunctionName("CreateCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(CreateCharacter));
            log.LogInformation("CreateCharacter endpoint hit");

            if (!ApiRequestHelper.IsAuthorized(req, log))
            {
                return new UnauthorizedResult();
            }

            var character = await ApiRequestHelper.ReadJsonAsync<Character>(req, log);
            log.LogInformation("Character payload parsed");

            if (character == null)
            {
                return new BadRequestObjectResult(new { success = false, error = "Character payload is missing or invalid." });
            }

            if (string.IsNullOrWhiteSpace(character.Name))
            {
                return new BadRequestObjectResult(new { success = false, error = "Character name is required." });
            }

            if (character.Stats == null || character.Stats.Count == 0)
            {
                return new BadRequestObjectResult(new { success = false, error = "Character stats are required." });
            }

            var requiredStats = new[] { "STR", "DEX", "CON", "INT", "WIS", "CHA" };
            bool hasAllStats = requiredStats.All(stat =>
                character.Stats != null &&
                character.Stats.ContainsKey(stat) &&
                character.Stats[stat] >= 1 && character.Stats[stat] <= 20
             );

            if (!hasAllStats)
            {
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = "Character must have valid stats for STR, DEX, CON, INT, WIS, and CHA (1-20)."
                });
            }

            await characterOut.AddAsync(character);
            log.LogInformation("Character {Id} created", character.Id);
            log.LogInformation($"Character {character.Id} created");

            return new CreatedResult($"/character/{character.id}", new { success = true, id = character.Id });
        }
    }
}
