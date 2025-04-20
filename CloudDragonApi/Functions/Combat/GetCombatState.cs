[FunctionName("GetCombatState")]
public static IActionResult GetCombatState(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "combat/{id}")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "CombatSessions",
        Connection = "CosmosDBConnection",
        Id = "{id}",
        PartitionKey = "{id}")] CombatSession session,
    string id,
    ILogger log)
{
    if (session == null)
        return new NotFoundObjectResult(new { success = false, error = "Combat session not found." });

    var current = session.Combatants.ElementAtOrDefault(session.TurnIndex);

    return new OkObjectResult(new
    {
        success = true,
        data = session,
        currentTurn = current?.Name,
        round = session.Round
    });
}
