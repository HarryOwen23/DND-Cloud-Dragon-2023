using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    /// <summary>
    /// Business logic helpers for leveling up characters.
    /// </summary>
    public static class LevelUpService
    {
        /// <summary>
        /// Determines whether a character becomes eligible to choose a subclass.
        /// </summary>
        /// <param name="character">Character being leveled.</param>
        /// <returns><c>true</c> if a subclass can be chosen.</returns>
        public static bool CheckSubclassUnlock(CharacterModel character)
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

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function for incrementing a character's level.
    /// </summary>
    public static class CharacterLevelUpFunction
    {
        /// <summary>
        /// Increments the level of the specified character.
        /// </summary>
        /// <param name="req">HTTP request triggering the level up.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for saving changes.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result containing new level information.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("LevelUpCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/level-up")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
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

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Assigns a subclass to an existing character.
    /// </summary>
    public static class AssignSubclassFunction
    {
        /// <summary>
        /// Updates the character's subclass if valid.
        /// </summary>
        /// <param name="req">HTTP request containing the subclass.</param>
        /// <param name="character">Character document to update.</param>
        /// <param name="characterOut">Output binding for saving.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Operation result.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("AssignSubclass")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/subclass")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
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
