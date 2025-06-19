using System;
using System.Collections.Generic;

namespace DNDCloudDragon
{
/// <summary>
/// Represents a playable race and the ability score bonuses it grants.
/// </summary>
public class Race
{
    /// <summary>
    /// Name of the race (e.g. Human, Elf).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Mapping of ability abbreviations to bonus values.
    /// </summary>
    public Dictionary<string, int> AbilityScoreBonuses { get; } = new();

    /// <summary>
    /// Initializes a new race with the specified name.
    /// </summary>
    /// <param name="name">Race name.</param>
    public Race(string name)
    {
        Name = name;
        Console.WriteLine($"Created race {name}");
    }

    /// <summary>
    /// Adds or increments an ability score bonus for this race.
    /// </summary>
    /// <param name="ability">Ability name (STR, DEX, etc.).</param>
    /// <param name="bonus">Bonus amount to add.</param>
    public void AddAbilityScoreBonus(string ability, int bonus)
    {
        if (AbilityScoreBonuses.ContainsKey(ability))
        {
            AbilityScoreBonuses[ability] += bonus;
        }
        else
        {
            AbilityScoreBonuses[ability] = bonus;
        }

        Console.WriteLine($"Added {bonus} {ability} bonus to {Name}");
    }

    /// <summary>
    /// Removes a specific ability bonus from the race.
    /// </summary>
    /// <param name="ability">Ability name to remove.</param>
    public void RemoveAbilityScoreBonus(string ability)
    {
        if (AbilityScoreBonuses.Remove(ability))
        {
            Console.WriteLine($"Removed {ability} bonus from {Name}");
        }
    }
}
}
