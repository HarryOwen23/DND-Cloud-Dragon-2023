using System;
using System.Collections.Generic;

/// <summary>
/// Provides a simple implementation of the standard 27 point buy system.
/// </summary>
public class CharacterStatsPointBuy
{
    private static readonly string[] AbilityNames = {"STR", "DEX", "CON", "INT", "WIS", "CHA"};

    private static readonly IReadOnlyDictionary<int, int> CostTable = new Dictionary<int, int>
    {
        [8] = 0,
        [9] = 1,
        [10] = 2,
        [11] = 3,
        [12] = 4,
        [13] = 5,
        [14] = 7,
        [15] = 9
    };

    /// <summary>
    /// Validates the requested scores and returns a completed stat block.
    /// </summary>
    /// <param name="requested">Dictionary mapping ability names to desired scores.</param>
    /// <returns>Finalized ability scores.</returns>
    /// <exception cref="ArgumentException">Thrown when scores are invalid or exceed 27 points.</exception>
    public Dictionary<string, int> GenerateStats(Dictionary<string, int> requested)
    {
        if (requested == null)
            throw new ArgumentNullException(nameof(requested));

        var result = new Dictionary<string, int>();
        int points = 0;

        foreach (var ability in AbilityNames)
        {
            int score = requested.TryGetValue(ability, out var value) ? value : 8;
            if (score < 8 || score > 15)
                throw new ArgumentException($"{ability} score must be between 8 and 15.");

            points += CostTable[score];
            result[ability] = score;
        }

        if (points > 27)
            throw new ArgumentException("Point buy total exceeds 27 points.");

        return result;
    }
}
