using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon
{
    /// <summary>
    /// Simple container for storing and modifying ability scores.
    /// </summary>
    internal class Ability_Score_Class
    {
        /// <summary>
        /// Current ability scores keyed by ability name.
        /// </summary>
        public Dictionary<string, int> AbilityScores { get; private set; }

        /// <summary>
        /// Initializes all ability scores to zero.
        /// </summary>
        public Ability_Score_Class()
        {
            AbilityScores = new Dictionary<string, int>
            {
                {"Strength", 0},
                { "Dexterity", 0 },
                { "Constitution", 0 },
                { "Intelligence", 0 },
                { "Wisdom", 0 },
                { "Charisma", 0 }
            };
        }
        /// <summary>
        /// Sets a specific ability score.
        /// </summary>
        /// <param name="abilityName">Ability name.</param>
        /// <param name="score">Score value.</param>
        public void SetAbilityScore(string abilityName, int score)
        {
            if (AbilityScores.ContainsKey(abilityName))
            {
                AbilityScores[abilityName] = score;
            }
            else
            {
                throw new ArgumentException("Invalid ability name.");
            }
        }

        /// <summary>
        /// Retrieves the value for the specified ability.
        /// </summary>
        /// <param name="abilityName">Ability name.</param>
        /// <returns>The current score.</returns>
        public int GetAbilityScore(string abilityName)
        {
            if (AbilityScores.ContainsKey(abilityName))
            {
                return AbilityScores[abilityName];
            }
            else
            {
                throw new ArgumentException("Invalid ability name.");
            }
        }
        /// <summary>
        /// Applies a set of racial bonuses to this stat block.
        /// </summary>
        /// <param name="racialBonuses">Bonus values keyed by ability.</param>
        public void ApplyRacialBonus(Dictionary<string, int> racialBonuses)
        {
            foreach (var bonus in racialBonuses)
            {
                if (AbilityScores.ContainsKey(bonus.Key))
                {
                    AbilityScores[bonus.Key] += bonus.Value;
                }
                else
                {
                    throw new ArgumentException($"Invalid ability name in racial bonuses: {bonus.Key}");
                }
            }
        }

        /// <summary>
        /// Returns a formatted string of ability names and values.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var ability in AbilityScores)
            {
                sb.AppendLine($"{ability.Key}: {ability.Value}");
            }
            return sb.ToString();
        }
    }
}
