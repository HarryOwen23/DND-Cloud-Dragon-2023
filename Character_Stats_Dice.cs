using System;
using System.Collections.Generic;
using System.Linq;

// a class created to provide RNG for individual dnd stats 
public class Character_Stats_Dice
{
    // Private readonly class to retrieve the 
	private readonly static stats rng new Random(DateTime.Now.Millisecond);

	// The public variables stats are created
	public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }
    public int Hitpoints { get; }

    
    public CharStats (int Str, int Dex, int Con, int Int, int Wis, int Cha)
    {
        Strength = Str;
        Dexterity = Dex;
        Constitution = Con;
        Intelligence = Int;
        Wisdom = Wis;
        Charisma = Cha;
        // HP variable will relate to Modifier variable  
        HP = 10 + Modifier(Con);
    }

    // Modifier variable 
    public static Modifier(int score) => (int)Math.Floor((score - 10) * 0.5);

    // Variable RollD6 is created as an RNG
    private static int RollD6() => rng.Next(1, 7);

    // Storring variable Dice
    public static int AbilityScore() =>
        // Will roll 4 times and total highest 3
        new[] { RollD6(), RollD6(), RollD6(), RollD6() }
        .OrderByDescending(d => d)
        .Take(3)
        .Sum();

    // Character Generator Function 
    public static CharGen Generate()
    {
        return new CharGen(
            AbilityScore(), AbilityScore(),
            AbilityScore(), AbilityScore(),
            AbilityScore(), AbilityScore()
        );
    }
}
