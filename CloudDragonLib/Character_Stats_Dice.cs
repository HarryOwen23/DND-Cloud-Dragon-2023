using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

// a class created to provide RNG for individual dnd stats 
public class CharStats
{
    // Private readonly class to retrieve the 
	private static Random statsrng;

	// The public variables stats are created
	public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }
    public int Hitpoints { get; }
    public int Modifier { get; }

    
    public CharStats (int Str, int Dex, int Con, int Int, int Wis, int Cha)
    {
        statsrng = new Random(DateTime.UtcNow.Millisecond);
        Strength = Str;
        Dexterity = Dex;
        Constitution = Con;
        Intelligence = Int;
        Wisdom = Wis;
        Charisma = Cha;
        
    }

    // Modifier variable 
    public static Modifier(int score) => (int)Math.Floor((score - 10) * 0.5);

    // Variable RollD6 is created as an RNG
    private static int RollD6() => statsrng.Next(1, 7);

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
