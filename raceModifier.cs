using System;

/// <summary>
/// Demonstrates how race ability score bonuses can be defined and displayed.
/// </summary>
class RaceModifier
{
    static void Main()
    {
        var human = new Race("Human");
        human.AddAbilityScoreBonus("STR", 1);
        human.AddAbilityScoreBonus("DEX", 1);
        human.AddAbilityScoreBonus("CON", 1);
        human.AddAbilityScoreBonus("INT", 1);
        human.AddAbilityScoreBonus("WIS", 1);
        human.AddAbilityScoreBonus("CHA", 1);

        Console.WriteLine($"{human.Name} ability score bonuses:");
        foreach (var bonus in human.AbilityScoreBonuses)
            Console.WriteLine($"{bonus.Key}: {bonus.Value}");
    }
}
