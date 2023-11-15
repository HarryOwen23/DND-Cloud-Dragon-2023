using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class DruidicCantrips
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
    public class DruidicSpells
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class DruidCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<DruidicCantrips> Cantrips { get; set; }
    }

    public class DruidSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<DruidicSpells> Spells { get; set; }
    }

    public class DruidCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public Dictionary<string, List<DruidicCantrips>> CantripCategories { get; set; }
    }

    public class DruidSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public Dictionary<string, List<DruidicSpells>> SpellCategories { get; set; }
    }

    internal class Druid_Cantrips_Json_Loader
    {
        public static DruidCantripData LoaddruidCantripData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var druidCantripCategory = JsonSerializer.Deserialize<DruidCantripcategory>(jsonData);
                return new DruidCantripData
                {
                    CantripCategories = new Dictionary<string, List<DruidicCantrips>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), druidCantripCategory.Cantrips }
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

    internal class Druid_Spells_Json_Loader
    {
        public static DruidSpellData LoadbardSpellData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var druidSpellCategory = JsonSerializer.Deserialize<DruidSpellcategory>(jsonData);
                return new DruidSpellData
                {
                    SpellCategories = new Dictionary<string, List<DruidicSpells>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), druidSpellCategory.Spells }
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

    internal class DruidCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Druid Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathDruidCantrip = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Cantrips.json";

            var cantripsDruid = Druid_Cantrips_Json_Loader.LoaddruidCantripData(jsonFilePathDruidCantrip);


            // Display the Armor data for Druid Cantrips
            if (cantripsDruid != null)
            {
                Console.WriteLine("Druid Cantrips:");
                foreach (var druidCans in cantripsDruid.CantripCategories["Druid Cantrips"])
                {
                    Console.WriteLine($"- Name: {druidCans.Name}, Source: {druidCans.Source}, School: {druidCans.School}, Cast_Time: {druidCans.Cast_Time}, Components: {druidCans.Components}, Duration: {druidCans.Duration}, Description: {druidCans.Description}, Spell_Lists: {druidCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class DruidSpellLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Druid Spell Data");
            // Define paths to the druid spell Json files
            string jsonFilePathDruidLevel1 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_1_Spells.json";
            string jsonFilePathDruidLevel2 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_2_Spells.json";
            string jsonFilePathDruidLevel3 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_3_Spells.json";
            string jsonFilePathDruidLevel4 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_4_Spells.json";
            string jsonFilePathDruidLevel5 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_5_Spells.json";
            string jsonFilePathDruidLevel6 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_6_Spells.json";
            string jsonFilePathDruidLevel7 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_7_Spells.json";
            string jsonFilePathDruidLevel8 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_8_Spells.json";
            string jsonFilePathDruidLevel9 = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Level_9_Spells.json";

            var level1druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel1);
            var level2druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel2);
            var level3druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel3);
            var level4druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel4);
            var level5druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel5);
            var level6druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel6);
            var level7druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel7);
            var level8druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel8);
            var level9druidspells = Druid_Spells_Json_Loader.LoadbardSpellData(jsonFilePathDruidLevel9);


            // Display the data for Level 1 spells
            if (level1druidspells != null)
            {
                Console.WriteLine("Level 1 Druid Spells:");
                foreach (var druidSpell1 in level1druidspells.SpellCategories["Level 1 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell1.Name}, School: {druidSpell1.School}, Description: {druidSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2druidspells != null)
            {
                Console.WriteLine("Level 2 Druid Spells:");
                foreach (var druidSpell2 in level2druidspells.SpellCategories["Level 2 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell2.Name}, School: {druidSpell2.School}, Description: {druidSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3druidspells != null)
            {
                Console.WriteLine("Level 3 Druid Spells:");
                foreach (var druidSpell3 in level3druidspells.SpellCategories["Level 3 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell3.Name}, School: {druidSpell3.School}, Description: {druidSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4druidspells != null)
            {
                Console.WriteLine("Level 4 Druid Spells:");
                foreach (var druidSpell4 in level4druidspells.SpellCategories["Level 4 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell4.Name}, School: {druidSpell4.School}, Description: {druidSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5druidspells != null)
            {
                Console.WriteLine("Level 5 Druid Spells:");
                foreach (var druidSpell5 in level5druidspells.SpellCategories["Level 5 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell5.Name}, School: {druidSpell5.School}, Description: {druidSpell5.Description} ");
                }
            }

            // Display the data for Level 6 spells
            if (level6druidspells != null)
            {
                Console.WriteLine("Level 6 Druid Spells:");
                foreach (var bardSpell6 in level6druidspells.SpellCategories["Level 6 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell6.Name}, School: {bardSpell6.School}, Description: {bardSpell6.Description} ");
                }
            }

            // Display the data for Level 7 spells
            if (level7druidspells != null)
            {
                Console.WriteLine("Level 7 Druid Spells:");
                foreach (var druidSpell7 in level7druidspells.SpellCategories["Level 7 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell7.Name}, School: {druidSpell7.School}, Description: {druidSpell7.Description} ");
                }
            }

            // Display the data for Level 8 spells
            if (level8druidspells != null)
            {
                Console.WriteLine("Level 8 Druid Spells:");
                foreach (var druidSpell8 in level8druidspells.SpellCategories["Level 8 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell8.Name}, School: {druidSpell8.School}, Description: {druidSpell8.Description} ");
                }
            }

            // Display the data for Level 9 spells
            if (level9druidspells != null)
            {
                Console.WriteLine("Level 9 Druid Spells:");
                foreach (var druidSpell9 in level9druidspells.SpellCategories["Level 9 Druid Spells"])
                {
                    Console.WriteLine($"- Name: {druidSpell9.Name}, School: {druidSpell9.School}, Description: {druidSpell9.Description} ");
                }
            }
        }
    }
}
