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
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function that creates a new character document in Cosmos DB.
    /// </summary>
    public static class CreateCharacterFunction
    {
        /// <summary>
        /// Creates a character from the posted JSON payload.
        /// </summary>
        /// <param name="req">HTTP request containing the character JSON.</param>
        /// <param name="characterOut">Cosmos DB output binding.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Action result describing the outcome.</returns>
        [FunctionName("CreateCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character")] HttpRequest req,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            ILogger log)
        {
            log.LogInformation("CreateCharacter endpoint hit");
            DebugLogger.Log("CreateCharacter invoked");

            // Validate API key if required
            var expected = Environment.GetEnvironmentVariable("API_KEY");
            if (!string.IsNullOrEmpty(expected) && (!req.Headers.TryGetValues("x-api-key", out var values) || values.FirstOrDefault() != expected))
            {
                return new UnauthorizedResult();
            }

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            CharacterModel? character;
            try
            {
                character = string.IsNullOrWhiteSpace(body)
                    ? null
                    : Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterModel>(body);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                log.LogError(ex, "Failed to parse character payload");
                character = null;
            }
            log.LogInformation("Character payload parsed");

            if (character == null)
            {
                return new BadRequestObjectResult(new { success = false, error = "Character payload is missing or invalid." });
            }

            if (string.IsNullOrWhiteSpace(character.Name))
            {
                return new BadRequestObjectResult(new { success = false, error = "Character name is required." });
            }

            if (character.Stats == null || character.Stats.Count == 0)
            {
                return new BadRequestObjectResult(new { success = false, error = "Character stats are required." });
            }

            var requiredStats = new[] { "STR", "DEX", "CON", "INT", "WIS", "CHA" };
            bool hasAllStats = requiredStats.All(stat =>
                character.Stats != null &&
                character.Stats.ContainsKey(stat) &&
                character.Stats[stat] >= 1 && character.Stats[stat] <= 20
             );

            if (!hasAllStats)
            {
                return new BadRequestObjectResult(new
                {
                    success = false,
                    error = "Character must have valid stats for STR, DEX, CON, INT, WIS, and CHA (1-20)."
                });
            }

            await characterOut.AddAsync(character);
            log.LogInformation("Character {Id} created", character.Id);
            DebugLogger.Log($"Character {character.Id} saved");

            return new CreatedResult(string.Empty, new { success = true, id = character.Id });
        }
    }
}