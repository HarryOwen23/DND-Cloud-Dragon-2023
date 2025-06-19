using System;
using System.Linq;

namespace CloudDragonLib
{
/// <summary>
/// Utility for generating random ability scores using the standard 4d6 drop lowest method.
/// </summary>
public class CharacterStatsDice
{
    /// <summary>
    /// Random number generator used for dice rolls.
    /// </summary>
    private static readonly Random rng = Random.Shared;

    /// <summary>
    /// Strength ability score.
    /// </summary>
    public int Strength { get; }

    /// <summary>
    /// Dexterity ability score.
    /// </summary>
    public int Dexterity { get; }

    /// <summary>
    /// Constitution ability score.
    /// </summary>
    public int Constitution { get; }

    /// <summary>
    /// Intelligence ability score.
    /// </summary>
    public int Intelligence { get; }

    /// <summary>
    /// Wisdom ability score.
    /// </summary>
    public int Wisdom { get; }

    /// <summary>
    /// Charisma ability score.
    /// </summary>
    public int Charisma { get; }

    /// <summary>
    /// Hitpoints calculated from Constitution modifier.
    /// </summary>
    public int Hitpoints => 10 + Modifier(Constitution);

    /// <summary>
    /// Creates a new <see cref="CharacterStatsDice"/> with the provided ability scores.
    /// </summary>
    /// <param name="str">Strength score.</param>
    /// <param name="dex">Dexterity score.</param>
    /// <param name="con">Constitution score.</param>
    /// <param name="intel">Intelligence score.</param>
    /// <param name="wis">Wisdom score.</param>
    /// <param name="cha">Charisma score.</param>
    public CharacterStatsDice(int str, int dex, int con, int intel, int wis, int cha)
    {
        Strength = str;
        Dexterity = dex;
        Constitution = con;
        Intelligence = intel;
        Wisdom = wis;
        Charisma = cha;
        Console.WriteLine($"Stats created STR={str} DEX={dex} CON={con} INT={intel} WIS={wis} CHA={cha}");
    }

    /// <summary>
    /// Calculates the ability modifier for the given score.
    /// </summary>
    public static int Modifier(int score) => (int)Math.Floor((score - 10) / 2.0);

    /// <summary>
    /// Rolls a single six-sided die.
    /// </summary>
    private static int RollD6() => rng.Next(1, 7);

    /// <summary>
    /// Rolls four six-sided dice, drops the lowest roll and sums the rest.
    /// </summary>
    public static int AbilityScore()
    {
        var rolls = Enumerable.Range(0, 4)
            .Select(_ => RollD6())
            .ToArray();
        var ordered = rolls.OrderByDescending(d => d).ToArray();
        var score = ordered.Take(3).Sum();
        Console.WriteLine($"Rolled {string.Join(",", rolls)} => {score}");
        return score;
    }

    /// <summary>
    /// Generates a full set of six ability scores using <see cref="AbilityScore"/>.
    /// </summary>
    public static CharacterStatsDice Generate()
    {
        var stats = new CharacterStatsDice(
            AbilityScore(), AbilityScore(),
            AbilityScore(), AbilityScore(),
            AbilityScore(), AbilityScore());
        Console.WriteLine($"Generated stats STR={stats.Strength} DEX={stats.Dexterity} CON={stats.Constitution} INT={stats.Intelligence} WIS={stats.Wisdom} CHA={stats.Charisma}");
        return stats;
    }
}
