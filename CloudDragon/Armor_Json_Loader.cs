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
                var lightCheck = armorMedium?.ArmorCategories ?? new List<Armor>();

                // You can then iterate over heavyCheck as needed
                foreach (var armor in heavyCheck)
                {
                    // Access properties with null checks
                    Console.WriteLine($"- Name: {armor?.Name}, ArmorClass: {armor?.ArmorClass}, Strength: {armor?.Strength}, Stealth: {armor?.Stealth}, Weight: {armor?.Weight}, Cost: {armor?.Cost} ");
                }

                // You can then iterate over heavyCheck as needed
                foreach (var armor in mediumCheck)
                {
                    // Access properties with null checks
                    Console.WriteLine($"- Name: {armor?.Name}, ArmorClass: {armor?.ArmorClass}, Strength: {armor?.Strength}, Stealth: {armor?.Stealth}, Weight: {armor?.Weight}, Cost: {armor?.Cost} ");
                }

                // You can then iterate over heavyCheck as needed
                foreach (var armor in lightCheck)
                {
                    // Access properties with null checks
                    Console.WriteLine($"- Name: {armor?.Name}, ArmorClass: {armor?.ArmorClass}, Strength: {armor?.Strength}, Stealth: {armor?.Stealth}, Weight: {armor?.Weight}, Cost: {armor?.Cost} ");
                }

            // Display the data for Light Armor
            if (armorLight != null && armorLight.ArmorCategories != null)
                {
                    Console.WriteLine("Light Armor:");
                    foreach (var armor in armorLight.ArmorCategories)
                    {
                        Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strength: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                    }
                }

                if (armorMedium != null && armorMedium.ArmorCategories != null)
                {
                    Console.WriteLine("Medium Armor:");
                    foreach (var armor in armorMedium.ArmorCategories)
                    {
                        Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strength: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                    }
                }

                if (armorHeavy != null && armorHeavy.ArmorCategories != null)
                {
                    Console.WriteLine("Heavy Armor:");
                    foreach (var armor in armorHeavy.ArmorCategories)
                    {
                        Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strength: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                    }
                }
            }
       }
}
