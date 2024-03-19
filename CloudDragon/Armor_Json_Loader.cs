using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Armor
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Armor Class (AC)")]
        public string ArmorClass { get; set; }

        [JsonPropertyName("Strength")]
        public string Strength { get; set; }

        [JsonPropertyName("Stealth")]
        public string Stealth { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }
    }

    public class ArmorCategory
    {
        [JsonPropertyName("Armors")]
        public List<Armor> Armors { get; set; }

        public ArmorCategory()
        {
            Armors = new List<Armor>();
        }
    }

    public class ArmorData
    {

        [JsonPropertyName("Heavy Armor")]
        public List<Armor> HeavyArmor { get; set; }

        [JsonPropertyName("Medium Armor")]
        public List<Armor> MediumArmor { get; set; }

        [JsonPropertyName("Light Armor")]
        public List<Armor> LightArmor { get; set; }

        [JsonPropertyName("Armor Categories")]
        public List<ArmorCategory> ArmorCategories { get; set; }
    }

    internal class ArmorJsonLoader
    {
        public static ArmorData LoadArmorData(string jsonFilePath)
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
                    return new ArmorData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new ArmorData();
                }

                var armorData = JsonSerializer.Deserialize<ArmorData>(jsonData);

                if (armorData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default ArmorData.");
                    return new ArmorData();
                }

                return armorData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class ArmorLoader : ILoader
    {
        private void DisplayArmorCategory(IEnumerable<Armor> armorList, string categoryTitle)
        {
            Console.WriteLine($"Displaying {categoryTitle} Armor...");

            if (armorList != null)
            {
                Console.WriteLine($"{categoryTitle} Armor:");
                foreach (var armor in armorList)
                {
                    Console.WriteLine($"- Name: {armor?.Name}, ArmorClass: {armor?.ArmorClass}, Strength: {armor?.Strength}, Stealth: {armor?.Stealth}, Weight: {armor?.Weight}, Cost: {armor?.Cost}");
                }
            }
        }

        public void Load()
        {
            Console.WriteLine("Loading Armor Data ...");

            string baseDirectory = Directory.GetCurrentDirectory();
            string jsonFilePathArmorHeavy = Path.Combine(baseDirectory, "Armor", "Armor_Heavy.json");
            string jsonFilePathArmorMedium = Path.Combine(baseDirectory, "Armor", "Armor_Medium.json");
            string jsonFilePathArmorLight = Path.Combine(baseDirectory, "Armor", "Armor_Light.json");

            var armorDataHeavy = ArmorJsonLoader.LoadArmorData(jsonFilePathArmorHeavy);
            var armorDataMedium = ArmorJsonLoader.LoadArmorData(jsonFilePathArmorMedium);
            var armorDataLight = ArmorJsonLoader.LoadArmorData(jsonFilePathArmorLight);

            // Display armor categories using the shared method
            DisplayArmorCategory(armorDataHeavy?.HeavyArmor, "Heavy");
            DisplayArmorCategory(armorDataMedium?.MediumArmor, "Medium");
            DisplayArmorCategory(armorDataLight?.LightArmor, "Light");
        }
    }
}
