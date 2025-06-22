using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.Equipment;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Equips an item from a character's inventory.
    /// </summary>
    public static class EquipItem
    {
        /// <summary>
        /// HTTP endpoint used to equip an item on a character.
        /// </summary>
        /// <param name="req">Request containing the item id.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist changes.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result describing success or failure.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("EquipItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/equip")] HttpRequest req,
            string id,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(requestBody);

            string itemId = data?.itemId;
            bool overwrite = data?.overwrite ?? false;

            if (string.IsNullOrEmpty(itemId))
                return new BadRequestObjectResult(new { success = false, error = "Missing itemId in request." });

            // Try to find the item in the character's inventory
            var item = character.Inventory.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                return new NotFoundObjectResult(new { success = false, error = $"Item '{itemId}' not found in inventory." });

            // Convert to EquipmentItem (if compatible)
            var equipItem = new EquipmentItem
            {
                Id = item.Id,
                Name = item.Name,
                Type = item.Type,
                Slot = item.Slot,
                Category = item.Category,
                ACBonus = item.ACBonus,
                Damage = item.Damage,
                Weight = item.Weight,
                Description = item.Description
            };

            var equipService = new EquipmentService();

            try
            {
                equipService.Equip(character, equipItem, overwrite);
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(new { success = false, error = ex.Message });
            }

            await characterOut.AddAsync(character);
            return new OkObjectResult(new { success = true, message = $"Equipped {equipItem.Name}." });
        }
    }
}
