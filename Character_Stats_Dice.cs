using System;
using System.Linq;

/// <summary>
/// Generates random DND character ability scores using 4d6 drop lowest.
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
    public int Hitpoints { get; }

    public CharacterStatsDice(int str, int dex, int con, int intel, int wis, int cha)
    {
        Strength = str;
        Dexterity = dex;
        Constitution = con;
        Intelligence = intel;
        Wisdom = wis;
        Charisma = cha;
        Hitpoints = 10 + Modifier(con);
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
