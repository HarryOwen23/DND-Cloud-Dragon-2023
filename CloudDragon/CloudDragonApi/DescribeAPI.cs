using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions
{
    public static class DescribeApiFunction
    {
        [FunctionName("DescribeApi")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "describe")] HttpRequest req,
            ILogger log)
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
