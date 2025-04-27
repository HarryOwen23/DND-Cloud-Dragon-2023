using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace CloudDragonApi.Services
{
    public static class SpellcastingService
    {
        [FunctionName("GetAvailableSpells")]
        public static async Task<IActionResult> GetAvailableSpells(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "spells/{className}/{level}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Spells",
                SqlQuery = "SELECT * FROM c WHERE c.ClassName = {className} AND c.Level = {level}",
                Connection = "CosmosDBConnection")] IEnumerable<dynamic> spells,
            string className,
            int level,
            ILogger log)
        {
            if (spells == null || !spells.Any())
            {
                return new NotFoundObjectResult(new { success = false, error = "No spells found for this class and level." });
            }

            return new OkObjectResult(new { success = true, spells = spells });
        }

        [FunctionName("GetAvailableCantrips")]
        public static async Task<IActionResult> GetAvailableCantrips(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cantrips/{className}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Spells",
                SqlQuery = "SELECT * FROM c WHERE c.ClassName = {className} AND c.Type = 'Cantrip'",
                Connection = "CosmosDBConnection")] IEnumerable<dynamic> cantrips,
            string className,
            ILogger log)
        {
            if (cantrips == null || !cantrips.Any())
            {
                return new NotFoundObjectResult(new { success = false, error = "No cantrips found for this class." });
            }

            return new OkObjectResult(new { success = true, cantrips = cantrips });
        }

        [FunctionName("LearnSpell")]
        public static async Task<IActionResult> LearnSpell(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/learn-spell")] HttpRequest req,
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

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string spellName = input?.spell;

            if (string.IsNullOrEmpty(spellName))
                return new BadRequestObjectResult(new { success = false, error = "Spell name is missing." });

            character.Inventory.Add(new Item { Name = spellName, Type = "Spell" });
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, message = $"Spell {spellName} learned." });
        }

        [FunctionName("PrepareSpells")]
        public static async Task<IActionResult> PrepareSpells(
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

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            List<string> preparedSpells = input?.preparedSpells?.ToObject<List<string>>();

            if (preparedSpells == null || preparedSpells.Count == 0)
                return new BadRequestObjectResult(new { success = false, error = "No spells provided for preparation." });

            // You could implement a "PreparedSpells" list or flag prepared spells differently
            foreach (var spellName in preparedSpells)
            {
                character.Inventory.Add(new Item { Name = spellName, Type = "PreparedSpell" });
            }

            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, prepared = preparedSpells });
        }

        [FunctionName("CastSpell")]
        public static async Task<IActionResult> CastSpell(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-spell")] HttpRequest req,
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

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string spellName = input?.spell;

            if (string.IsNullOrEmpty(spellName))
                return new BadRequestObjectResult(new { success = false, error = "Spell name is missing." });

            log.LogInformation($"Character {character.Name} casts {spellName}!");

            return new OkObjectResult(new { success = true, message = $"Spell {spellName} casted!" });
        } 
    }
}
