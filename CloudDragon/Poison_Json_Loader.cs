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
        public Dictionary<string, List<Poison>> PoisonCategories { get; set; }
    }

    internal class Poison_Json_Loader
    {
        public static PoisonData LoadPoisonData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var poiCategory = JsonSerializer.Deserialize<PoisonCat>(jsonData);
                return new PoisonData
                {
                    PoisonCategories = new Dictionary<string, List<Poison>>
                    {
                        {Path.GetFileNameWithoutExtension(jsonData), poiCategory.Poisons }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw e;
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
                foreach (var poi in poisonstuff.PoisonCategories["Poisons"])
                {
                    Console.WriteLine($"- Name: {poi.Name}, Type: {poi.Type}, Price per Dose: {poi.DosePrice} ");
                }
            }
        }
    }

}
