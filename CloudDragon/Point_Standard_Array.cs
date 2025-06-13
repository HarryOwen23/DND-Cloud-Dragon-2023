using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon
{
    internal class Point_Standard_Array
    {
        /// <summary>
        /// The available values of the standard array.
        /// </summary>
        private readonly int[] standardArray = { 15, 14, 13, 12, 10, 8 };

        /// <summary>
        /// Stores the selected scores keyed by ability name.
        /// </summary>
        public Dictionary<string, int> AbilityScores { get; private set; }

        private HashSet<int> usedPointVals;

        /// <summary>
        /// Initializes the standard array helper.
        /// </summary>
        public Point_Standard_Array()
        {
            AbilityScores = new Dictionary<string, int>();
            usedPointVals = new HashSet<int>();
        }

        /// <summary>
        /// Attempts to assign a score to the specified ability.
        /// </summary>
        /// <param name="ability">Ability name.</param>
        /// <param name="score">Score value from the standard array.</param>
        /// <returns>True if the assignment succeeded.</returns>
        public bool AssignAbilityScore(string ability, int score)
        {
            if (Array.Exists(standardArray, element => element == score) && !usedPointVals.Contains(score))
            {
                AbilityScores[ability] = score;
                usedPointVals.Add(score);
                Console.WriteLine($"Assigned {score} to {ability}");
                return true;
            }
            Console.WriteLine($"Failed to assign {score} to {ability}");
            return false;
        }

        /// <summary>
        /// Determines if all scores from the array have been used.
        /// </summary>
        public bool AllScoresAssigned()
        {
            bool complete = usedPointVals.Count == standardArray.Length;
            Console.WriteLine($"All scores assigned? {complete}");
            return complete;
        }

        /// <summary>
        /// Writes all assigned scores to the console.
        /// </summary>
        public void DisplayScores()
        {
            foreach (var kvp in AbilityScores)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}
