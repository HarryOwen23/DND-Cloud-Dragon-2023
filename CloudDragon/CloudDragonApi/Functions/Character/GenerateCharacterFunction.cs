using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function for generating a character and persisting it using the <see cref="CharacterContextEngine"/>.
    /// </summary>
    public class GenerateCharacterFunction
    {
        private readonly CharacterContextEngine _engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCharacterFunction"/> class.
        /// </summary>
        /// <param name="engine">Engine used to fill in character details.</param>
        public GenerateCharacterFunction(CharacterContextEngine engine)
        {
            _engine = engine;
        }

        /// <summary>
        /// HTTP entry point for character generation.
        /// </summary>
        /// <param name="req">HTTP request containing the character payload.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>Action result with generated character information.</returns>
        [FunctionName("GenerateCharacter")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/generate")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GenerateCharacter endpoint hit.");
            DebugLogger.Log("Parsing character payload");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var character = JsonConvert.DeserializeObject<Character>(requestBody);
            DebugLogger.Log("Character payload parsed");

            if (character == null || string.IsNullOrWhiteSpace(character.Name))
            {
                return new BadRequestObjectResult(new { success = false, error = "Invalid character input." });
            }

            DebugLogger.Log($"Generating character '{character.Name}'");
            var result = await _engine.BuildAndStoreCharacterAsync(character);
            DebugLogger.Log($"Character '{result.Name}' stored successfully");

            return new OkObjectResult(new
            {
                success = true,
                character = result
            });
        }
    }
}
