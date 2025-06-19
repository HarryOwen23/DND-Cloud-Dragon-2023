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
using CloudDragon.CloudDragonApi.Functions.Character.Services;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class LevelUpOptionsFunction
    {
        [FunctionName("GetLevelUpOptions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/level-up-options")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            ILogger log)
        {
            if (character == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            int level = character.Level;

            bool canIncreaseStats = (level >= 4 && (level - 4) % 4 == 0);
            bool canChooseFeat = canIncreaseStats; // In D&D, usually same level as stat boosts
            bool subclassAvailable = LevelUpService.CheckSubclassUnlock(character);

            List<string> newSpellsAvailable = new();

            // Check if the class is a caster class and suggest spell unlocking
            var casterClasses = new List<string> { "wizard", "cleric", "sorcerer", "warlock", "druid", "bard", "paladin", "ranger", "artificer" };
            if (casterClasses.Contains(character.Class?.ToLower()))
            {
                // Very basic idea: assume new spells every even level
                if (level % 2 == 0)
                {
                    newSpellsAvailable.Add("New spells available (placeholder)");
                }
            }

            return new OkObjectResult(new
            {
                success = true,
                canIncreaseStats,
                canChooseFeat,
                newSpellsAvailable,
                subclassAvailable
            });
        }
    }
}
