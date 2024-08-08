using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon
{
    internal class Ability_Score_Class
    {
        public Dictionary<string, int> AbilityScores { get; private set; }

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