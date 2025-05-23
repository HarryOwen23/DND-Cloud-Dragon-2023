using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models; // Make sure this points to the correct namespace for Character

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
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            // Soft delete logic
            characterToDelete.Name += " (DELETED)";
            characterToDelete.Level = -1;

            await characterOut.AddAsync(characterToDelete);
            return new OkObjectResult(new { success = true, message = "Character deleted (soft delete)." });
        }
    }
}
