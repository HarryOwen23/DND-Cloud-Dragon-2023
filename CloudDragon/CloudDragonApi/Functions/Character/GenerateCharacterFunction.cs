using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

namespace CloudDragonApi.Character
{
    public class GenerateCharacterFunction
    {
        private readonly CharacterContextEngine _engine;

        public GenerateCharacterFunction(CharacterContextEngine engine)
        {
            _engine = engine;
        }

        [FunctionName("GenerateCharacter")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/generate")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GenerateCharacter endpoint hit.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var character = JsonConvert.DeserializeObject<Character>(requestBody);

            if (character == null || string.IsNullOrWhiteSpace(character.Name))
            {
                return new BadRequestObjectResult(new { success = false, error = "Invalid character input." });
            }

            var result = await _engine.BuildAndStoreCharacterAsync(character);

            return new OkObjectResult(new
            {
                success = true,
                character = result
            });
        }
    }
}
