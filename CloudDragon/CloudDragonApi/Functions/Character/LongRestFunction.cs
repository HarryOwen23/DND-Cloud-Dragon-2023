using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Restores spell slots and clears cast spells for a character.
    /// </summary>
    public static class LongRestFunction
    {
        /// <summary>
        /// Azure Function HTTP trigger to process a long rest event.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document loaded from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for persisting changes.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>A result indicating success.</returns>
        [FunctionName("LongRest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/long-rest")] HttpRequest req,
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
