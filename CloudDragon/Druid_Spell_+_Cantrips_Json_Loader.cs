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
        public  List<DruidicCantrips> CantripCategories { get; set; }
    }

    public class DruidSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<DruidicSpells> SpellCategories { get; set; }
    }

    internal class Druid_Cantrips_Json_Loader
    {
        // DruidCantripData
        public static DruidCantripData LoaddruidCantripData(string jsonFilePath)
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
                    return new DruidCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new DruidCantripData();
                }

                var druidCantripData = JsonSerializer.Deserialize<DruidCantripData>(jsonData);

                if (druidCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new DruidCantripData();
                }

                return druidCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Druid_Spells_Json_Loader
    {
        public static DruidSpellData LoaddruidSpellData(string jsonFilePath)
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
                    return new DruidSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new DruidSpellData();
                }

                var druidSpellData = JsonSerializer.Deserialize<DruidSpellData>(jsonData);

                if (druidSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new DruidSpellData();
                }

                return druidSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
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
            if (cantripsDruid != null && cantripsDruid.CantripCategories != null)
            {
                Console.WriteLine("Druid Cantrips:");
                foreach (var druidCans in cantripsDruid.CantripCategories)
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

            var druidLevel1Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel1);
            var druidLevel2Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel2);
            var druidLevel3Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel3);
            var druidLevel4Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel4);
            var druidLevel5Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel5);
            var druidLevel6Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel6);
            var druidLevel7Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel7);
            var druidLevel8Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel8);
            var druidLevel9Spells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidLevel9);


            if (druidLevel1Spells != null && druidLevel1Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Druid Spells:");
                foreach (var spell in druidLevel1Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel2Spells != null && druidLevel2Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Druid Spells:");
                foreach (var spell in druidLevel2Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel3Spells != null && druidLevel3Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Druid Spells:");
                foreach (var spell in druidLevel3Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel4Spells != null && druidLevel4Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Druid Spells:");
                foreach (var spell in druidLevel4Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel5Spells != null && druidLevel5Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Druid Spells:");
                foreach (var spell in druidLevel5Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel6Spells != null && druidLevel6Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 6 Druid Spells:");
                foreach (var spell in druidLevel6Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel7Spells != null && druidLevel7Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 7 Druid Spells:");
                foreach (var spell in druidLevel7Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel8Spells != null && druidLevel8Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 8 Druid Spells:");
                foreach (var spell in druidLevel8Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (druidLevel9Spells != null && druidLevel9Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 9 Druid Spells:");
                foreach (var spell in druidLevel9Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }
        }
    }
}
