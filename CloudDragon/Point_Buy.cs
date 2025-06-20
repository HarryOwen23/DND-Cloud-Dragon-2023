using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon
{
    /// <summary>
    /// Implements a simple 27 point buy system for ability scores.
    /// </summary>
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
        /// <summary>
        /// Current ability scores after purchases.
        /// </summary>
        public Dictionary<string, int> AbilityScores { get; private set; }

        /// <summary>
        /// Points remaining to spend.
        /// </summary>
        public int RemainingPoints { get; private set; }

        /// <summary>
        /// Initializes a new point buy calculator with default scores.
        /// </summary>
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

        /// <summary>
        /// Attempts to purchase an ability score at the specified value.
        /// </summary>
        /// <param name="abilityName">Ability name.</param>
        /// <param name="score">Desired score.</param>
        /// <returns>True if the purchase succeeded.</returns>
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

        /// <summary>
        /// Returns a summary of the current ability scores.
        /// </summary>
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
