using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Helper Azure Functions for character management.
    /// </summary>
    public static class CharacterUtilities
    {
        /// <summary>
        /// Clears all stats for the specified character document.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character loaded from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for the updated character.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response describing the result.</returns>
        [FunctionName("ResetCharacterStats")]
        public static async Task<IActionResult> ResetCharacterStats(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/reset-stats")] HttpRequest req,
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
            DebugLogger.Log($"ResetCharacterStats called for {id}");
            if (character == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            character.Stats.Clear();
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, message = "Stats reset." });
        }

        /// <summary>
        /// Increments the character level by one.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response containing the new level.</returns>
        [FunctionName("LevelUpCharacterSimple")]
        public static async Task<IActionResult> LevelUpCharacterSimple(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/level-up-simple")] HttpRequest req,
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
            DebugLogger.Log($"LevelUpCharacterSimple called for {id}");
            if (character == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            character.Level++;
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, newLevel = character.Level });
        }

        /// <summary>
        /// Validates the character fields for common mistakes.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character loaded from Cosmos DB.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response indicating validation results.</returns>
        [FunctionName("ValidateCharacter")]
        public static async Task<IActionResult> ValidateCharacter(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}/validate")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CloudDragonLib.Models.Character character,
            ILogger log)
        {
            DebugLogger.Log($"ValidateCharacter called for {id}");
            if (character == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Character not found." });
            }

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(character.Name)) errors.Add("Missing name.");
            if (character.Stats == null || character.Stats.Count != 6) errors.Add("Stats must have 6 attributes.");
            if (character.CarriedWeight > 100) errors.Add("Too much weight carried.");

            return new OkObjectResult(new
            {
                success = errors.Count == 0,
                errors
            });
        }

        /// <summary>
        /// Applies a background template to the character.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document.</param>
        /// <param name="backgrounds">Available background records.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response with the applied background.</returns>
        [FunctionName("ApplyBackground")]
        public static async Task<IActionResult> ApplyBackground(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/background")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CloudDragonLib.Models.Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Backgrounds",
                Connection = "CosmosDBConnection")] IEnumerable<dynamic> backgrounds,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CloudDragonLib.Models.Character> characterOut,
            ILogger log)
        {
            DebugLogger.Log($"ApplyBackground called for {id}");
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string backgroundId = input?.backgroundId;

            if (character == null || string.IsNullOrEmpty(backgroundId))
            {
                return new BadRequestObjectResult(new { success = false, error = "Invalid input." });
            }

            var background = backgrounds.FirstOrDefault(b => b.id == backgroundId);
            if (background == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Background not found." });
            }

            character.Background = background.name;
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, background = background.name });
        }

        /// <summary>
        /// Adds a spell item to the character inventory.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response describing the outcome.</returns>
        [FunctionName("AssignSpell")]
        public static async Task<IActionResult> AssignSpell(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/spells/add")] HttpRequest req,
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
            DebugLogger.Log($"AssignSpell called for {id}");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string spellName = input?.spell;

            if (character == null || string.IsNullOrEmpty(spellName))
            {
                return new BadRequestObjectResult(new { success = false, error = "Invalid request." });
            }

            character.Inventory.Add(new Item { Name = spellName, Type = "Spell" });
            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, spell = spellName });
        }

        /// <summary>
        /// Adds a feat to the character and applies any bonuses.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document.</param>
        /// <param name="feats">Available feats.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>HTTP response describing the result.</returns>
        [FunctionName("AddFeat")]
        public static async Task<IActionResult> AddFeat(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/feats/add")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CloudDragonLib.Models.Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Feats",
                Connection = "CosmosDBConnection")] IEnumerable<dynamic> feats,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CloudDragonLib.Models.Character> characterOut,
            ILogger log)
        {
            DebugLogger.Log($"AddFeat called for {id}");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string featId = input?.feat;

            if (character == null || string.IsNullOrEmpty(featId))
            {
                return new BadRequestObjectResult(new { success = false, error = "Invalid request." });
            }

            var feat = feats.FirstOrDefault(f => f.id == featId);
            if (feat == null)
            {
                return new NotFoundObjectResult(new { success = false, error = "Feat not found." });
            }

            character.Inventory.Add(new Item { Name = feat.name, Type = "Feat" });

            if (feat.bonus != null)
            {
                foreach (var bonus in feat.bonus)
                {
                    string stat = bonus.name;
                    int value = bonus.value;
                    if (!character.Stats.ContainsKey(stat))
                        character.Stats[stat] = value;
                    else
                        character.Stats[stat] += value;
                }
            }

            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, feat = feat.name });
        }
    }
}