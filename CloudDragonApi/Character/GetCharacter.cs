using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib;
using System;
using System.Threading.Tasks;

[FunctionName("GetCharacter")]
public static IActionResult GetCharacter(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "character/{id}")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "Characters",
        Connection = "CosmosDBConnection",
        Id = "{id}",
        PartitionKey = "{id}")] Character character,
    string id,
    ILogger log)
{
    log.LogInformation("GetCharacter triggered for ID: {Id}", id);

    if (character == null)
    {
        return new NotFoundObjectResult(new { success = false, error = $"Character with ID '{id}' not found." });
    }

    return new OkObjectResult(new { success = true, data = character });
}
