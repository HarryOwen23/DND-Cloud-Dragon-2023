using System;
using System.IO;
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
            DebugLogger.Log("CreateCharacter endpoint hit");

            if (!ApiRequestHelper.IsAuthorized(req, log))
            {
                return new UnauthorizedResult();
            }

            var character = await ApiRequestHelper.ReadJsonAsync<Character>(req, log);
            DebugLogger.Log("Character payload parsed");

            if (character == null || string.IsNullOrWhiteSpace(character.Name))
                return new BadRequestObjectResult(new { success = false, error = "Invalid character data." });

            await characterOut.AddAsync(character);
            log.LogInformation("Character {Id} created", character.Id);
            DebugLogger.Log($"Character {character.Id} created");

            return new OkObjectResult(new { success = true, id = character.Id });
        }
    }
}
