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

        Character updates;
        try
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogDebug("Update payload: {Body}", body);
            updates = JsonConvert.DeserializeObject<Character>(body);
        }
        catch (JsonException ex)
        {
            log.LogError(ex, "UpdateCharacter failed to parse payload");
            return new BadRequestObjectResult(new { success = false, error = "Invalid character update payload." });
        }
        DebugLogger.Log("Parsed update payload");

        if (updates == null)
            return new BadRequestObjectResult(new { success = false, error = "Invalid character update payload." });

        // Update only the fields that are included
        existingChar.Name = updates.Name ?? existingChar.Name;
        existingChar.Class = updates.Class ?? existingChar.Class;
        existingChar.Race = updates.Race ?? existingChar.Race;
        existingChar.Level = updates.Level > 0 ? updates.Level : existingChar.Level;

        if (updates.Stats != null)
            foreach (var kvp in updates.Stats)
                existingChar.Stats[kvp.Key] = kvp.Value;

        if (updates.Classes != null)
            foreach (var kvp in updates.Classes)
                existingChar.Classes[kvp.Key] = kvp.Value;

        if (updates.Subclasses != null)
            foreach (var kvp in updates.Subclasses)
                existingChar.Subclasses[kvp.Key] = kvp.Value;

        if (updates.Equipped != null)
            foreach (var kvp in updates.Equipped)
                existingChar.Equipped[kvp.Key] = kvp.Value;

        if (updates.SpellSlots != null)
            foreach (var kvp in updates.SpellSlots)
                existingChar.SpellSlots[kvp.Key] = kvp.Value;

        if (updates.Inventory != null)
            existingChar.Inventory = updates.Inventory;

        if (updates.PreparedSpells != null)
            existingChar.PreparedSpells = updates.PreparedSpells;

        if (updates.CastedSpells != null)
            existingChar.CastedSpells = updates.CastedSpells;

        await characterOut.AddAsync(existingChar);
        log.LogInformation("Character {Id} updated", existingChar.Id);
        DebugLogger.Log($"Character {existingChar.Id} updated");

        return new OkObjectResult(new { success = true, updated = existingChar });
    }
}
}
