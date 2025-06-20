using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
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
        /// <param name="context">Function execution context for logging.</param>
        /// <returns>A <see cref="HttpResponseData"/> describing the outcome.</returns>
        [Function("CreateCharacter")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character")] HttpRequestData req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            FunctionContext context)
        {
            var log = context.GetLogger(nameof(CreateCharacter));
            log.LogInformation("CreateCharacter endpoint hit");
            DebugLogger.Log("CreateCharacter invoked");

            // Validate API key if required
            var expected = Environment.GetEnvironmentVariable("API_KEY");
            if (!string.IsNullOrEmpty(expected) && (!req.Headers.TryGetValues("x-api-key", out var values) || values.FirstOrDefault() != expected))
            {
                var unauthorized = req.CreateResponse(HttpStatusCode.Unauthorized);
                await unauthorized.WriteAsJsonAsync(new { success = false, error = "Unauthorized" });
                return unauthorized;
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

            var response = req.CreateResponse();

            if (character == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Character payload is missing or invalid." });
                return response;
            }

            if (string.IsNullOrWhiteSpace(character.Name))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Character name is required." });
                return response;
            }

            if (character.Stats == null || character.Stats.Count == 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { success = false, error = "Character stats are required." });
                return response;
            }

            var requiredStats = new[] { "STR", "DEX", "CON", "INT", "WIS", "CHA" };
            bool hasAllStats = requiredStats.All(stat =>
                character.Stats != null &&
                character.Stats.ContainsKey(stat) &&
                character.Stats[stat] >= 1 && character.Stats[stat] <= 20
             );

            if (!hasAllStats)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new
                {
                    success = false,
                    error = "Character must have valid stats for STR, DEX, CON, INT, WIS, and CHA (1-20)."
                });
                return response;
            }

            await characterOut.AddAsync(character);
            log.LogInformation("Character {Id} created", character.Id);
            DebugLogger.Log($"Character {character.Id} saved");

            response.StatusCode = HttpStatusCode.Created;
            await response.WriteAsJsonAsync(new { success = true, id = character.Id });
            return response;
        }
    }
}