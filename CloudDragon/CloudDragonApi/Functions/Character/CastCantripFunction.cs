using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function that allows a character to cast a cantrip.
    /// </summary>
    public static class CastCantripFunction
    {
        /// <summary>
        /// Validates the request and logs the cantrip casting action.
        /// </summary>
        /// <param name="req">HTTP request containing the cantrip name.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="context">Function execution context.</param>
        /// <returns>HTTP response describing the outcome.</returns>
        [Function("CastCantrip")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-cantrip")] HttpRequestData req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            string id,
            FunctionContext context)
        {
            var log = context.GetLogger("CastCantrip");
            var response = req.CreateResponse();

            if (character == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
            }

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);

            string cantripName = input?.cantrip;

            if (string.IsNullOrEmpty(cantripName))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Cantrip name is required." });
                return response;
            }

            bool knowsCantrip = character.Inventory?.Any(i => i.Name == cantripName && i.Type == "Cantrip") ?? false;

            if (!knowsCantrip)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = $"Character does not know the cantrip {cantripName}." });
                return response;
            }

            log.LogInformation($"{character.Name} casts cantrip {cantripName}!");

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, message = $"{character.Name} casts {cantripName}!" });
            return response;
        }
    }
}