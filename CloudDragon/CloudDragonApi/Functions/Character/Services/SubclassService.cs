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
    public static class SubclassService
    {
        [FunctionName("GetAvailableSubclasses")]
        public static async Task<IActionResult> GetAvailableSubclasses(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "subclasses/{className}")] HttpRequest req,
            string className,
            [CosmosDB(
                DatabaseName = "CloudDragonDB",
                ContainerName = "Classes",
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
