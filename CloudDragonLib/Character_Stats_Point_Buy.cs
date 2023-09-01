using System;
using System.Collections.Generic;

public class Character_Stats_Point_Buy
{
    // String array is set 
    static void Main()
    {
        string[] abilityScores = { "Str", "Dex", "Con", "Int", "Wis", "Cha" };
        
        // boolean to allow editing 
        bool editing = true;

        int[] CharStats = { 8, 8, 8, 8, 8, 8 };
        int max_spend_points = 27; 

        while (editing)
        {
            int select = 0; 
            int adjust = 0;
            int result = 0;
            int remainder = 0;

            // Console writelines 
            Console.WriteLine("You have " + max_spend_points + " points to spend on your ability scores.");
            Console.WriteLine("Select a number to adjust that ability's score:\n1) STR = " + abilityScores[0] + "\n2) DEX = " + abilityScores[1] + "\n3) CON = " + abilityScores[2] + "\n4) INT = " + abilityScores[3] + "\n5) WIS = " + abilityScores[4] + "\n6) CHA = " + abilityScores[5]);
            Console.WriteLine("Select a number outside the above range to finalize your ability scores.");


            if (int.TryParse(Console.ReadLine(), out select))
            {
                while (select >= 1 && select <= 6)
                {
                    Console.WriteLine("Enter the number of points to adjust the score: ");
                    if (int.TryParse(Console.ReadLine(), out adjust))
                    {
                        if (adjust <= max_spend_points && adjust >= CharStats[select - 1] + 8)
                        {
                            max_spend_points -= adjust;
                            CharStats[select - 1] += adjust;
                            Console.WriteLine("New " + abilityScores[select - 1] + " score: " + abilityScores[select - 1]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid entry. You don't have enough points or the value would exceed 15.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry. Please enter a valid integer.");
                    }

                    Console.WriteLine("You have " + max_spend_points + " points left.");
                    Console.WriteLine("Select a number to adjust that ability's score:\n1) STR = " + abilityScores[0] + "\n2) DEX = " + abilityScores[1] + "\n3) CON = " + abilityScores[2] + "\n4) INT = " + abilityScores[3] + "\n5) WIS = " + abilityScores[4] + "\n6) CHA = " + abilityScores[5]);
                    Console.WriteLine("Select a number outside the above range to finalize your ability scores.");

                    if (!int.TryParse(Console.ReadLine(), out select))
                        break;
                }

                editing = false;
            }
            else
            {
                Console.WriteLine("Invalid entry; try again.");
            }
        }

        Console.WriteLine("Ability Score Adjustment Completed!");
        Console.WriteLine("The Final Scores Are:");
        for (int i = 0; i < abilityScores.Length; i++)
        {
            Console.WriteLine(abilityScores[i] + ": " + abilityScores[i]);
        }
    }
}

