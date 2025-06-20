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

namespace CloudDragon.CloudDragonApi.Functions.SpellCasting
{
    /// <summary>
    /// Retrieves cantrip spells for the specified class.
    /// </summary>
    public static class GetAvailableCantrips
    {
        /// <summary>
        /// Returns all matching cantrips from the Cosmos DB container.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="cantrips">Query results from Cosmos DB.</param>
        /// <param name="className">Player class name.</param>
        /// <param name="log">Function logger.</param>
        [FunctionName("GetAvailableCantrips")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cantrips/{className}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Spells",
                SqlQuery = "SELECT * FROM c WHERE c.ClassName = {className} AND c.Type = 'Cantrip'",
                ConnectionStringSetting = "CosmosDBConnection")] IEnumerable<dynamic> cantrips,
            string className,
            ILogger log)
        {
            if (cantrips == null || !cantrips.Any())
                return new NotFoundObjectResult(new { success = false, error = "No cantrips found." });

            return new OkObjectResult(new { success = true, cantrips });
        }
    }
}
