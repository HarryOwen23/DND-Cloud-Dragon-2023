using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

namespace CloudDragonApi.Spellcasting
{
    public static class GetAvailableCantrips
    {
        [FunctionName("GetAvailableCantrips")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cantrips/{className}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                containerName: "Spells",
                SqlQuery = "SELECT * FROM c WHERE c.ClassName = {className} AND c.Type = 'Cantrip'",
                Connection = "CosmosDBConnection")] IEnumerable<dynamic> cantrips,
            string className,
            ILogger log)
        {
            if (cantrips == null || !cantrips.Any())
                return new NotFoundObjectResult(new { success = false, error = "No cantrips found." });

            return new OkObjectResult(new { success = true, cantrips });
        }
    }
}
