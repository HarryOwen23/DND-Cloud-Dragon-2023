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
        public static ClericCantripData LoadbardCantripData(string jsonFilePath)
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
                return null;
            }
        }
    }

    internal class Cleric_Spells_Json_Loader
    {
        public static ClericSpellData LoadbardSpellData(string jsonFilePath)
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
                return null;
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

            var cantripsCleric = Cleric_Cantrips_Json_Loader.LoadbardCantripData(jsonFilePathClericCantrip);


            // Display the Armor data for Cleric Cantrips
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
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            // Define paths to the bard spell Json files
            string jsonFilePathClericLevel1 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_1_Spells.json";
            string jsonFilePathClericLevel2 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_2_Spells.json";
            string jsonFilePathClericLevel3 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_3_Spells.json";
            string jsonFilePathClericLevel4 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_4_Spells.json";
            string jsonFilePathClericLevel5 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_5_Spells.json";
            string jsonFilePathClericLevel6 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_6_Spells.json";
            string jsonFilePathClericLevel7 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_7_Spells.json";
            string jsonFilePathClericLevel8 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_8_Spells.json";
            string jsonFilePathClericLevel9 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_9_Spells.json";

            var level1clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel1);
            var level2clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel2);
            var level3clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel3);
            var level4clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel4);
            var level5clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel5);
            var level6clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel6);
            var level7clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel7);
            var level8clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel8);
            var level9clericspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathClericLevel9);


            // Display the data for Level 1 spells
            if (level1clericspells != null)
            {
                Console.WriteLine("Level 1 Cleric Spells:");
                foreach (var clericSpell1 in level1clericspells.SpellCategories["Level 1 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell1.Name}, School: {clericSpell1.School}, Description: {clericSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2clericspells != null)
            {
                Console.WriteLine("Level 2 Cleric Spells:");
                foreach (var clericSpell2 in level2clericspells.SpellCategories["Level 2 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell2.Name}, School: {clericSpell2.School}, Description: {clericSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3clericspells != null)
            {
                Console.WriteLine("Level 3 Cleric Spells:");
                foreach (var bardSpell3 in level3clericspells.SpellCategories["Level 3 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell3.Name}, School: {bardSpell3.School}, Description: {bardSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4clericspells != null)
            {
                Console.WriteLine("Level 4 Cleric Spells:");
                foreach (var clericSpell4 in level4clericspells.SpellCategories["Level 4 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell4.Name}, School: {clericSpell4.School}, Description: {clericSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5clericspells != null)
            {
                Console.WriteLine("Level 5 Cleric Spells:");
                foreach (var clericSpell5 in level5clericspells.SpellCategories["Level 5 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell5.Name}, School: {clericSpell5.School}, Description: {clericSpell5.Description} ");
                }
            }

            // Display the data for Level 6 spells
            if (level6clericspells != null)
            {
                Console.WriteLine("Level 6 Cleric Spells:");
                foreach (var clericSpell6 in level6clericspells.SpellCategories["Level 6 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell6.Name}, School: {clericSpell6.School}, Description: {clericSpell6.Description} ");
                }
            }

            // Display the data for Level 7 spells
            if (level7clericspells != null)
            {
                Console.WriteLine("Level 7 Cleric Spells:");
                foreach (var clericSpell7 in level7clericspells.SpellCategories["Level 7 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell7.Name}, School: {clericSpell7.School}, Description: {clericSpell7.Description} ");
                }
            }

            // Display the data for Level 8 spells
            if (level8clericspells != null)
            {
                Console.WriteLine("Level 8 Cleric Spells:");
                foreach (var clericSpell8 in level8clericspells.SpellCategories["Level 8 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell8.Name}, School: {clericSpell8.School}, Description: {clericSpell8.Description} ");
                }
            }

            // Display the data for Level 9 spells
            if (level9clericspells != null)
            {
                Console.WriteLine("Level 9 Cleric Spells:");
                foreach (var clericSpell9 in level9clericspells.SpellCategories["Level 9 Cleric Spells"])
                {
                    Console.WriteLine($"- Name: {clericSpell9.Name}, School: {clericSpell9.School}, Description: {clericSpell9.Description} ");
                }
            }
        }
    }
}
