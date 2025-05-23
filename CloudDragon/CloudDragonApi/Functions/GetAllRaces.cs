using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CloudDragonApi.Data; // Assuming RacesPopulator lives here
using CloudDragonApi.Models; // Assuming
namespace CloudDragonApi.Races
{
    public static class GetAllRaces
    {
        [FunctionName("GetAllRaces")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "races")] HttpRequest req,
            ILogger log)
        {
            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json"); // fileName ignored in current implementation

            return new OkObjectResult(new { success = true, races });
        }
    }
}
