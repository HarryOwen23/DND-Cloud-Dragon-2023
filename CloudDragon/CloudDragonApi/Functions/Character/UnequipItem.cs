using System.IO;
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
using CloudDragon.Equipment;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Removes an equipped item from the specified slot on a character.
    /// </summary>
    public static class UnequipItem
    {
        /// <summary>
        /// HTTP entry point for unequipping an item.
        /// </summary>
        /// <param name="req">Request containing slot information.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist the update.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result containing success status.</returns>
        [FunctionName("UnequipItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/unequip")] HttpRequest req,
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
