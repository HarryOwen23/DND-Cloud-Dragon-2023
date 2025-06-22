using System.Net;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character; 

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function responsible for casting spells from a character sheet.
    /// </summary>
    public class CastSpellFunction
    {
        private readonly CosmosClient _cosmosClient;
        private const string DatabaseName = "CloudDragonDB";
        private const string ContainerName = "Characters";

        public CastSpellFunction(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        /// <summary>
        /// Casts a spell for the specified character.
        /// </summary>
        /// <param name="req">HTTP request containing the spell info.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="logger">Function logger.</param>
        /// <returns>Result with cast confirmation.</returns>
        [FunctionName("CastSpell")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-spell")] HttpRequest req,
            string id,
            ILogger logger)
        {
            

            // Parse input
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonSerializer.Deserialize<SpellCastInput>(body);

            if (input == null || string.IsNullOrEmpty(input.Spell) || input.Level <= 0)
            {
                return new BadRequestObjectResult(new { success = false, error = "Spell name and level are required." });
            }

            // Get container and fetch character
            var container = _cosmosClient.GetContainer(DatabaseName, ContainerName);
            var character = await GetCharacterAsync(id, container);

            if (character is null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            var c = character;

            // Validate spell preparation
            if (c.PreparedSpells == null || !c.PreparedSpells.Contains(input.Spell))
            {
                return new BadRequestObjectResult(new { success = false, error = $"Spell '{input.Spell}' is not prepared." });
            }

            // Validate spell slot availability
            if (c.SpellSlots == null || !c.SpellSlots.ContainsKey(input.Level) || c.SpellSlots[input.Level] <= 0)
            {
                return new BadRequestObjectResult(new { success = false, error = $"No available spell slots at level {input.Level}." });
            }

            // Cast the spell
            c.SpellSlots[input.Level]--;
            c.CastedSpells ??= new List<string>();
            c.CastedSpells.Add(input.Spell);

            // Save updated character
            await container.ReplaceItemAsync(c, c.Id, new PartitionKey(c.Id));

            return new OkObjectResult(new { success = true, message = $"Casted {input.Spell} (Level {input.Level})" });
        }

        /// <summary>
        /// Retrieves the character document from Cosmos DB.
        /// </summary>
        /// <param name="id">Character identifier.</param>
        /// <param name="container">Cosmos DB container.</param>
        /// <returns>The character or <c>null</c> if not found.</returns>
        private async Task<CharacterModel?> GetCharacterAsync(string id, Container container)
        {
            try
            {
                var response = await container.ReadItemAsync<CharacterModel>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Payload describing the spell to cast and at what level.
    /// </summary>
    public class SpellCastInput
    {
        /// <summary>Name of the spell being cast.</summary>
        public string Spell { get; set; }

        /// <summary>Slot level used to cast the spell.</summary>
        public int Level { get; set; }
    }
}
