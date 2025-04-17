[FunctionName("LoadMockCharacters")]
public static async Task<IActionResult> LoadMockCharacters(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "dev/mock-characters")] HttpRequest req,
    [CosmosDB("CloudDragonDB", "Characters", Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
    ILogger log)
{
    var mockChars = new[]
    {
        new Character { Name = "Mock Knight", Class = "Fighter", Race = "Human", Level = 2, Stats = new() { ["STR"] = 16, ["DEX"] = 12 } },
        new Character { Name = "Test Mage", Class = "Wizard", Race = "Elf", Level = 1, Stats = new() { ["INT"] = 17, ["WIS"] = 10 } }
    };

    foreach (var c in mockChars) await characterOut.AddAsync(c);

    return new OkObjectResult(new { success = true, count = mockChars.Length });
}
