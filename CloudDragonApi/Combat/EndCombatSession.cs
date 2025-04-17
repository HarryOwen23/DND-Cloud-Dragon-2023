[FunctionName("EndCombatSession")]
public static async Task<IActionResult> EndCombatSession(
    [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "combat/{id}")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "CombatSessions",
        Connection = "CosmosDBConnection",
        Id = "{id}",
        PartitionKey = "{id}")] CombatSession session,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "CombatSessions",
        Connection = "CosmosDBConnection")] IAsyncCollector<CombatSession> sessionOut,
    string id,
    ILogger log)
{
    if (session == null)
        return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

    // Soft delete: flag or rename it
    session.Name += " (ENDED)";
    session.Combatants.ForEach(c => c.Conditions.Add("Archived"));

    await sessionOut.AddAsync(session);

    return new OkObjectResult(new
    {
        success = true,
        message = "Combat session marked as ended.",
        sessionId = session.Id
    });
}
