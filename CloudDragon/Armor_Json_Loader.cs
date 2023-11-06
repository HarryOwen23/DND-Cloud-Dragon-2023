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
                return null;
            }
        }
    }

    internal class ArmorLoader : ILoader
    {
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
                Console.WriteLine("Heavy Armor:");
                foreach (var armor in armorHeavy.ArmorCategories["Heavy Armor"])
                {
                    Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strenght: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                }
            }

            // Display the Armor data for Medium Armor
            if (armorMedium != null)
            {
                Console.WriteLine("Medium Armor:");
                foreach (var armor in armorMedium.ArmorCategories["Medium Armor"])
                {
                    Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strenght: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                }
            }

            // Display the Armor data for Light Armor
            if (armorLight != null)
            {
                Console.WriteLine("Medium Armor:");
                foreach (var armor in armorLight.ArmorCategories["Light Armor"])
                {
                    Console.WriteLine($"- Name: {armor.Name}, ArmorClass: {armor.ArmorClass}, Strenght: {armor.Strength}, Stealth: {armor.Stealth}, Weight: {armor.Weight}, Cost: {armor.Cost} ");
                }
            }
        }
    }
}