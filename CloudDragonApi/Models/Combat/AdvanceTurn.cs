[FunctionName("AdvanceTurn")]
public static async Task<IActionResult> AdvanceTurn(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "combat/{id}/advance")] HttpRequest req,
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
        return new NotFoundObjectResult(new { success = false, error = "Session not found." });

    session.TurnIndex++;
    if (session.TurnIndex >= session.Combatants.Count)
    {
        session.TurnIndex = 0;
        session.Round++;
    }

    await sessionOut.AddAsync(session);
    return new OkObjectResult(new
    {
        success = true,
        nextTurn = session.Combatants[session.TurnIndex].Name,
        round = session.Round
    });

    session.Log.Add($"Turn {session.TurnIndex + 1}: {currentCombatant.Name}'s turn began.");
}
