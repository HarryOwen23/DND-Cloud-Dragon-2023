using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Threading.Tasks;
using CloudDragon.CloudDragonApi.Utils;
using Microsoft.Extensions.Logging;

namespace CloudDragon.CloudDragonApi.Functions
{
    public static class DescribeApiFunction
    {
        [Function("DescribeApi")]
        /// <summary>
        /// Returns a JSON object describing the available CloudDragon API endpoints.
        /// </summary>
        /// <param name="req">The incoming HTTP request.</param>
        /// <param name="context">The current function execution context.</param>
        /// <returns>The HTTP response containing the list of endpoints.</returns>
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "describe")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger(nameof(DescribeApiFunction));
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

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(result);
            DebugLogger.Log("Returning list of endpoints");
            return response;
        }
    }
}