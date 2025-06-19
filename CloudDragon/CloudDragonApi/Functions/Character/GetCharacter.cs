using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using System.Threading.Tasks;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function that retrieves a single character by ID.
    /// </summary>
    public static class GetCharacter
    {
        /// <summary>
        /// Gets a character from Cosmos DB and returns key fields.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="character">Character loaded from Cosmos DB.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Action result containing the character or error.</returns>
        [FunctionName("GetCharacter")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character character,
            string id,
            ILogger log)
        {
            log.LogInformation("GetCharacter triggered for ID: {Id}", id);
            DebugLogger.Log($"GetCharacter requested for {id}");

            if (character == null)
            {
                DebugLogger.Log($"Character {id} not found");
                return new NotFoundObjectResult(new
                {
                    success = false,
                    error = $"Character with ID '{id}' not found."
                });
            }

            DebugLogger.Log($"Returning character {character.Id}");
            return new OkObjectResult(new
            {
                success = true,
                character.Id,
                character.Name,
                character.Class,
                character.Race,
                character.Level,
                character.Stats,
                Equipped = character.Equipped,
                Inventory = character.Inventory,
                AC = character.AC,
                CarriedWeight = character.CarriedWeight,
                CreatedAt = character.CreatedAt
            });
        }
    }
}
