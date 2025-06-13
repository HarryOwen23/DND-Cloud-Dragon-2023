using System;

/// <summary>
/// Simple console demo that shows race ability score bonus handling.
/// </summary>
class RaceModifier
{
    /// <summary>
    /// Entry point for the demo application.
    /// </summary>
    static void Main()
    {
        Console.WriteLine("Starting race modifier demo");
        var human = new Race("Human");
        human.AddAbilityScoreBonus("STR", 1);
        human.AddAbilityScoreBonus("DEX", 1);
        human.AddAbilityScoreBonus("CON", 1);
        human.AddAbilityScoreBonus("INT", 1);
        human.AddAbilityScoreBonus("WIS", 1);
        human.AddAbilityScoreBonus("CHA", 1);

        Console.WriteLine($"Created race: {human.Name}");

        Console.WriteLine($"{human.Name} ability score bonuses:");
        foreach (var bonus in human.AbilityScoreBonuses)
            Console.WriteLine($"{bonus.Key}: {bonus.Value}");
        Console.WriteLine("Demo complete");
    }
}
