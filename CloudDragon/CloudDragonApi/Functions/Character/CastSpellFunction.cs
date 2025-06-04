using System.Net;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character; 

namespace CloudDragonApi.Spellcasting
{
    public class CastSpellFunction
    {
        private readonly CosmosClient _cosmosClient;
        private const string DatabaseName = "CloudDragonDB";
        private const string ContainerName = "Characters";

        public CastSpellFunction(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        [Function("CastSpell")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/cast-spell")] HttpRequestData req,
            string id,
            FunctionContext context)
        {
            var logger = context.GetLogger("CastSpell");
            var response = req.CreateResponse();

            // Parse input
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonSerializer.Deserialize<SpellCastInput>(body);

            if (input == null || string.IsNullOrEmpty(input.Spell) || input.Level <= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Spell name and level are required." });
                return response;
            }

            // Get container and fetch character
            var container = _cosmosClient.GetContainer(DatabaseName, ContainerName);
            var character = await GetCharacterAsync(id, container);

            if (character is null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
            }

            var c = character;

            // Validate spell preparation
            if (c.PreparedSpells == null || !c.PreparedSpells.Contains(input.Spell))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = $"Spell '{input.Spell}' is not prepared." });
                return response;
            }

            // Validate spell slot availability
            if (c.SpellSlots == null || !c.SpellSlots.ContainsKey(input.Level) || c.SpellSlots[input.Level] <= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = $"No available spell slots at level {input.Level}." });
                return response;
            }

            // Cast the spell
            c.SpellSlots[input.Level]--;
            c.CastedSpells ??= new List<string>();
            c.CastedSpells.Add(input.Spell);

            // Save updated character
            await container.ReplaceItemAsync(c, c.Id, new PartitionKey(c.Id));

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, message = $"Casted {input.Spell} (Level {input.Level})" });
            return response;
        }

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

    public class SpellCastInput
    {
        public string Spell { get; set; }
        public int Level { get; set; }
    }
}
