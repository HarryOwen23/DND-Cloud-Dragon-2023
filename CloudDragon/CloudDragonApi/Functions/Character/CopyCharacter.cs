using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class CharacterCloneFunction
    {
        /// <summary>
        /// Creates a clone of an existing character document.
        /// </summary>
        /// <param name="req">Incoming HTTP request.</param>
        /// <param name="sourceChar">Source character loaded from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for the new clone.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response containing the identifier of the clone.</returns>
        [FunctionName("CopyCharacter")]
        public static async Task<IActionResult> CopyCharacter(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/clone")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel sourceChar,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            ILogger log)
        {
            if (sourceChar == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            var clone = new CharacterModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = $"{sourceChar.Name} (Clone)",
                Class = sourceChar.Class,
                Race = sourceChar.Race,
                Level = sourceChar.Level,
                Stats = new Dictionary<string, int>(sourceChar.Stats)
            };

            await characterOut.AddAsync(clone);

            return new OkObjectResult(new { success = true, id = clone.Id });
        }
    }
}