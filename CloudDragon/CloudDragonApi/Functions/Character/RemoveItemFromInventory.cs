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

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class RemoveItemFromInventory
    {
        [FunctionName("RemoveItemFromInventory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/inventory/remove")] HttpRequest req,
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
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(requestBody);

            string itemId = data?.itemId;
            if (string.IsNullOrEmpty(itemId))
                return new BadRequestObjectResult(new { success = false, error = "Missing itemId." });

            var item = character.Inventory.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return new NotFoundObjectResult(new { success = false, error = $"Item '{itemId}' not found." });

            character.Inventory.Remove(item);
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, message = $"Removed '{item.Name}' from inventory." });
        }
    }
}
