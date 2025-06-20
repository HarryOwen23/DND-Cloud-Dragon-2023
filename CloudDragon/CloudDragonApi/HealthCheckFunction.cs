using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi
{
    /// <summary>
    /// Simple health check endpoint to verify the API is running.
    /// </summary>
    public static class HealthCheckFunction
    {
        /// <summary>
        /// Responds with a timestamp and version string.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <param name="log">Function logger.</param>
        [FunctionName("HealthCheck")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "health")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HealthCheck triggered.");
            DebugLogger.Log("Health check endpoint hit");
            var result = new
            {
                success = true,
                message = "CloudDragon API is up and running!",
                version = "1.0.0", // You can increment this
                timestamp = DateTime.UtcNow
            };
            DebugLogger.Log("Health check returning OK result");
            return new OkObjectResult(result);
        }
    }
}
