using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon
{
    internal class Point_Standard_Array
    {
        // readonly int for standard array 
        private readonly int[] standardArray = { 15, 14, 13, 12, 10, 8 };

        public Dictionary<string, int> AbilityScores { get; private set; }

        private HashSet<int> usedPointVals;

        public Point_Standard_Array()
        {
            AbilityScores = new Dictionary<string, int>();
            usedPointVals = new HashSet<int>();
        }

        // Method to assign a score to an ability
        public bool AssignAbilityScore(string ability, int score)
        {
            if (Array.Exists(standardArray, element => element == score) && !usedPointVals.Contains(score))
            {
                AbilityScores[ability] = score;
                usedPointVals.Add(score);
                return true;
            }
            return false;
        }

        // Method to check if all scores have been assigned
        public bool AllScoresAssigned()
        {
            return usedPointVals.Count == standardArray.Length;
        }

        // Method to display assigned scores
        public void DisplayScores()
        {
            foreach (var kvp in AbilityScores)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}
