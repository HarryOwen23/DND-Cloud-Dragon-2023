using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models; // Ensure the Character class is in this namespace
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Lists all characters stored in Cosmos DB.
    /// </summary>
    public static class ListCharactersFunction
    {
        /// <summary>
        /// Returns every character document in the database.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="characters">Enumeration of characters from Cosmos DB.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>List of characters.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("ListCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "characters")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Characters",
                SqlQuery = "SELECT * FROM c WHERE c.Level > 0",
                ConnectionStringSetting = "CosmosDBConnection")] IEnumerable<CharacterModel> characters,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(ListCharacters));
            DebugLogger.Log("ListCharacters called");
            return new OkObjectResult(new { success = true, data = characters });
        }
    }
}
