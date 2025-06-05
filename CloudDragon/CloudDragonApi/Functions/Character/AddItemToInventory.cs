using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonApi.Utils;

using CloudDragonLib.Models;

namespace CloudDragonApi.Inventory_System
{
    public static class AddItemToInventory
    {
        [FunctionName("AddItemToInventory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/inventory/add")] HttpRequest req,
            string id,
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
            ILogger log)
        {
            if (character == null)
            {
                DebugLogger.Log($"AddItemToInventory - character {id} not found");
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            DebugLogger.Log($"AddItemToInventory called for {id}");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var item = JsonConvert.DeserializeObject<Item>(requestBody);

            if (item == null || string.IsNullOrEmpty(item.Name))
            {
                DebugLogger.Log("Invalid item payload received");
                return new BadRequestObjectResult(new { success = false, error = "Invalid item data." });
            }

            character.Inventory.Add(item);
            await characterOut.AddAsync(character);
            DebugLogger.Log($"Added {item.Name} to {id}'s inventory");

            return new OkObjectResult(new { success = true, message = $"Added '{item.Name}' to inventory." });
        }
    }
}
