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

/// <summary>
/// Azure Function used to update an existing character.
/// </summary>
public static class UpdateCharacterFunction
{
    /// <summary>
    /// Updates the provided character with any supplied fields.
    /// </summary>
    /// <param name="req">HTTP request containing the partial character payload.</param>
    /// <param name="existingChar">Character loaded from Cosmos DB.</param>
    /// <param name="characterOut">Output binding for persisting updates.</param>
    /// <param name="id">Character identifier.</param>
    /// <param name="log">Function logger.</param>
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
        {
            return new UnauthorizedResult();
        }

        if (existingChar == null)
        {
            log.LogWarning("Character {Id} not found", id);
            DebugLogger.Log($"Character {id} not found for update");
            return new NotFoundObjectResult(new { success = false, error = "Character not found." });
        }

        Character? updates;
        try
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogDebug("Update payload: {Body}", body);
            updates = JsonConvert.DeserializeObject<Character>(body);
            DebugLogger.Log("Parsed update payload");
        }
        catch (JsonException ex)
        {
            log.LogError(ex, "UpdateCharacter failed to parse payload");
            return new BadRequestObjectResult(new { success = false, error = "Invalid character update payload." });
        }

        if (updates == null)
            return new BadRequestObjectResult(new { success = false, error = "Invalid character update payload." });

        // Core identity and class details
        if (!string.IsNullOrWhiteSpace(updates.Name)) existingChar.Name = updates.Name;
        if (!string.IsNullOrWhiteSpace(updates.Race)) existingChar.Race = updates.Race;
        if (!string.IsNullOrWhiteSpace(updates.Class)) existingChar.Class = updates.Class;
        if (!string.IsNullOrWhiteSpace(updates.Subclass)) existingChar.Subclass = updates.Subclass;
        if (updates.Age.HasValue) existingChar.Age = updates.Age;
        if (updates.Level > 0) existingChar.Level = updates.Level;
        if (!string.IsNullOrWhiteSpace(updates.Background)) existingChar.Background = updates.Background;

        // Narrative fields
        if (!string.IsNullOrWhiteSpace(updates.Personality)) existingChar.Personality = updates.Personality;
        if (!string.IsNullOrWhiteSpace(updates.Appearance)) existingChar.Appearance = updates.Appearance;
        if (!string.IsNullOrWhiteSpace(updates.Backstory)) existingChar.Backstory = updates.Backstory;
        if (!string.IsNullOrWhiteSpace(updates.Goals)) existingChar.Goals = updates.Goals;
        if (!string.IsNullOrWhiteSpace(updates.Allies)) existingChar.Allies = updates.Allies;
        if (!string.IsNullOrWhiteSpace(updates.Secrets)) existingChar.Secrets = updates.Secrets;
        if (!string.IsNullOrWhiteSpace(updates.FlavorText)) existingChar.FlavorText = updates.FlavorText;

        // Dictionaries
        if (updates.Stats != null && updates.Stats.Count > 0)
        {
            foreach (var kvp in updates.Stats)
            {
                existingChar.Stats[kvp.Key] = kvp.Value;
            }
        }

        if (updates.Classes != null && updates.Classes.Count > 0)
        {
            foreach (var kvp in updates.Classes)
            {
                existingChar.Classes[kvp.Key] = kvp.Value;
            }
        }

        if (updates.Subclasses != null && updates.Subclasses.Count > 0)
        {
            foreach (var kvp in updates.Subclasses)
            {
                existingChar.Subclasses[kvp.Key] = kvp.Value;
            }
        }

        if (updates.Equipped != null && updates.Equipped.Count > 0)
        {
            foreach (var kvp in updates.Equipped)
            {
                existingChar.Equipped[kvp.Key] = kvp.Value;
            }
        }

        if (updates.Inventory != null)
        {
            existingChar.Inventory = updates.Inventory;
        }

        if (updates.PreparedSpells != null)
        {
            existingChar.PreparedSpells = updates.PreparedSpells;
        }

        if (updates.CastedSpells != null)
        {
            existingChar.CastedSpells = updates.CastedSpells;
        }

        if (updates.SpellSlots != null && updates.SpellSlots.Count > 0)
        {
            foreach (var kvp in updates.SpellSlots)
            {
                existingChar.SpellSlots[kvp.Key] = kvp.Value;
            }
        }

        // AC and weight
        if (updates.AC > 0) existingChar.AC = updates.AC;
        if (updates.CarriedWeight > 0) existingChar.CarriedWeight = updates.CarriedWeight;

        await characterOut.AddAsync(existingChar);
        log.LogInformation("Character {Id} updated", existingChar.Id);

        return new OkObjectResult(new { success = true, updated = existingChar });
    }
}