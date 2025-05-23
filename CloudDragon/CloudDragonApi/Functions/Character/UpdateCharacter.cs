using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

public static class UpdateCharacterFunction
{
    [FunctionName("UpdateCharacter")]
    public static async Task<IActionResult> UpdateCharacter(
        [HttpTrigger(AuthorizationLevel.Function, "put", "patch", Route = "character/{id}")] HttpRequest req,
        [CosmosDB(
            databaseName: "CloudDragonDB",
            containerName: "Characters",
            Connection = "CosmosDBConnection",
            Id = "{id}",
            PartitionKey = "{id}")] Character existingChar,
        [CosmosDB(
            databaseName: "CloudDragonDB",
            containerName: "Characters",
            Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
        string id,
        ILogger log)
    {
        if (existingChar == null)
            return new NotFoundObjectResult(new { success = false, error = "Character not found." });

        string body = await new StreamReader(req.Body).ReadToEndAsync();
        var updates = JsonConvert.DeserializeObject<Character>(body);

        if (updates == null)
            return new BadRequestObjectResult(new { success = false, error = "Invalid character update payload." });

        // Update only the fields that are included
        existingChar.Name = updates.Name ?? existingChar.Name;
        existingChar.Class = updates.Class ?? existingChar.Class;
        existingChar.Race = updates.Race ?? existingChar.Race;
        existingChar.Level = updates.Level > 0 ? updates.Level : existingChar.Level;
        existingChar.Stats = updates.Stats?.Count > 0 ? updates.Stats : existingChar.Stats;

        await characterOut.AddAsync(existingChar);

        return new OkObjectResult(new { success = true, updated = existingChar });
    }
}
