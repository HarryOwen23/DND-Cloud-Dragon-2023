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
using CloudDragonLib.Models; // Assuming RacesPopulator lives here

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
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(body);
            string raceName = data?.race;

            if (string.IsNullOrWhiteSpace(raceName))
                return new BadRequestObjectResult(new { success = false, error = "Race not provided." });

            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json");
            var race = races.FirstOrDefault(r => r.Name.Equals(raceName, StringComparison.OrdinalIgnoreCase));

            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            if (race == null)
                return new NotFoundObjectResult(new { success = false, error = "Race not found." });

            character.Stats ??= new Dictionary<string, int>();

            foreach (var bonus in race.abilityBonuses)
            {
                if (character.Stats.ContainsKey(bonus.Key))
                    character.Stats[bonus.Key] += bonus.Value;
                else
                    character.Stats[bonus.Key] = bonus.Value;
            }

            character.Race = race.Name;
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, race = race.Name, stats = character.Stats });
        }
    }
}
