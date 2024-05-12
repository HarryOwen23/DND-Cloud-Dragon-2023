using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class MagicItems
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Attunement")]
        public string Attunement { get; set; }

        [JsonPropertyName("Description")]
        public string Description{ get; set; }

        [JsonPropertyName("Rarity")]
        public string Rarity { get; set; }
    }

    public class MICategory
    {
        [JsonPropertyName("magicalItems")]
        public List<MagicItems> MagicalItems { get; set; }
    }

    public class MagicalItemData
    {
        [JsonPropertyName("Magical Item Categories")]
        public List<MagicItems> MiCategories { get; set; }
    }

    internal class Magical_Items_Json_Loader
    {
        public static MagicalItemData LoadMagicalItemData(string jsonFilePath)
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
                    return new MagicalItemData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new MagicalItemData();
                }

                var magicalItemData = JsonSerializer.Deserialize<MagicalItemData>(jsonData);

                if (magicalItemData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new MagicalItemData();
                }

                return magicalItemData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; 
            }
        }
    }

    internal class MagicalItemLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Magical Item Data ...");

            string jsonFilePathCommon = "Magical Items\\Magical_Items.json";
            

            var commonItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathCommon);


            // Display the Armor data for common magical items
            if (commonItems != null && commonItems.MiCategories != null)
            {
                Console.WriteLine("Common Magical Items:");
                foreach (var magicitems in commonItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description}, Rarity {magicitems.Rarity} ");
                }
            }
        }
    }
}
