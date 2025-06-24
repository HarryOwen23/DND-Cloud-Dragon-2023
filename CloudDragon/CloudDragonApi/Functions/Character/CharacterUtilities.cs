using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Provides utility functions for managing character-related operations.
    /// </summary>
    public static class CharacterUtilities
    {
        /// <summary>
        /// Resets all ability stats for a character.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">The unique character ID.</param>
        /// <param name="character">The character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist changes.</param>
        /// <param name="log">Logger for diagnostic information.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [FunctionName("ResetCharacterStats")]
        public static async Task<IActionResult> ResetCharacterStats(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/reset-stats")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                connection: "CosmosDBConnection",
                id: "{id}",
                partitionKey: "{id}")] CloudDragonLib.Models.Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                connection: "CosmosDBConnection")] IAsyncCollector<CloudDragonLib.Models.Character> characterOut,
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
        /// Increases a character's level by one.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">The unique character ID.</param>
        /// <param name="character">The character document from Cosmos DB.</param>
        /// <param name="characterOut">Output binding to persist changes.</param>
        /// <param name="log">Logger for diagnostic information.</param>
        /// <returns>HTTP response indicating the new level or an error.</returns>
        [FunctionName("LevelUpCharacterSimple")]
        public static async Task<IActionResult> LevelUpCharacterSimple(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/level-up-simple")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                connection: "CosmosDBConnection",
                id: "{id}",
                partitionKey: "{id}")] CloudDragonLib.Models.Character character,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                connection: "CosmosDBConnection")] IAsyncCollector<CloudDragonLib.Models.Character> characterOut,
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
        /// Validates a character's properties for common issues.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="id">The unique character ID.</param>
        /// <param name="character">The character document from Cosmos DB.</param>
        /// <param name="log">Logger for diagnostic information.</param>
        /// <returns>HTTP response indicating validation results.</returns>
        [FunctionName("ValidateCharacter")]
        public static async Task<IActionResult> ValidateCharacter(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}/validate")] HttpRequest req,
            string id,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                connection: "CosmosDBConnection",
                id: "{id}",
                partitionKey: "{id}")] CloudDragonLib.Models.Character character,
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
    }
}
