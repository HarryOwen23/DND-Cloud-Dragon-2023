using System;
using System.Collections.Generic;
using System.Drawing;
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
        [JsonPropertyName("Armor Categories")]
        public List<Armor> ArmorCategories { get; set; }
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
        private void DisplayArmorCategory(List<Armor> armorCategories, string categoryTitle)
        {
            if (armorCategories != null)
            {
                Console.WriteLine($"{categoryTitle} Armor:");
                foreach (var armor in armorCategories)
                {
                    Console.WriteLine($"- Name: {armor?.Name}, ArmorClass: {armor?.ArmorClass}, Strength: {armor?.Strength}, Stealth: {armor?.Stealth}, Weight: {armor?.Weight}, Cost: {armor?.Cost}");
                }
            }
        }

        public void Load()
        {
            Console.WriteLine("Loading Armor Data ...");

            // Define the paths to the JSON files
            string jsonFilePathArmorHeavy = "Armor\\Armor_Heavy.json";
            string jsonFilePathArmorMedium = "Armor\\Armor_Medium.json";
            string jsonFilePathArmorLight = "Armor\\Armor_Light.json";

            var armorHeavy = ArmorJsonLoader.LoadArmorData(jsonFilePathArmorHeavy);
            var armorMedium = ArmorJsonLoader.LoadArmorData(jsonFilePathArmorMedium);
            var armorLight = ArmorJsonLoader.LoadArmorData(jsonFilePathArmorLight);

            // Perform null checks before accessing properties
            var heavyCheck = armorHeavy?.ArmorCategories ?? new List<Armor>();
            var mediumCheck = armorMedium?.ArmorCategories ?? new List<Armor>();
            var lightCheck = armorLight?.ArmorCategories ?? new List<Armor>();

            // Display armor categories using the shared method
            DisplayArmorCategory(heavyCheck, "Heavy");
            DisplayArmorCategory(mediumCheck, "Medium");
            DisplayArmorCategory(lightCheck, "Light");
        }
    }
}
