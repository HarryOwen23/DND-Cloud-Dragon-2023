using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudDragonApi.Races
{
    public static class GetRaceByName
    {
        [FunctionName("GetRaceByName")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "races/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            var populator = new RacesPopulator();
            var races = await populator.Populate("races.json");
            var match = races.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (match == null)
                return new NotFoundObjectResult(new { success = false, error = "Race not found." });

            return new OkObjectResult(new { success = true, race = match });
        }
    }
}
