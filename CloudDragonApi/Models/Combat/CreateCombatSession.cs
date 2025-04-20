[FunctionName("CreateCombatSession")]
public static async Task<IActionResult> CreateCombatSession(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "CombatSessions",
        Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
    ILogger log)
{
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    CombatSession session;

    try
    {
        session = JsonConvert.DeserializeObject<CombatSession>(body);
        if (session == null || string.IsNullOrWhiteSpace(session.Name))
            return new BadRequestObjectResult(new { success = false, error = "Invalid session data." });

        // Roll initiative for each combatant
        var rng = new Random();
        session.Combatants.ForEach(c =>
        {
            c.Initiative = rng.Next(1, 21) + c.InitiativeModifier;
        });

        // Sort combatants
        session.Combatants = session.Combatants
            .OrderByDescending(c => c.Initiative)
            .ToList();

        await sessionOut.AddAsync(session);
        return new OkObjectResult(new { success = true, id = session.Id });
    }
    catch (Exception ex)
    {
        log.LogError(ex, "Error creating combat session.");
        return new BadRequestObjectResult(new { success = false, error = ex.Message });
    }
}
