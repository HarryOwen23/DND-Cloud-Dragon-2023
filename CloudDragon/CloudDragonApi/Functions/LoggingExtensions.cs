using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CloudDragonApi
{
    public static class LoggingExtensions
    {
        public static void LogRequestDetails(this ILogger log, HttpRequest req, string functionName)
        {
            var ip = req.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
            log.LogInformation("Function {Function} triggered via {Method} {Path} from {IP}",
                functionName, req.Method, req.Path, ip);
        }
    }
}
