using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Trinkettype
    {
        [JsonPropertyName("Dice Number")]
        public int DiceNumber { get; set; }
        
        [JsonPropertyName("Trinket")]
        public string Trinket { get; set; }
    }

    public class TrinketCategory
    {
        [JsonPropertyName("Trinkets")]
        public List<Trinkettype> Trinkets { get; set; }
    }

    public class TrinketsData
    {
        [JsonPropertyName("Trinkets")]
        public List<Trinkettype> Trinkets { get; set; }

    }

    internal class TrinketJsonLoader
    {
        public static List<Trinkettype> LoadTrinketData(string jsonFilePath)
        {
            try
            {
                if (jsonFilePath == null)
                {
                    throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null.");
                }

                if (!File.Exists(jsonFilePath))
                {
                    Console.WriteLine($"File not found: {jsonFilePath}");
                    return null;
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return null;
                }

                var trinketData = JsonSerializer.Deserialize<TrinketsData>(jsonData);

                if (trinketData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default TrinketsData.");
                    return null;
                }

                return trinketData.Trinkets;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class TrinketLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Trinket Data ...");

            // Define the paths to the JSON files
            string jsonFilePathAcquisitionsIncorperated = "Trinkets\\Trinkets_Acquisitions_Incorporated.json";

            // Load the trinket data using the TrinketJsonLoader
            var trinketsAcquisitionsIncorporated = TrinketJsonLoader.LoadTrinketData(jsonFilePathAcquisitionsIncorperated);

            // Display the trinket data for Acquisitions Incorporated
            if (trinketsAcquisitionsIncorporated != null)
            {
                Console.WriteLine("Acquisitions Incorporated Trinkets:");
                foreach (var trinket in trinketsAcquisitionsIncorporated)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

        }
    }
}
