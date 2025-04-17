[FunctionName("GetConditions")]
public static IActionResult GetConditions(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "conditions")] HttpRequest req,
    ILogger log)
{
    var conditions = new List<Condition>
    {
        new() { Name = "Blinded", Effect = "Cannot see. Disadvantage on attack rolls.", EndsOnTurnEnd = false },
        new() { Name = "Poisoned", Effect = "Disadvantage on attack rolls and ability checks.", EndsOnTurnEnd = false },
        new() { Name = "Stunned", Effect = "Incapacitated. Can’t move. Fails STR/DEX saves.", EndsOnTurnEnd = true }
    };

    return new OkObjectResult(new { success = true, data = conditions });
}
