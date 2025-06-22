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

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Removes an item from a character's inventory.
    /// </summary>
    public static class RemoveItemFromInventory
    {
        /// <summary>
        /// HTTP endpoint used to delete an item from inventory.
        /// </summary>
        /// <param name="req">Request containing the item id.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document loaded from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist the removal.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result with removal confirmation.</returns>
        [FunctionName("RemoveItemFromInventory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/inventory/remove")] HttpRequest req,
            string id,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
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
