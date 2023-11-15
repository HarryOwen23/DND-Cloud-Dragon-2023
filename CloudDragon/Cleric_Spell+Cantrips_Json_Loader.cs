using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class ClericCantrips
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
    public class ClericSpells
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class ClericCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<ClericCantrips> Cantrips { get; set; }
    }

    public class ClericSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<ClericSpells> Spells { get; set; }
    }

    public class ClericCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public Dictionary<string, List<ClericCantrips>> CantripCategories { get; set; }
    }

    public class ClericSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public Dictionary<string, List<ClericSpells>> SpellCategories { get; set; }
    }

    internal class Cleric_Cantrips_Json_Loader
    {
        public static ClericCantripData LoadclericCantripData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var clericCantripCategory = JsonSerializer.Deserialize<ClericCantripcategory>(jsonData);
                return new ClericCantripData
                {
                    CantripCategories = new Dictionary<string, List<ClericCantrips>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), clericCantripCategory.Cantrips }
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

    internal class Cleric_Spells_Json_Loader
    {
        public static ClericSpellData LoadclericSpellData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var clericSpellCategory = JsonSerializer.Deserialize<ClericSpellcategory>(jsonData);
                return new ClericSpellData
                {
                    SpellCategories = new Dictionary<string, List<ClericSpells>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), clericSpellCategory.Spells }
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

    internal class ClericCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Cleric Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathClericCantrip = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Cantrips.json";

            var cantripsCleric = Cleric_Cantrips_Json_Loader.LoadclericCantripData(jsonFilePathClericCantrip);


            // Display the data for Cleric Cantrips
            if (cantripsCleric != null)
            {
                Console.WriteLine("Cleric Cantrips:");
                foreach (var clericCans in cantripsCleric.CantripCategories["Cleric Cantrips"])
                {
                    Console.WriteLine($"- Name: {clericCans.Name}, Source: {clericCans.Source}, School: {clericCans.School}, Cast_Time: {clericCans.Cast_Time}, Components: {clericCans.Components}, Duration: {clericCans.Duration}, Description: {clericCans.Description}, Spell_Lists: {clericCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class ClericSpellLoader : ILoader
    {
        private static void DisplayClericSpells(string spellLevel, Dictionary<string, List<ClericSpells>> spellCategories)
        {
            Console.WriteLine($"Level {spellLevel} Cleric Spells:");
            foreach (var clericSpell in spellCategories[$"Level {spellLevel} Cleric Spells"])
            {
                Console.WriteLine($"- Name: {clericSpell.Name}, School: {clericSpell.School}, Description: {clericSpell.Description} ");
            }
        }

        void ILoader.Load()
        {
            Console.WriteLine("Loading Cleric Spell Data");
            // Define paths to the cleric spell Json files
            string[] jsonFilePaths = new string[]
            {
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_1_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_2_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_3_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_4_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_5_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_6_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_7_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_8_Spells.json",
            "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_9_Spells.json"
            };

            Dictionary<string, List<ClericSpells>>[] clericSpellData = new Dictionary<string, List<ClericSpells>>[jsonFilePaths.Length];

            for (int i = 0; i < jsonFilePaths.Length; i++)
            {
                clericSpellData[i] = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePaths[i])?.SpellCategories;
            }

            // Display cleric spell data for each level
            for (int i = 0; i < clericSpellData.Length; i++)
            {
                if (clericSpellData[i] != null)
                {
                    DisplayClericSpells((i + 1).ToString(), clericSpellData[i]);
                }
            }
        }
    }
}
