using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models; // Make sure this points to the correct namespace for Character
using CloudDragonApi.Utils;

namespace CloudDragonApi.Functions.Character
{
    public static class DeleteCharacterFunction
    {
        [FunctionName("DeleteCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "character/{id}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character characterToDelete,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            string id,
            ILogger log)
        {
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
