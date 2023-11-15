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
        [JsonPropertyName("Armor Class")]
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
    }

    public class ArmorData
    {
        [JsonPropertyName("Armor Categories")]
        public Dictionary<string, List<Armor>> ArmorCategories { get; set; }
    }

    internal class Armor_Json_Loader
    {
        public static ArmorData LoadArmorData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var armorCategory = JsonSerializer.Deserialize<ArmorCategory>(jsonData);
                return new ArmorData
                {
                    ArmorCategories = new Dictionary<string, List<Armor>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), armorCategory.Armors }
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

    internal class ArmorLoader : ILoader
    {
        private static void DisplayArmorData(string armorType, Dictionary<string, List<Armor>> armorCategories)
        {
            Console.WriteLine($"{armorType} Armor:");
            foreach (var armor in armorCategories[armorType])
            {
                Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strength: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
            }
        }

        void ILoader.Load()
        {
            Console.WriteLine("Loading Armor Data ...");
            // Define the paths to the JSON files
            string jsonFilePathArmorHeavy = "Armor\\Armor_Heavy.json";
            string jsonFilePathArmorMedium = "Armor\\Armor_Medium.json";
            string jsonFilePathArmorLight = "Armor\\Armor_Light.json";

            var armorHeavy = Armor_Json_Loader.LoadArmorData(jsonFilePathArmorHeavy);
            var armorMedium = Armor_Json_Loader.LoadArmorData(jsonFilePathArmorMedium);
            var armorLight = Armor_Json_Loader.LoadArmorData(jsonFilePathArmorLight);

            // Display the Armor data for Heavy Armor
            if (armorHeavy != null)
            {
                DisplayArmorData("Heavy Armor", armorHeavy.ArmorCategories);
            }

            // Display the Armor data for Medium Armor
            if (armorMedium != null)
            {
                DisplayArmorData("Medium Armor", armorMedium.ArmorCategories);
            }

            // Display the Armor data for Light Armor
            if (armorLight != null)
            {
                DisplayArmorData("Light Armor", armorLight.ArmorCategories);
            }
        }
    }
}