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

        public Armor()
        {
            Name = string.Empty;
            ArmorClass = string.Empty;
            Strength = string.Empty;
            Stealth = string.Empty;
            Weight = string.Empty;
            Cost = string.Empty;
        }
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
        public Dictionary<string, List<Armor>> ArmorCategories { get; set; }

        public ArmorData()
        {
            ArmorCategories = new Dictionary<string, List<Armor>>();
        }
    }



    internal class ArmorJsonLoader
    {
        public static ArmorData LoadArmorData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var armorCategory = JsonSerializer.Deserialize<ArmorCategory>(jsonData);

                if (armorCategory?.Armors != null)
                {
                    return new ArmorData
                    {
                        ArmorCategories = new Dictionary<string, List<Armor>>
                {
                    { Path.GetFileNameWithoutExtension(jsonFilePath), armorCategory.Armors }
                }
                    };
                }
                else
                {
                    Console.WriteLine($"Error loading JSON file: Invalid or empty data in {jsonFilePath}.");
                    return new ArmorData(); // Return an empty ArmorData instance
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }

        internal class ArmorLoader : ILoader
        {
            private static void DisplayArmorData(string armorType, Dictionary<string, List<Armor>>? armorCategories)
            {
                Console.WriteLine($"{armorType} Armor:");

                if (armorCategories != null && armorCategories.TryGetValue(armorType, out var armors))
                {
                    foreach (var armor in armors)
                    {
                        Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strength: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                    }
                }
                else
                {
                    Console.WriteLine($"No data found for {armorType} Armor.");
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


                // Display the Armor data for Heavy Armor
                DisplayArmorData("Heavy Armor", armorHeavy?.ArmorCategories);

                // Display the Armor data for Medium Armor
                DisplayArmorData("Medium Armor", armorMedium?.ArmorCategories);

                // Display the Armor data for Light Armor
                DisplayArmorData("Light Armor", armorLight?.ArmorCategories);

            }
        }
    }
}