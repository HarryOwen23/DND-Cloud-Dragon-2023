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
using CloudDragonApi;
using CloudDragonApi.Utils;

namespace CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Azure Function used to update an existing character document.
    /// </summary>
    public static class UpdateCharacterFunction
    {
        /// <summary>
    /// Updates the provided character with any supplied fields.
    /// </summary>
    /// <param name="req">HTTP request containing the partial character payload.</param>
    /// <param name="existingChar">Character document loaded from Cosmos DB.</param>
    /// <param name="characterOut">Output binding for persisting updates.</param>
    /// <param name="id">Character identifier.</param>
    /// <param name="log">Function logger.</param>
    /// <returns>Action result with the updated character.</returns>
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
        log.LogRequestDetails(req, nameof(UpdateCharacter));
        DebugLogger.Log($"UpdateCharacter called for {id}");

        if (!ApiRequestHelper.IsAuthorized(req, log))
            return new UnauthorizedResult();

        if (existingChar == null)
        {
            log.LogWarning("Character {Id} not found", id);
            DebugLogger.Log($"Character {id} not found for update");
            return new NotFoundObjectResult(new { success = false, error = "Character not found." });
        }

        string body = await new StreamReader(req.Body).ReadToEndAsync();
        log.LogDebug("Update payload: {Body}", body);
        var updates = JsonConvert.DeserializeObject<Character>(body);
        DebugLogger.Log("Parsed update payload");

        if (updates == null)
            return new BadRequestObjectResult(new { success = false, error = "Invalid character update payload." });

        // Update only the fields that are included
        existingChar.Name = updates.Name ?? existingChar.Name;
        existingChar.Class = updates.Class ?? existingChar.Class;
        existingChar.Race = updates.Race ?? existingChar.Race;
        existingChar.Level = updates.Level > 0 ? updates.Level : existingChar.Level;
        existingChar.Stats = updates.Stats?.Count > 0 ? updates.Stats : existingChar.Stats;

        await characterOut.AddAsync(existingChar);
        log.LogInformation("Character {Id} updated", existingChar.Id);
        DebugLogger.Log($"Character {existingChar.Id} updated");

        return new OkObjectResult(new { success = true, updated = existingChar });
    }
}
}
