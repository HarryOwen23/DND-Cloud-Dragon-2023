using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Poison
    {
        [JsonPropertyName("Name")]
        public string Name {  get; set; }
        [JsonPropertyName("Type")]
        public string Type { get; set; }
        [JsonPropertyName("Price per Dose")]
        public string DosePrice { get; set; }
    }

    public class PoisonCat
    {
        [JsonPropertyName("poisons")]
        public List<Poison> Poisons { get; set; }
    }

    public class PoisonData
    {
        [JsonPropertyName("Poison Categories")]
        public List<Poison> PoisonCategories { get; set; }
    }

    internal class Poison_Json_Loader
    {
        public static PoisonData LoadPoisonData(string jsonFilePath)
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
                    return new PoisonData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new PoisonData();
                }

                var poisonData = JsonSerializer.Deserialize<PoisonData>(jsonData);

                if (poisonData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new PoisonData();
                }

                return poisonData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }
    internal class PoisonLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Magical Item Data ...");
            string jsonFilePathpoison = "Poisons\\Poisons.json";

            var poisonstuff = Poison_Json_Loader.LoadPoisonData(jsonFilePathpoison);

            // Display the Armor data for common magical items
            if (poisonstuff != null)
            {
                Console.WriteLine("Poisons:");
                foreach (var poi in poisonstuff.PoisonCategories)
                {
                    Console.WriteLine($"- Name: {poi.Name}, Type: {poi.Type}, Price per Dose: {poi.DosePrice} ");
                }
            }
        }
    }

}
