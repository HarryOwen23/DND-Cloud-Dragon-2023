using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models; // Make sure this points to the correct namespace for Character
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function that performs a soft delete on a character document.
    /// </summary>
    public static class DeleteCharacter
    {
        /// <summary>
        /// Marks the specified character as deleted in Cosmos DB.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="characterToDelete">Character instance loaded from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist the updated character.</param>
        /// <param name="id">Identifier of the character.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response describing the result.</returns>
        [FunctionName("DeleteCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "character/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel characterToDelete,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            string id,
            ILogger log)
        {
            log.LogInformation("DeleteCharacter called for {Id}", id);
            DebugLogger.Log($"DeleteCharacter called for {id}");

            if (characterToDelete == null)
            {
                DebugLogger.Log($"DeleteCharacter - character {id} not found");
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            DebugLogger.Log($"Deleting character {id} (soft delete)");
            // Soft delete logic
            characterToDelete.Name += " (DELETED)";
            characterToDelete.Level = -1;

            await characterOut.AddAsync(characterToDelete);
            DebugLogger.Log($"Character {id} marked as deleted");
            return new OkObjectResult(new { success = true, message = "Character deleted (soft delete)." });
        }
    }
}