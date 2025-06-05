using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CloudDragonApi.Utils;

namespace CloudDragonApi.Services
{
    public static class SpellSlotService 
    {
        [FunctionName("GetSpellSlots")]
        public static IActionResult GetSpellSlots(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}/spell-slots")] HttpRequest req,
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

            DebugLogger.Log($"Retrieving spell slots for character {character.Id}");
            return new OkObjectResult(new { success = true, spellSlots = character.SpellSlots });
        }
        [FunctionName("UseSpellSlot")]
        public static async Task<IActionResult> UseSpellSlot(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/spell-slots/use")] HttpRequest req,
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
            int level = input?.level;

            if (level <= 0 || !character.SpellSlots.ContainsKey(level) || character.SpellSlots[level] <= 0)
                return new BadRequestObjectResult(new { success = false, error = "No available spell slots at that level." });

            character.SpellSlots[level]--;
            DebugLogger.Log($"Character {character.Id} used a level {level} spell slot. Remaining: {character.SpellSlots[level]}");

            await characterOut.AddAsync(character);
            return new OkObjectResult(new { success = true, remaining = character.SpellSlots[level] });
        }

        [FunctionName("LongRestRecoverSlots")]
        public static async Task<IActionResult> LongRestRecoverSlots(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/long-rest")] HttpRequest req,
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

            // TODO: Replace this with proper per-class slot recovery later if needed.
            foreach (var key in character.SpellSlots.Keys.ToList())
            {
                character.SpellSlots[key] = GetMaxSpellSlots(character.Level, key);
            }

            DebugLogger.Log($"Character {character.Id} completed long rest. Slots reset.");

            await characterOut.AddAsync(character);
            return new OkObjectResult(new { success = true, spellSlots = character.SpellSlots });
        }

        private static int GetMaxSpellSlots(int characterLevel, int spellLevel)
        {
            // Simplified logic — we can replace with real D&D rules later if you want.
            if (characterLevel < spellLevel * 2)
                return 0;
            if (spellLevel == 1)
                return 4;
            if (spellLevel == 2)
                return 3;
            if (spellLevel == 3)
                return 3;
            if (spellLevel == 4)
                return 2;
            if (spellLevel == 5)
                return 1;
            return 0;
        }
    }
}
