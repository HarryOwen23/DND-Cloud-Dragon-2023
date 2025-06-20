using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
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
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response describing the outcome.</returns>
        [FunctionName("CastCantrip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-cantrip")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            string id,
            ILogger log)
        {
            

            if (character == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);

            string cantripName = input?.cantrip;

            if (string.IsNullOrEmpty(cantripName))
            {
                return new BadRequestObjectResult(new { success = false, error = "Cantrip name is required." });
            }

            bool knowsCantrip = character.Inventory?.Any(i => i.Name == cantripName && i.Type == "Cantrip") ?? false;

            if (!knowsCantrip)
            {
                return new BadRequestObjectResult(new { success = false, error = $"Character does not know the cantrip {cantripName}." });
            }

            log.LogInformation($"{character.Name} casts cantrip {cantripName}!");

            return new OkObjectResult(new { success = true, message = $"{character.Name} casts {cantripName}!" });
        }
    }
}