using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; 
using CloudDragonLib.Models;

namespace CloudDragonApi.Functions.Character
{
    public static class CreateCharacterFunction
    {
        [FunctionName("CreateCharacter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Characters",
                Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var character = JsonConvert.DeserializeObject<Character>(requestBody);

            if (character == null || string.IsNullOrWhiteSpace(character.Name))
                return new BadRequestObjectResult(new { success = false, error = "Invalid character data." });

            await characterOut.AddAsync(character);

            return new OkObjectResult(new { success = true, id = character.Id });
        }
    }
}
