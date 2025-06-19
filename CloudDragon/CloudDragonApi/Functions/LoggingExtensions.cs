using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudDragon.CloudDragonApi.Functions
{
    /// <summary>
    /// Helper extension methods for logging common request details.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Logs the HTTP method, path, and remote IP address for a function invocation.
        /// </summary>
        /// <param name="log">Function logger.</param>
        /// <param name="req">Incoming HTTP request.</param>
        /// <param name="functionName">Name of the Azure Function being invoked.</param>
        public static void LogRequestDetails(this ILogger log, HttpRequest req, string functionName)
        {
            var ip = req.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
            log.LogInformation(
                "Function {Function} triggered via {Method} {Path} from {IP}",
                functionName,
                req.Method,
                req.Path,
                ip);
        }
    }
}
