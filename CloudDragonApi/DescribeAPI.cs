[FunctionName("DescribeApi")]
public static IActionResult DescribeApi(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "describe")] HttpRequest req,
    ILogger log)
{
    return new OkObjectResult(new
    {
        success = true,
        endpoints = new[]
        {
            "POST /character",
            "GET /character/{id}",
            "PATCH /character/{id}",
            "POST /combat",
            "POST /combat/{id}/advance",
            "POST /character/{id}/inventory",
            "GET /conditions"
        }
    });
}
