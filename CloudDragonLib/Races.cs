using System;
using System.Collections.Generic;

// Class created to allow users to select a race for characters 
namespace CloudDragonLib.Models
{
    /// <summary>
    /// Represents a race and its ability score bonuses.
    /// </summary>
    public class Races
    {
        /// <summary>
        /// Name of the race.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ability score bonuses granted by this race.
        /// </summary>
        public Dictionary<string, int> AbilityBonuses { get; } = new();

        /// <summary>
        /// Creates a race with the given name.
        /// </summary>
        /// <param name="nameOfRace">Name of the race.</param>
        public Races(string nameOfRace)
        {
            Name = nameOfRace;
            Console.WriteLine($"Created race {nameOfRace}");
        }

        /// <summary>
        /// Adds or increments an ability bonus for this race.
        /// </summary>
        /// <param name="abilityName">Ability abbreviation (STR, DEX, etc.).</param>
        /// <param name="abilityBonus">Bonus amount to add.</param>
        public void AddAbilityBonus(string abilityName, int abilityBonus)
        {
            if (AbilityBonuses.TryGetValue(abilityName, out var current))
            {
                AbilityBonuses[abilityName] = current + abilityBonus;
                Console.WriteLine($"Increased {Name}'s {abilityName} bonus from {current} to {AbilityBonuses[abilityName]}");
            }
            else
            {
                AbilityBonuses[abilityName] = abilityBonus;
                Console.WriteLine($"Added {abilityBonus} {abilityName} bonus to {Name}");
            }
        }

        /// <summary>
        /// Removes an ability bonus from the race if present.
        /// </summary>
        /// <param name="abilityName">Ability abbreviation to remove.</param>
        public void RemoveAbilityBonus(string abilityName)
        {
            if (AbilityBonuses.Remove(abilityName))
            {
                Console.WriteLine($"Removed {abilityName} bonus from {Name}");
            }
        }
    }
}
