using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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
        /// <param name="context">Execution context.</param>
        /// <returns>HTTP response describing the result.</returns>
        [Function("ResetCharacterStats")]
        public static async Task<HttpResponseData> ResetCharacterStats(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/reset-stats")] HttpRequestData req,
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
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(ResetCharacterStats));
            DebugLogger.Log($"ResetCharacterStats called for {id}");
            var response = req.CreateResponse();

            if (character == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
            }

            character.Stats.Clear();
            await characterOut.AddAsync(character);

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, message = "Stats reset." });
            return response;
        }

        /// <summary>
        /// Increments the character level by one.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="context">Execution context.</param>
        /// <returns>HTTP response containing the new level.</returns>
        [Function("LevelUpCharacterSimple")]
        public static async Task<HttpResponseData> LevelUpCharacterSimple(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/level-up-simple")] HttpRequestData req,
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
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(LevelUpCharacterSimple));
            DebugLogger.Log($"LevelUpCharacterSimple called for {id}");
            var response = req.CreateResponse();

            if (character == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
            }

            character.Level++;
            await characterOut.AddAsync(character);

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, newLevel = character.Level });
            return response;
        }

        /// <summary>
        /// Validates the character fields for common mistakes.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character loaded from Cosmos DB.</param>
        /// <param name="context">Execution context.</param>
        /// <returns>HTTP response indicating validation results.</returns>
        [Function("ValidateCharacter")]
        public static async Task<HttpResponseData> ValidateCharacter(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}/validate")] HttpRequestData req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection",
                Id = "{id}",
                PartitionKey = "{id}")] CloudDragonLib.Models.Character character,
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(ValidateCharacter));
            DebugLogger.Log($"ValidateCharacter called for {id}");
            var response = req.CreateResponse();

            if (character == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Character not found." });
                return response;
            }

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(character.Name)) errors.Add("Missing name.");
            if (character.Stats == null || character.Stats.Count != 6) errors.Add("Stats must have 6 attributes.");
            if (character.CarriedWeight > 100) errors.Add("Too much weight carried.");

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new
            {
                success = errors.Count == 0,
                errors
            });
            return response;
        }

        /// <summary>
        /// Applies a background template to the character.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document.</param>
        /// <param name="backgrounds">Available background records.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="context">Execution context.</param>
        /// <returns>HTTP response with the applied background.</returns>
        [Function("ApplyBackground")]
        public static async Task<HttpResponseData> ApplyBackground(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/background")] HttpRequestData req,
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
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(ApplyBackground));
            DebugLogger.Log($"ApplyBackground called for {id}");
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string backgroundId = input?.backgroundId;

            var response = req.CreateResponse();

            if (character == null || string.IsNullOrEmpty(backgroundId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Invalid input." });
                return response;
            }

            var background = backgrounds.FirstOrDefault(b => b.id == backgroundId);
            if (background == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Background not found." });
                return response;
            }

            character.Background = background.name;
            await characterOut.AddAsync(character);

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, background = background.name });
            return response;
        }

        /// <summary>
        /// Adds a spell item to the character inventory.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="context">Execution context.</param>
        /// <returns>HTTP response describing the outcome.</returns>
        [Function("AssignSpell")]
        public static async Task<HttpResponseData> AssignSpell(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/spells/add")] HttpRequestData req,
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
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(AssignSpell));
            DebugLogger.Log($"AssignSpell called for {id}");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string spellName = input?.spell;

            var response = req.CreateResponse();

            if (character == null || string.IsNullOrEmpty(spellName))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Invalid request." });
                return response;
            }

            character.Inventory.Add(new Item { Name = spellName, Type = "Spell" });
            await characterOut.AddAsync(character);

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, spell = spellName });
            return response;
        }

        /// <summary>
        /// Adds a feat to the character and applies any bonuses.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">Character identifier.</param>
        /// <param name="character">Character document.</param>
        /// <param name="feats">Available feats.</param>
        /// <param name="characterOut">Output binding for persistence.</param>
        /// <param name="context">Execution context.</param>
        /// <returns>HTTP response describing the result.</returns>
        [Function("AddFeat")]
        public static async Task<HttpResponseData> AddFeat(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/feats/add")] HttpRequestData req,
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
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(AddFeat));
            DebugLogger.Log($"AddFeat called for {id}");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic input = JsonConvert.DeserializeObject(body);
            string featId = input?.feat;

            var response = req.CreateResponse();

            if (character == null || string.IsNullOrEmpty(featId))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Invalid request." });
                return response;
            }

            var feat = feats.FirstOrDefault(f => f.id == featId);
            if (feat == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                await response.WriteAsJsonAsync(new { success = false, error = "Feat not found." });
                return response;
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

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteAsJsonAsync(new { success = true, feat = feat.name });
            return response;
        }
    }
}