using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;

namespace CloudDragonApi.Inventory_System
{
    /// <summary>
    /// Retrieves the inventory for a specific character.
    /// </summary>
    public static class ListInventory
    {
        /// <summary>
        /// Returns the inventory list and carried weight for the character.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character loaded from Cosmos DB.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Inventory details.</returns>
        [FunctionName("ListInventory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}/inventory")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character character,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(ListInventory));
            DebugLogger.Log($"Listing inventory for {id}");

            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            return new OkObjectResult(new
            {
                success = true,
                inventory = character.Inventory,
                carriedWeight = character.CarriedWeight
            });
        }
    }
}
