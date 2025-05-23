using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using System.Threading.Tasks;

namespace CloudDragonApi.Character
{
    public static class GetCharacter
    {
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

            if (character == null)
            {
                return new NotFoundObjectResult(new
                {
                    success = false,
                    error = $"Character with ID '{id}' not found."
                });
            }

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
