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
        [JsonPropertyName("Acquisitions Incorporated Trinkets")]
        public List<Trinkettype> AcquisitionsIncorporatedTrinkets { get; set; }

    }

    internal class TrinketJsonLoader
    {
        public static TrinketsData LoadTrinketData(string jsonFilePath)
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
                    return new TrinketsData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new TrinketsData();
                }

                // Debugging: Log JSON data to verify it's loaded correctly
                Console.WriteLine($"JSON Data: {jsonData}");

                var trinketData = JsonSerializer.Deserialize<TrinketsData>(jsonData);

                // Debugging: Log deserialized data to verify it's loaded correctly
                if (trinketData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default TrinketsData.");
                    return new TrinketsData();
                }

                if (trinketData.AcquisitionsIncorporatedTrinkets == null)
                {
                    Console.WriteLine("AcquisitionsIncorporatedTrinkets is null. Returning default TrinketsData.");
                    return new TrinketsData();
                }

                // Debugging: Log deserialized data to verify it's loaded correctly
                Console.WriteLine($"Trinket Data Count: {trinketData.AcquisitionsIncorporatedTrinkets.Count}");

                foreach (var trinket in trinketData.AcquisitionsIncorporatedTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }

                return trinketData;
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
        void ILoader.Load()
        {
            Console.WriteLine("Loading Trinket Data ...");

            // Define the paths to the JSON files
            string jsonFilePathAcquisitionsIncorperated = "Trinkets\\Trinkets_Acquisitions_Incorporated.json";

            // Load the trinket data using the TrinketJsonLoader
            var trinketsAcquisitionsIncorporated = TrinketJsonLoader.LoadTrinketData(jsonFilePathAcquisitionsIncorperated);
            


            // Display the trinket data for Acquisitions Incorporated
            if (trinketsAcquisitionsIncorporated != null && trinketsAcquisitionsIncorporated.AcquisitionsIncorporatedTrinkets != null)
            {
                Console.WriteLine("Acquisitions Incorporated Trinkets:");
                foreach (var trinket in trinketsAcquisitionsIncorporated.AcquisitionsIncorporatedTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

        }
    }
}
