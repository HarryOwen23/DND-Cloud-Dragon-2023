/// <summary>
/// Common global using directives shared across the CloudDragon Azure Functions project.
/// Keeping them in one file avoids cluttering individual classes with repeated using statements.
/// </summary>
// Azure Functions base namespaces
global using Microsoft.Azure.WebJobs;
global using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
global using Microsoft.Azure.WebJobs.Extensions.Http;

// ASP.NET Core primitives and logging abstractions
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Logging;

// Application models and interfaces
global using CloudDragon.Models.ModelContext;
global using CloudDragon.Models;
