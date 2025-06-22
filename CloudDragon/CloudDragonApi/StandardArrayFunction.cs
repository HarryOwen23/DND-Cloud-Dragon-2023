using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi
{
    /// <summary>
    /// Azure Function that returns the 5e standard array of ability scores.
    /// </summary>
    public static class StandardArrayFunction
    {
        /// <summary>
        /// HTTP entry point for retrieving the standard array.
        /// </summary>
        /// <param name="req">Incoming HTTP request.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>The standard array wrapped in an <see cref="IActionResult"/>.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("StandardArray")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "standard-array")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("StandardArray endpoint triggered.");
            DebugLogger.Log("StandardArray endpoint hit");
            if (!ApiRequestHelper.IsAuthorized(req, log))
            {
                return new UnauthorizedResult();
            }
            var stats = CharacterStatsStandardArray.Generate();
            DebugLogger.Log("Returning standard array values");
            return await Task.FromResult(new OkObjectResult(new { success = true, data = stats }));
        }
    }
}
