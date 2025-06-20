using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using CloudDragon.CloudDragonApi.Utils;
using Microsoft.Extensions.Logging;

namespace CloudDragon.CloudDragonApi.Functions
{
    public static class DescribeApiFunction
    {
        [FunctionName("DescribeApi")]
        /// <summary>
        /// Returns a JSON object describing the available CloudDragon API endpoints.
        /// </summary>
        /// <param name="req">The incoming HTTP request.</param>
        /// <param name="context">The current function execution context.</param>
        /// <returns>The HTTP response containing the list of endpoints.</returns>
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "describe")] HttpRequest req,
            ILogger logger)
        {
            DebugLogger.Log("Describe API endpoint hit");

            var result = new
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
            };

            DebugLogger.Log("Returning list of endpoints");
            return new OkObjectResult(result);
        }
    }
}