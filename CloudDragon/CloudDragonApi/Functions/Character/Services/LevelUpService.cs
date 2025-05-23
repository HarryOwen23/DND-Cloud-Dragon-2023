using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

namespace CloudDragonApi.Services
{
    public static class LevelUpService
    {
        public static bool CheckSubclassUnlock(Character character)
        {
            if (character == null || string.IsNullOrWhiteSpace(character.Class))
                return false;

            string className = character.Class.ToLower();
            int requiredLevel = ClassSystemService.GetSubclassUnlockLevel(className);

            bool alreadyHasSubclass = character.Subclasses != null &&
                                       character.Subclasses.ContainsKey(className);

            return character.Level >= requiredLevel && !alreadyHasSubclass;
        }
    }
}

// 2. Create POST /character/{id}/level-up function
namespace CloudDragonApi.Character
{
    public static class CharacterLevelUpFunction
    {
        [FunctionName("LevelUpCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/level-up")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            string id,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            character.Level++;
            await characterOut.AddAsync(character);

            bool subclassAvailable = LevelUpService.CheckSubclassUnlock(character);

            return new OkObjectResult(new
            {
                success = true,
                newLevel = character.Level,
                subclassAvailable
            });
        }
    }
}

// 3. Build POST /character/{id}/subclass function
namespace CloudDragonApi.Character
{
    public static class AssignSubclassFunction
    {
        [FunctionName("AssignSubclass")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/subclass")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            string id,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string subclassName = input?.subclass;

            if (string.IsNullOrEmpty(subclassName))
                return new BadRequestObjectResult(new { success = false, error = "Subclass name is missing." });

            try
            {
                ClassSystemService.AssignSubclass(character, subclassName);
                await characterOut.AddAsync(character);

                return new OkObjectResult(new { success = true, message = $"Subclass {subclassName} assigned." });
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to assign subclass.");
                return new BadRequestObjectResult(new { success = false, error = ex.Message });
            }
        }
    }
}
