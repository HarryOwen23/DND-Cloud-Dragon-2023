using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonLib.Models;
using CloudDragonApi.Utils;
namespace CloudDragonApi.Races
{
    public static class GetAllRaces
    {
        [FunctionName("GetAllRaces")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "races")] HttpRequest req,
            ILogger log)
        {
            log.LogRequestDetails(req, nameof(GetAllRaces));
            DebugLogger.Log("GetAllRaces called");

            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json"); // fileName ignored in current implementation
            DebugLogger.Log($"Loaded {races.Count} races");

            log.LogInformation("Returning {Count} races", races.Count);
            return new OkObjectResult(new { success = true, races });
        }
    }
}
