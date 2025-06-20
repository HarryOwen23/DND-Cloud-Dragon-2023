using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragon.CloudDragonApi.Utils;

using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function that adds an item to a character's inventory.
    /// </summary>
    public static class AddItemToInventory
    {
        /// <summary>
        /// Adds an item from the request body to the specified character.
        /// </summary>
        /// <param name="req">HTTP request containing the item payload.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for persisting the update.</param>
        /// <param name="context">The current function execution context.</param>
        /// <returns>The HTTP response describing the operation.</returns>
        [Function("AddItemToInventory")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/inventory/add")] HttpRequestData req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            FunctionContext context)
        {
            var log = context.GetLogger("AddItemToInventory");
            var response = req.CreateResponse();

            if (character == null)
            {
                DebugLogger.Log($"AddItemToInventory - character {id} not found");
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
            }

            DebugLogger.Log($"AddItemToInventory called for {id}");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var item = JsonConvert.DeserializeObject<Item>(requestBody);

            if (item == null || string.IsNullOrEmpty(item.Name))
            {
                DebugLogger.Log("Invalid item payload received");
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Invalid item data." });
                return response;
            }

            character.Inventory.Add(item);
            await characterOut.AddAsync(character);
            DebugLogger.Log($"Added {item.Name} to {id}'s inventory");

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, message = $"Added '{item.Name}' to inventory." });
            return response;
        }
    }
}
