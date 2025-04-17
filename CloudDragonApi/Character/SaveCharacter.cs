using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib;
using System;
using System.Threading.Tasks;

[FunctionName("SaveCharacter")]
public static async Task<IActionResult> SaveCharacter(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "Characters",
        Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
    ILogger log)
{
    log.LogInformation("SaveCharacter triggered");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    Character newChar;

    try
    {
        newChar = JsonConvert.DeserializeObject<Character>(requestBody);
        if (newChar == null || string.IsNullOrWhiteSpace(newChar.Name))
        {
            return new BadRequestObjectResult(new { success = false, error = "Missing or invalid character data." });
        }

        await characterOut.AddAsync(newChar);
        log.LogInformation("Character saved: {Name} ({Id})", newChar.Name, newChar.Id);

        return new OkObjectResult(new { success = true, id = newChar.Id });
    }
    catch (Exception ex)
    {
        log.LogError(ex, "Failed to save character.");
        return new BadRequestObjectResult(new { success = false, error = ex.Message });
    }
}
