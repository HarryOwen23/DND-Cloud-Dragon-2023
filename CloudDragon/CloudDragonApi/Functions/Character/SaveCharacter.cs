using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using System.Threading.Tasks;
using CloudDragon.CloudDragonApi.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    public static class SaveCharacterFunction
    {
    [FunctionName("SaveCharacter")]
    public static async Task<IActionResult> SaveCharacter(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character")] HttpRequest req,
        [CosmosDB(
            databaseName: "CloudDragonDB",
            containerName: "Characters",
            Connection = "CosmosDBConnection")] IAsyncCollector<CharacterModel> characterOut,
        ILogger log)
    {
        log.LogInformation("SaveCharacter triggered");

        if (!ApiRequestHelper.IsAuthorized(req, log))
        {
            return new UnauthorizedResult();
        }

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        CharacterModel newChar;

        try
        {
            newChar = JsonConvert.DeserializeObject<CharacterModel>(requestBody);

            if (newChar == null || string.IsNullOrWhiteSpace(newChar.Name))
            {
                return new BadRequestObjectResult(new { success = false, error = "Missing or invalid character data." });
            }

            InitializeSpellSlots(newChar);

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

    private static void InitializeSpellSlots(Character character)
    {
        var fullCasters = new List<string> { "wizard", "cleric", "druid", "bard", "sorcerer", "warlock" };
        var halfCasters = new List<string> { "paladin", "ranger", "artificer" };
        var thirdCasters = new List<string> { "fighter", "rogue" };

        string charClass = character.Class?.ToLower() ?? "";

        if (string.IsNullOrEmpty(charClass))
            return;

        int effectiveLevel = character.Level;

        if (halfCasters.Contains(charClass))
            effectiveLevel = character.Level / 2;
        else if (thirdCasters.Contains(charClass))
            effectiveLevel = character.Level / 3;

        character.SpellSlots = new Dictionary<int, int>();

        if (effectiveLevel >= 1) character.SpellSlots[1] = 2;
        if (effectiveLevel >= 3) character.SpellSlots[2] = 2;
        if (effectiveLevel >= 5) character.SpellSlots[3] = 2;
        if (effectiveLevel >= 7) character.SpellSlots[4] = 1;
        if (effectiveLevel >= 9) character.SpellSlots[5] = 1;
    }
}

}
