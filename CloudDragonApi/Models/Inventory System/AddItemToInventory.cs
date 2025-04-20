[FunctionName("AddItemToInventory")]
public static async Task<IActionResult> AddItemToInventory(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "character/{id}/inventory")] HttpRequest req,
    [CosmosDB("CloudDragonDB", "Characters", Connection = "CosmosDBConnection", Id = "{id}", PartitionKey = "{id}")] Character character,
    [CosmosDB("CloudDragonDB", "Characters", Connection = "CosmosDBConnection")] IAsyncCollector<Character> characterOut,
    string id,
    ILogger log)
{
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var item = JsonConvert.DeserializeObject<Item>(body);

    character.Inventory ??= new List<Item>();
    character.Inventory.Add(item);

    await characterOut.AddAsync(character);

    return new OkObjectResult(new { success = true, added = item });
}
