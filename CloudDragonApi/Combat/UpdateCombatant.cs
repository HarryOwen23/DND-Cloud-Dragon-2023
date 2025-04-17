[FunctionName("UpdateCombatant")]
public static async Task<IActionResult> UpdateCombatant(
    [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "combat/{sessionId}/combatant/{name}")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "CombatSessions",
        Connection = "CosmosDBConnection",
        Id = "{sessionId}",
        PartitionKey = "{sessionId}")] CombatSession session,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "CombatSessions",
        Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
    string sessionId,
    string name,
    ILogger log)
{
    if (session == null)
        return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

    var combatant = session.Combatants.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
    if (combatant == null)
        return new NotFoundObjectResult(new { success = false, error = $"Combatant '{name}' not found in session." });

    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var update = JsonConvert.DeserializeObject<Combatant>(body);

    if (update.HP != 0) combatant.HP = update.HP;
    if (update.AC != 0) combatant.AC = update.AC;
    if (update.Conditions != null && update.Conditions.Any())
        combatant.Conditions = update.Conditions;

    await sessionOut.AddAsync(session);

    return new OkObjectResult(new
    {
        success = true,
        updated = combatant
    });
}
