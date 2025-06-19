using System;

namespace CloudDragon.CloudDragonApi.Utils
{
    /// <summary>
    /// Simple helper for writing debug information.
    /// This can be toggled off by setting <see cref="IsDebugEnabled"/> to false.
    /// </summary>
    public static class DebugLogger
    {
        /// <summary>
        /// Controls whether debug output is written to the console.
        /// </summary>
        public static bool IsDebugEnabled { get; set; } = true;

        /// <summary>
        /// Write a debug message to the console with timestamp.
        /// </summary>
        public static void Log(string message)
        {
            if (!IsDebugEnabled || string.IsNullOrEmpty(message))
                return;

            Console.WriteLine($"[DEBUG] {DateTime.UtcNow:O} - {message}");
        }
    }
}
