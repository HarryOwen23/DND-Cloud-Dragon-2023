using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class LongRestFunction
    {
        [FunctionName("LongRest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/long-rest")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            // Fully restore spell slots
            if (character.SpellSlots != null)
            {
                foreach (var level in character.SpellSlots.Keys.ToList())
                {
                    // Very basic: restore 2 slots per level (adjust as needed)
                    character.SpellSlots[level] = 2;
                }
            }

            // Clear casted spells (optional, for tracking purposes)
            character.CastedSpells?.Clear();

            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, message = $"{character.Name} has completed a long rest and restored spell slots." });
        }
    }
}
