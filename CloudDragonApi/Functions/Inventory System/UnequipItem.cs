using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using CloudDragonLib.Models;
using CloudDragon.Equipment;

namespace CloudDragonApi.Inventory_System
{
    public static class UnequipItem
    {
        [FunctionName("UnequipItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/unequip")] HttpRequest req,
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

            string slot = data?.slot;
            if (string.IsNullOrWhiteSpace(slot))
                return new BadRequestObjectResult(new { success = false, error = "Missing slot in request." });

            var equipService = new EquipmentService();
            bool removed = equipService.Unequip(character, slot);

            if (!removed)
                return new BadRequestObjectResult(new { success = false, error = $"No item equipped in slot '{slot}'." });

            await characterOut.AddAsync(character);
            return new OkObjectResult(new { success = true, message = $"Unequipped item from slot '{slot}'." });
        }
    }
}
