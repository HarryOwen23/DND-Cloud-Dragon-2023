using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models; // Ensure the Character class is in this namespace

namespace CloudDragonApi.Functions.Character
{
    public static class ListCharactersFunction
    {
        [FunctionName("ListCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "characters")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                SqlQuery = "SELECT * FROM c WHERE c.Level > 0",
                Connection = "CosmosDBConnection")] IEnumerable<Character> characters,
            ILogger log)
        {
            return new OkObjectResult(new { success = true, data = characters });
        }
    }
}
