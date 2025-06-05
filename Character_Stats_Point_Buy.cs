using System;
using System.Collections.Generic;

/// <summary>
/// Simple console utility for allocating ability scores using the standard
/// 27-point buy system.
/// </summary>
public class CharacterStatsPointBuy
{
    private static readonly string[] AbilityNames = {"STR", "DEX", "CON", "INT", "WIS", "CHA"};

    public static void Main()
    {
        var stats = new int[AbilityNames.Length];
        for (int i = 0; i < stats.Length; i++)
            stats[i] = 8;

        int points = 27;
        while (true)
        {
            Console.WriteLine($"Points remaining: {points}");
            for (int i = 0; i < AbilityNames.Length; i++)
                Console.WriteLine($"{i + 1}) {AbilityNames[i]} = {stats[i]}");

            Console.WriteLine("Choose ability number to modify or any other key to finish:");
            if (!int.TryParse(Console.ReadLine(), out int selection) || selection < 1 || selection > 6)
                break;

            Console.Write("Enter adjustment amount: ");
            if (!int.TryParse(Console.ReadLine(), out int adjust))
                continue;

            int current = stats[selection - 1];
            int proposed = current + adjust;
            if (proposed < 8 || proposed > 15)
            {
                Console.WriteLine("Scores must be between 8 and 15.");
                continue;
            }

            int cost = Cost(proposed) - Cost(current);
            if (points - cost < 0)
            {
                Console.WriteLine("Not enough points.");
                continue;
            }

            points -= cost;
            stats[selection - 1] = proposed;
        }

        Console.WriteLine("Final ability scores:");
        for (int i = 0; i < AbilityNames.Length; i++)
            Console.WriteLine($"{AbilityNames[i]}: {stats[i]}");
    }

    private static int Cost(int score) => score switch
    {
        8 => 0,
        9 => 1,
        10 => 2,
        11 => 3,
        12 => 4,
        13 => 5,
        14 => 7,
        15 => 9,
        _ => throw new ArgumentOutOfRangeException(nameof(score))
    };
}
