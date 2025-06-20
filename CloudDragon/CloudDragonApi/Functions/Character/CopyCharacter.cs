using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
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
                PartitionKey = "{id}")] CharacterModel sourceChar,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            ILogger log)
        {
            if (sourceChar == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

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
