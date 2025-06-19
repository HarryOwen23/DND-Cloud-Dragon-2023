using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class CastCantripFunction
    {
        [FunctionName("CastCantrip")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-cantrip")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character character,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);

            string cantripName = input?.cantrip;

            if (string.IsNullOrEmpty(cantripName))
                return new BadRequestObjectResult(new { success = false, error = "Cantrip name is required." });

            // Validate that the cantrip is known (Inventory contains cantrip)
            bool knowsCantrip = character.Inventory?.Any(i => i.Name == cantripName && i.Type == "Cantrip") ?? false;

            if (!knowsCantrip)
                return new BadRequestObjectResult(new { success = false, error = $"Character does not know the cantrip {cantripName}." });

            log.LogInformation($"{character.Name} casts cantrip {cantripName}!");

            return new OkObjectResult(new { success = true, message = $"{character.Name} casts {cantripName}!" });
        }
    }
}
