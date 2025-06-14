using System;
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
using CloudDragonApi.Utils;

namespace CloudDragonApi.Functions.Character
{
    public static class ApplyRaceFunction
    {
        [FunctionName("ApplyRace")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/apply-race")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CloudDragonLib.Models.Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CloudDragonLib.Models.Character> characterOut,
            ILogger log)
        {
            DebugLogger.Log($"ApplyRace triggered for {id}");
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            DebugLogger.Log($"Request Body: {body}");
            var data = JsonConvert.DeserializeObject<dynamic>(body);
            string raceName = data?.race;

            if (string.IsNullOrWhiteSpace(raceName))
            {
                DebugLogger.Log("Race not provided in request");
                return new BadRequestObjectResult(new { success = false, error = "Race not provided." });
            }

            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json");
            DebugLogger.Log($"Loaded {races.Count} races");
            var race = races.FirstOrDefault(r => r.Name.Equals(raceName, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                DebugLogger.Log($"Character {id} not found");
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            if (race == null)
            {
                DebugLogger.Log($"Race not found: {raceName}");
                return new NotFoundObjectResult(new { success = false, error = "Race not found." });
            }

            character.Stats ??= new Dictionary<string, int>();

            foreach (var bonus in race.AbilityBonuses)
            {
                if (character.Stats.ContainsKey(bonus.Key))
                    character.Stats[bonus.Key] += bonus.Value;
                else
                    character.Stats[bonus.Key] = bonus.Value;
            }

            character.Race = race.Name;
            await characterOut.AddAsync(character);

            DebugLogger.Log($"Applied race {race.Name} to {id}");

            return new OkObjectResult(new { success = true, race = race.Name, stats = character.Stats });
        }
    }
}
