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

namespace CloudDragonApi.Spellcasting
{
    public static class CastSpellFunction
    {
        [FunctionName("CastSpell")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-spell")] HttpRequest req,
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

            string spellName = input?.spell;
            int spellLevel = (int?)input?.level ?? 0;

            if (string.IsNullOrEmpty(spellName) || spellLevel <= 0)
                return new BadRequestObjectResult(new { success = false, error = "Spell name and level are required." });

            // Validate if character knows the spell
            if (character.PreparedSpells == null || !character.PreparedSpells.Contains(spellName))
                return new BadRequestObjectResult(new { success = false, error = $"Spell {spellName} is not prepared." });

            // Validate spell slot availability
            if (character.SpellSlots == null || !character.SpellSlots.ContainsKey(spellLevel) || character.SpellSlots[spellLevel] <= 0)
                return new BadRequestObjectResult(new { success = false, error = $"No available spell slots at level {spellLevel}." });

            // Burn a spell slot
            character.SpellSlots[spellLevel]--;

            // Optional: Log spell casting
            character.CastedSpells ??= new List<string>();
            character.CastedSpells.Add(spellName);

            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, message = $"Casted {spellName} (Level {spellLevel})" });
        }
    }
}
