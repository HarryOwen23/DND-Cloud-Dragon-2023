using System.Collections.Generic;

namespace CloudDragonLib
{
    /// <summary>
    /// Provides the classic D&D standard array ability scores.
    /// </summary>
    public static class CharacterStatsStandardArray
    {
        /// <summary>
        /// The values used for the standard array in order of assignment.
        /// </summary>
        private static readonly int[] StandardArray = { 15, 14, 13, 12, 10, 8 };

        /// <summary>
        /// Ability names used when generating the standard array dictionary.
        /// </summary>
        private static readonly string[] AbilityNames = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };

        /// <summary>
        /// Returns a dictionary of ability scores using the standard array in order.
        /// </summary>
        public static Dictionary<string, int> Generate()
        {
            var stats = new Dictionary<string, int>();
            for (int i = 0; i < AbilityNames.Length; i++)
            {
                stats[AbilityNames[i]] = StandardArray[i];
                Console.WriteLine($"{AbilityNames[i]} assigned {StandardArray[i]}");
            }
            Console.WriteLine("Standard array results:");
            foreach (var kvp in stats)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            return stats;
        }
    }
}
