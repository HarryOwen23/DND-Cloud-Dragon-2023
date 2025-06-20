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
    /// Retrieves leveled spells for the specified class and level.
    /// </summary>
    public static class GetAvailableSpells
    {
        /// <summary>
        /// Queries the Cosmos DB container for matching spells.
        /// </summary>
        /// <param name="req">HTTP request.</param>
        /// <param name="spells">Query result set.</param>
        /// <param name="className">Name of the caster class.</param>
        /// <param name="level">Spell level requested.</param>
        /// <param name="log">Function logger.</param>
        [FunctionName("GetAvailableSpells")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "spells/{className}/{level}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudDragonDB",
                ContainerName: "Spells",
                SqlQuery = "SELECT * FROM c WHERE c.ClassName = {className} AND c.Level = {level}",
                ConnectionStringSetting = "CosmosDBConnection")] IEnumerable<dynamic> spells,
            string className,
            int level,
            ILogger log)
        {
            if (spells == null || !spells.Any())
                return new NotFoundObjectResult(new { success = false, error = "No spells found." });

            return new OkObjectResult(new { success = true, spells });
        }
    }
}
