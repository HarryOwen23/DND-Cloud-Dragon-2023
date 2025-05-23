using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;

namespace CloudDragonApi.Functions.Character
{
    public static class CharacterCloneFunction
    {
        [FunctionName("CopyCharacter")]
        public static async Task<IActionResult> CopyCharacter(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/clone")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character sourceChar,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            ILogger log)
        {
            if (sourceChar == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            var clone = new Character
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
