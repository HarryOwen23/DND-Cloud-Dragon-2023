using System;
using System.Collections.Generic;
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
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class ApplyLevelUpFunction
    {
        [FunctionName("ApplyLevelUp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/apply-levelup")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            string id,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);

            // Handle stat increases
            var statIncreases = input?.statIncreases?.ToObject<Dictionary<string, int>>();
            if (statIncreases != null)
            {
                foreach (var kvp in statIncreases)
                {
                    if (!character.Stats.ContainsKey(kvp.Key))
                        character.Stats[kvp.Key] = 0;

                    character.Stats[kvp.Key] += kvp.Value;
                }
            }

            // Handle feats
            var chosenFeat = input?.feat;
            if (chosenFeat != null)
            {
                character.Inventory.Add(new Item
                {
                    Name = (string)chosenFeat,
                    Type = "Feat"
                });
            }

            // Handle new spells
            var learnedSpells = input?.newSpells?.ToObject<List<string>>();
            if (learnedSpells != null)
            {
                foreach (var spellName in learnedSpells)
                {
                    character.Inventory.Add(new Item
                    {
                        Name = spellName,
                        Type = "Spell"
                    });
                }
            }

            await characterOut.AddAsync(character);

            return new OkObjectResult(new
            {
                success = true,
                message = "Level-up applied successfully.",
                updatedCharacterId = character.Id
            });
        }
    }
}