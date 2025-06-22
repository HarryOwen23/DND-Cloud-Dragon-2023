using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CloudDragon.CloudDragonApi.Functions.Character
{
    /// <summary>
    /// Utility endpoints for working with character subclasses.
    /// </summary>
    public static class SubclassService
    {
        /// <summary>
        /// Retrieves available subclasses for the specified class.
        /// </summary>
        /// <param name="req">Incoming request.</param>
        /// <param name="className">Class name being queried.</param>
        /// <param name="allEntries">Cosmos DB records for that class.</param>
        /// <param name="log">Function logger.</param>
        /// <returns>List of subclasses or an error response.</returns>
        [Microsoft.Azure.WebJobs.FunctionName("GetAvailableSubclasses")]
        public static async Task<IActionResult> GetAvailableSubclasses(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "subclasses/{className}")] HttpRequest req,
            string className,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                CollectionName = "Classes",
                SqlQuery = "SELECT * FROM c WHERE c.{partitionKey} = {className}",
                ConnectionStringSetting = "CosmosDBConnection")] IEnumerable<dynamic> allEntries,
            ILogger log)
        {
            if (string.IsNullOrEmpty(className))
                return new BadRequestObjectResult(new { success = false, error = "Class name is required." });

            if (allEntries == null || !allEntries.Any())
                return new NotFoundObjectResult(new { success = false, error = "No subclasses found for this class." });

            // Filter out the base class and only return subclasses
            var subclasses = allEntries
                .Where(entry => ((string)entry.id).StartsWith("subclass_"))
                .Select(entry => new
                {
                    Id = entry.id,
                    Name = entry.name
                })
                .ToList();

            if (!subclasses.Any())
                return new NotFoundObjectResult(new { success = false, error = "No subclasses available." });

            return new OkObjectResult(new { success = true, subclasses });
        }
    }
}
