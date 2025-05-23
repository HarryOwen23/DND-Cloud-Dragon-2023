using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudDragonApi
{
    public static class HealthCheckFunction
    {
        [FunctionName("HealthCheck")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "health")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HealthCheck triggered.");

            return new OkObjectResult(new
            {
                success = true,
                message = "CloudDragon API is up and running!",
                version = "1.0.0", // You can increment this
                timestamp = DateTime.UtcNow
            });
        }
    }
}