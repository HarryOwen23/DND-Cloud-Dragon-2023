using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    /// <summary>
    /// Helper functions for retrieving and modifying a character's spell slots.
    /// </summary>
    public static class SpellSlotService 
    {
        /// <summary>
        /// Returns the current spell slot counts for the specified character.
        /// </summary>
        [FunctionName("GetSpellSlots")]
        public static IActionResult GetSpellSlots(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}/spell-slots")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CharacterModel character,
            ILogger log)
        {
            if (character == null)
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });

            DebugLogger.Log($"Retrieving spell slots for character {character.Id}");
            return new OkObjectResult(new { success = true, spellSlots = character.SpellSlots });
        }
        /// <summary>
        /// Consumes a spell slot of the specified level if available.
        /// </summary>
        [FunctionName("UseSpellSlot")]
        public static async Task<IActionResult> UseSpellSlot(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/spell-slots/use")] HttpRequest req,
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

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            int level = input?.level;

            if (level <= 0 || !character.SpellSlots.ContainsKey(level) || character.SpellSlots[level] <= 0)
                return new BadRequestObjectResult(new { success = false, error = "No available spell slots at that level." });

            character.SpellSlots[level]--;
            DebugLogger.Log($"Character {character.Id} used a level {level} spell slot. Remaining: {character.SpellSlots[level]}");

            await characterOut.AddAsync(character);
            return new OkObjectResult(new { success = true, remaining = character.SpellSlots[level] });
        }

        /// <summary>
        /// Resets all spell slots to their maximum values after a long rest.
        /// </summary>
        [FunctionName("LongRestRecoverSlots")]
        public static async Task<IActionResult> LongRestRecoverSlots(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/long-rest")] HttpRequest req,
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

            // Currently assumes full caster progression for all classes.
            foreach (var key in character.SpellSlots.Keys.ToList())
            {
                character.SpellSlots[key] = GetMaxSpellSlots(character.Level, key);
            }

            DebugLogger.Log($"Character {character.Id} completed long rest. Slots reset.");

            await characterOut.AddAsync(character);
            return new OkObjectResult(new { success = true, spellSlots = character.SpellSlots });
        }

        private static readonly Dictionary<int, Dictionary<int, int>> FullCasterSlots = new()
        {
            [1] = new() { [1] = 2 },
            [2] = new() { [1] = 3 },
            [3] = new() { [1] = 4, [2] = 2 },
            [4] = new() { [1] = 4, [2] = 3 },
            [5] = new() { [1] = 4, [2] = 3, [3] = 2 },
            [6] = new() { [1] = 4, [2] = 3, [3] = 3 },
            [7] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 1 },
            [8] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 2 },
            [9] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 1 },
            [10] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2 },
            [11] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1 },
            [12] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1 },
            [13] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1, [7] = 1 },
            [14] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1, [7] = 1 },
            [15] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1, [7] = 1, [8] = 1 },
            [16] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1, [7] = 1, [8] = 1 },
            [17] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 2, [6] = 1, [7] = 1, [8] = 1, [9] = 1 },
            [18] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 3, [6] = 1, [7] = 1, [8] = 1, [9] = 1 },
            [19] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 3, [6] = 2, [7] = 1, [8] = 1, [9] = 1 },
            [20] = new() { [1] = 4, [2] = 3, [3] = 3, [4] = 3, [5] = 3, [6] = 2, [7] = 2, [8] = 1, [9] = 1 }
        };

        private static int GetMaxSpellSlots(int characterLevel, int spellLevel)
        {
            if (FullCasterSlots.TryGetValue(characterLevel, out var levels) &&
                levels.TryGetValue(spellLevel, out int result))
            {
                return result;
            }
            return 0;
        }
    }
}
