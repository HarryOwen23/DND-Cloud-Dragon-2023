using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    // Class to represent Magic Items
    public class MagicItems
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attunement")]
        public string Attunement { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("rarity")]
        public string Rarity { get; set; }
    }

    // Class to represent categories of Magic Items
    public class MICategory
    {
        [JsonPropertyName("magicalItems")]
        public List<MagicItems> MagicalItems { get; set; }
    }

    // Class to represent Magical Item Data
    public class MagicalItemData
    {
        [JsonPropertyName("magicalItemCategories")]
        public List<MICategory> MiCategories { get; set; }
    }

    // Class to load Magical Item JSON data
    internal class MagicalItemsJsonLoader
    {
        public static MagicalItemData LoadMagicalItemData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new MagicalItemData();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<MagicalItemData>(jsonData) ?? new MagicalItemData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Loader class for Magical Items
    internal class MagicalItemLoader : ILoader
    {
        private const string JsonFilePathMagItems = "Magical Items\\Magical_Items.json";

        public void Load()
        {
            Console.WriteLine("Loading Magical Item Data ...");
            var magItems = MagicalItemsJsonLoader.LoadMagicalItemData(JsonFilePathMagItems);

            if (magItems?.MiCategories != null)
            {
                Console.WriteLine("Magical Items:");
                foreach (var category in magItems.MiCategories)
                {
                    foreach (var magicItem in category.MagicalItems)
                    {
                        Console.WriteLine($"- Name: {magicItem.Name}, Type: {magicItem.Type}, Attunement: {magicItem.Attunement}, Description: {magicItem.Description}, Rarity: {magicItem.Rarity}");
                    }
                }
            }
        }
    }

}
