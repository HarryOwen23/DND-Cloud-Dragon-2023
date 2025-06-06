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
        public string Name { get; set; }

        /// <summary>
        /// Ability score bonuses granted by this race.
        /// </summary>
        public Dictionary<string, int> AbilityBonuses { get; } = new();

        public Races(string nameOfRace)
        {
            Name = nameOfRace;
        }

        public void AddAbilityBonus(string abilityName, int abilityBonus)
        {
            if (AbilityBonuses.ContainsKey(abilityName))
            {
                AbilityBonuses[abilityName] += abilityBonus;
            }
            else
            {
                AbilityBonuses[abilityName] = abilityBonus;
            }
        }

        public void RemoveAbilityBonus(string abilityName)
        {
            AbilityBonuses.Remove(abilityName);
        }
    }
}
