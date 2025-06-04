using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi.Data; // Assuming RacesPopulator lives here
using CloudDragonApi.Models; // Assuming
using CloudDragonApi;
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

            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json"); // fileName ignored in current implementation

            log.LogInformation("Returning {Count} races", races.Count);
            return new OkObjectResult(new { success = true, races });
        }
    }
}
