using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class BardicCantrips
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Casting Time")]
        public string Cast_Time { get; set; }

        [JsonPropertyName("Range")]
        public string Range { get; set; }

        [JsonPropertyName("Components")]
        public string Components { get; set; }

        [JsonPropertyName("Duration")]
        public string Duration { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Spell Lists")]
        public string Spell_Lists { get; set; }
    }
    public class BardicSpells
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class BardCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<BardicCantrips> Cantrips { get; set; }
    }

    public class BardSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<BardicSpells> Spells { get; set; }
    }

    public class BardCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public Dictionary<string, List<BardicCantrips>> CantripCategories { get; set; }
    }

    public class BardSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public Dictionary<string, List<BardicSpells>> SpellCategories { get; set; }
    }

    internal class Bard_Cantrips_Json_Loader
    {
        public static BardCantripData LoadbardCantripData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var bardCantripCategory = JsonSerializer.Deserialize<BardCantripcategory>(jsonData);
                return new BardCantripData
                {
                    CantripCategories = new Dictionary<string, List<BardicCantrips>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), bardCantripCategory.Cantrips }
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

    internal class Bard_Spells_Json_Loader
    {
        public static BardSpellData LoadbardSpellData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var bardSpellCategory = JsonSerializer.Deserialize<BardSpellcategory>(jsonData);
                return new BardSpellData
                {
                    SpellCategories = new Dictionary<string, List<BardicSpells>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), bardSpellCategory.Spells }
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

    internal class BardCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathBardCantrip = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Cantrips.json";

            var cantripsBard = Bard_Cantrips_Json_Loader.LoadbardCantripData(jsonFilePathBardCantrip);


            // Display the Armor data for Bard Cantrips
            if (cantripsBard != null)
            {
                Console.WriteLine("Bard Cantrips:");
                foreach (var bardCans in cantripsBard.CantripCategories["Bard Cantrips"])
                {
                    Console.WriteLine($"- Name: {bardCans.Name}, Source: {bardCans.Source}, School: {bardCans.School}, Cast_Time: {bardCans.Cast_Time}, Components: {bardCans.Components}, Duration: {bardCans.Duration}, Description: {bardCans.Description}, Spell_Lists: {bardCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class BardSpellLoader : ILoader
    {
        private static void DisplayBardicSpells(string spellLevel, Dictionary<string, List<BardicSpells>> spellCategories)
        {
            Console.WriteLine($"Level {spellLevel} Bard Spells:");
            foreach (var bardSpell in spellCategories[$"Level {spellLevel} Bard Spells"])
            {
                Console.WriteLine($"- Name: {bardSpell.Name}, School: {bardSpell.School}, Description: {bardSpell.Description} ");
            }
        }

        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            // Define paths to the bard spell Json files
            string[] jsonFilePaths = new string[]
            {
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_1_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_2_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_3_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_4_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_5_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_6_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_7_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_8_Spells.json",
            "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_9_Spells.json"
            };

            Dictionary<string, List<BardicSpells>>[] bardSpellData = new Dictionary<string, List<BardicSpells>>[jsonFilePaths.Length];

            for (int i = 0; i < jsonFilePaths.Length; i++)
            {
                bardSpellData[i] = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePaths[i])?.SpellCategories;
            }

            // Display bard spell data for each level
            for (int i = 0; i < bardSpellData.Length; i++)
            {
                if (bardSpellData[i] != null)
                {
                    DisplayBardicSpells((i + 1).ToString(), bardSpellData[i]);
                }
            }
        }
    }
}
