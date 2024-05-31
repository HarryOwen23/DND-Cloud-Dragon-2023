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
        public string Name { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("dosePrice")]
        public string DosePrice { get; set; }
    }

    public class PoisonCategory
    {
        [JsonPropertyName("poisons")]
        public List<Poison> Poisons { get; set; }
    }

    public class PoisonData
    {
        [JsonPropertyName("Poison Categories")]
        public List<PoisonCategory> PoisonCategories { get; set; }
    }

    internal class PoisonJsonLoader
    {
        public static PoisonData LoadPoisonData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new PoisonData();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<PoisonData>(jsonData) ?? new PoisonData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }
    internal class PoisonLoader : ILoader
    {
        private const string JsonFilePathPoison = "Poisons\\Poisons.json";

        public void Load()
        {
            Console.WriteLine("Loading Poison Data ...");
            var poisonData = PoisonJsonLoader.LoadPoisonData(JsonFilePathPoison);

            if (poisonData?.PoisonCategories != null)
            {
                Console.WriteLine("Poisons:");
                foreach (var category in poisonData.PoisonCategories)
                {
                    foreach (var poison in category.Poisons)
                    {
                        Console.WriteLine($"- Name: {poison.Name}, Type: {poison.Type}, Price per Dose: {poison.DosePrice}");
                    }
                }
            }
        }
    }
}
