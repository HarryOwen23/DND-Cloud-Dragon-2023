import azure.functions as func
import logging

app = func.FunctionApp()

[Function("HttpFunction")]
public IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
{
    return new OkObjectResult($"Welcome to Azure Functions, {req.Query["name"]}!");
}