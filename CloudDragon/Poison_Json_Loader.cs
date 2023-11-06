using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class poison
    {
        [JsonPropertyName("Name")]
        public string Name {  get; set; }
        [JsonPropertyName("Type")]
        public string Type { get; set; }
        [JsonPropertyName("Price per Dose")]
        public string dosePrice { get; set; }
    }

    public class poisonCat
    {
        [JsonPropertyName("poisons")]
        public List<poison> poisons { get; set; }
    }

    public class poisonData
    {
        [JsonPropertyName("Poison Categories")]
        public Dictionary<string, List<poison>> poisonCategories { get; set; }
    }

    internal class Poison_Json_Loader
    {
        public static poisonData LoadPoisonData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var poiCategory = JsonSerializer.Deserialize<poisonCat>(jsonData);
                return new poisonData
                {
                    poisonCategories = new Dictionary<string, List<poison>>
                    {
                        {Path.GetFileNameWithoutExtension(jsonData), poiCategory.poisons }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                return null;
            }
        }
    }
    internal class poisonLoader : ILoader
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
                foreach (var poi in poisonstuff.poisonCategories["Poisons"])
                {
                    Console.WriteLine($"- Name: {poi.Name}, Type: {poi.Type}, Price per Dose: {poi.dosePrice} ");
                }
            }
        }
    }

}
