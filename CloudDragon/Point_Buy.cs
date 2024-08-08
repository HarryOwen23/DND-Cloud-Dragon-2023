using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon
{
    internal class Point_Buyer
    {
        private const int StartingStatPoints = 27;
        private const int BaseAbilityScore = 8;
        private readonly Dictionary<int, int> PointCostTable = new()
        {
            { 8, 0 },
            { 9, 1 },
            { 10, 2 },
            { 11, 3 },
            { 12, 4 },
            { 13, 5 },
            { 14, 7 },
            { 15, 9 }
        };
        public Dictionary<string, int> AbilityScores { get; private set; }
        public int RemainingPoints { get; private set; }

        public Point_Buyer()
        {
            AbilityScores = new Dictionary<string, int>
            {
                { "Strength", BaseAbilityScore },
                { "Dexterity", BaseAbilityScore },
                { "Constitution", BaseAbilityScore },
                { "Intelligence", BaseAbilityScore },
                { "Wisdom", BaseAbilityScore },
                { "Charisma", BaseAbilityScore }
            };
            RemainingPoints = StartingStatPoints;
        }

        public bool SetAbilityScore(string abilityName, int score)
        {
            if (!AbilityScores.ContainsKey(abilityName))
            {
                throw new ArgumentException("The ability name is invalid. Please try again.");
            }

            if (!PointCostTable.ContainsKey(score))
            {
                throw new ArgumentException("The ability score is invalid. Please try again.");
            }

            int currentScore = AbilityScores[abilityName];
            int costDifference = PointCostTable[score] - PointCostTable[currentScore];

            if (costDifference > RemainingPoints)
            {
                return false;
            }

            AbilityScores[abilityName] = score;
            RemainingPoints -= costDifference;
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var ability in AbilityScores)
            {
                sb.AppendLine($"{ability.Key}: {ability.Value}");
            }
            sb.AppendLine($"Remaining Points: {RemainingPoints}");
            return sb.ToString();
        }
    }
}
