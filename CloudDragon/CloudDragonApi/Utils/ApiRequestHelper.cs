using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CloudDragon.CloudDragonApi.Utils
{
    /// <summary>
    /// Utility methods for working with HTTP requests.
    /// </summary>
    public static class ApiRequestHelper
    {
        /// <summary>
        /// Reads and deserializes JSON from the request body.
        /// </summary>
        public static async Task<T?> ReadJsonAsync<T>(HttpRequest req, ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(body))
            {
                log.LogWarning("Request body is empty");
                return default;
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch (JsonException ex)
            {
                log.LogError(ex, "Failed to deserialize request body to {Type}", typeof(T).Name);
                return default;
            }
        }

        /// <summary>
        /// Validates the API key provided via header 'x-api-key'.
        /// </summary>
        public static bool IsAuthorized(HttpRequest req, ILogger log)
        {
            var expected = System.Environment.GetEnvironmentVariable("API_KEY");
            if (string.IsNullOrEmpty(expected))
                return true; // no key required

            if (!req.Headers.TryGetValue("x-api-key", out var provided) || provided != expected)
            {
                log.LogWarning("Unauthorized request missing or invalid API key");
                return false;
            }
            return true;
        }
    }
}
