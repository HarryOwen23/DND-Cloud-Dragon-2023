using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Threading.Tasks;
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
