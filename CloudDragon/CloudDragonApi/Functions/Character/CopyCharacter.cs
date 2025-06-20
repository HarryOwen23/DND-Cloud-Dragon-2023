using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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
        /// <param name="context">Function execution context.</param>
        /// <returns>HTTP response containing the identifier of the clone.</returns>
        [Function("CopyCharacter")]
        public static async Task<HttpResponseData> CopyCharacter(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/clone")] HttpRequestData req,
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
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(CopyCharacter));

            var response = req.CreateResponse();

            if (sourceChar == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
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

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, id = clone.Id });
            return response;
        }
    }
}