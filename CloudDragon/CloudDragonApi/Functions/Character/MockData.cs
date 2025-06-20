using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class LoadMockCharactersFunction
    {
        [FunctionName("LoadMockCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "dev/mock-characters")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Characters",
                ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
            ILogger log)
        {
            var mockChars = new[]
            {
                new CharacterModel { Name = "Mock Knight", Class = "Fighter", Race = "Human", Level = 2, Stats = new() { ["STR"] = 16, ["DEX"] = 12 } },
                new CharacterModel { Name = "Test Mage", Class = "Wizard", Race = "Elf", Level = 1, Stats = new() { ["INT"] = 17, ["WIS"] = 10 } }
            };

            foreach (var c in mockChars)
            {
                await characterOut.AddAsync(c);
            }

            return new OkObjectResult(new { success = true, count = mockChars.Length });
        }
    }
}
