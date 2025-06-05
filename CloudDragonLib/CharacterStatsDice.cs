using System;
using System.Linq;

/// <summary>
/// Utility for generating random ability scores using the standard 4d6 drop lowest method.
/// </summary>
public class CharacterStatsDice
{
    private static readonly Random rng = new();

    public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }

    public int Hitpoints => 10 + Modifier(Constitution);

    public CharacterStatsDice(int str, int dex, int con, int intel, int wis, int cha)
    {
        Strength = str;
        Dexterity = dex;
        Constitution = con;
        Intelligence = intel;
        Wisdom = wis;
        Charisma = cha;
    }

    public static int Modifier(int score) => (int)Math.Floor((score - 10) / 2.0);

    private static int RollD6() => rng.Next(1, 7);

    public static int AbilityScore()
    {
        return Enumerable.Range(0, 4)
            .Select(_ => RollD6())
            .OrderByDescending(d => d)
            .Take(3)
            .Sum();
    }

    public static CharacterStatsDice Generate()
    {
        return new CharacterStatsDice(
            AbilityScore(), AbilityScore(),
            AbilityScore(), AbilityScore(),
            AbilityScore(), AbilityScore());
    }
}
