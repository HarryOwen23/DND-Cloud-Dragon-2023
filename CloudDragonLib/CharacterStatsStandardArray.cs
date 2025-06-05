using System.Collections.Generic;

/// <summary>
/// Provides the classic D&D standard array ability scores.
/// </summary>
public static class CharacterStatsStandardArray
{
    private static readonly int[] StandardArray = { 15, 14, 13, 12, 10, 8 };
    private static readonly string[] AbilityNames = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };

    /// <summary>
    /// Returns a dictionary of ability scores using the standard array in order.
    /// </summary>
    public static Dictionary<string, int> Generate()
    {
        var stats = new Dictionary<string, int>();
        for (int i = 0; i < AbilityNames.Length; i++)
            stats[AbilityNames[i]] = StandardArray[i];
        return stats;
    }
}
