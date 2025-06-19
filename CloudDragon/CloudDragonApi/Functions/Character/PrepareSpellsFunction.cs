using System.IO;
using System.Linq;
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
    public static class PrepareSpellsFunction
    {
        [FunctionName("PrepareSpells")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/prepare-spells")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);

            var spellsToPrepare = ((IEnumerable<dynamic>)input?.spells)?.Select(s => (string)s)?.ToList();
            if (spellsToPrepare == null || !spellsToPrepare.Any())
                return new BadRequestObjectResult(new { success = false, error = "No spells provided to prepare." });

            character.PreparedSpells = spellsToPrepare;
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, prepared = spellsToPrepare });
        }
    }
}
