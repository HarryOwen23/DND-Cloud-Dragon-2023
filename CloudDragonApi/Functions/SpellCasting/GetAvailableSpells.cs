using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonLib.Models;

namespace CloudDragonApi.Spellcasting

[FunctionName("GetAvailableSpells")]
public static IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "spells/{className}/{level}")] HttpRequest req,
    [CosmosDB(
        databaseName: "CloudDragonDB",
        containerName: "Spells",
        SqlQuery = "SELECT * FROM c WHERE c.ClassName = {className} AND c.Level = {level}",
        Connection = "CosmosDBConnection")] IEnumerable<dynamic> spells,
    string className,
    int level,
    ILogger log)
{
    if (spells == null || !spells.Any())
        return new NotFoundObjectResult(new { success = false, error = "No spells found." });

    return new OkObjectResult(new { success = true, spells });
}
