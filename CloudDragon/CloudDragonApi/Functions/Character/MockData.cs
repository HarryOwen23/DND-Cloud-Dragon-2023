using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class LoadMockCharactersFunction
    {
        [FunctionName("LoadMockCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "dev/mock-characters")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            ILogger log)
        {
            var mockChars = new[]
            {
                new Character { Name = "Mock Knight", Class = "Fighter", Race = "Human", Level = 2, Stats = new() { ["STR"] = 16, ["DEX"] = 12 } },
                new Character { Name = "Test Mage", Class = "Wizard", Race = "Elf", Level = 1, Stats = new() { ["INT"] = 17, ["WIS"] = 10 } }
            };

            foreach (var c in mockChars)
            {
                await characterOut.AddAsync(c);
            }

            return new OkObjectResult(new { success = true, count = mockChars.Length });
        }
    }
}
