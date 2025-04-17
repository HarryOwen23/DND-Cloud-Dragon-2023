using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib;
using System;
using System.Threading.Tasks;

[FunctionName("DeleteCharacter")]
public static async Task<IActionResult> DeleteCharacter(
    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "character/{id}")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "Characters",
        Connection = "CosmosDBConnection",
        Id = "{id}",
        PartitionKey = "{id}")] Character characterToDelete,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "Characters",
        Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
    string id,
    ILogger log)
{
    if (characterToDelete == null)
        return new NotFoundObjectResult(new { success = false, error = "Character not found." });

    // Soft delete by setting a flag (safer for audit/history)
    characterToDelete.Name += " (DELETED)";
    characterToDelete.Level = -1;

    await characterOut.AddAsync(characterToDelete);
    return new OkObjectResult(new { success = true, message = "Character deleted (soft delete)." });
}
