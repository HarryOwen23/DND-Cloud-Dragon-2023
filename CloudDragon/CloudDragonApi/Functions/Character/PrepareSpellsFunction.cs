using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Updates a character's list of prepared spells.
    /// </summary>
    public static class PrepareSpellsFunction
    {
        /// <summary>
        /// HTTP POST endpoint to set the prepared spells for a character.
        /// </summary>
        /// <param name="req">Request containing spells to prepare.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist the change.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Result listing the prepared spells.</returns>
        [FunctionName("PrepareSpells")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/prepare-spells")] HttpRequest req,
            string id,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
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
