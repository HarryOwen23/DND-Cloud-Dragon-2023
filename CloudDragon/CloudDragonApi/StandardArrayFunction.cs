using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CloudDragonApi
{
    public static class StandardArrayFunction
    {
        [FunctionName("StandardArray")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "standard-array")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("StandardArray endpoint triggered.");
            var stats = CharacterStatsStandardArray.Generate();
            return await Task.FromResult(new OkObjectResult(new { success = true, data = stats }));
        }
    }
}
