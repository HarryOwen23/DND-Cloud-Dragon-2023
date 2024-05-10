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

        [JsonPropertyName("Level")]
        public int Level { get; set; }
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
            string jsonFilePathDruidSpells = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Spells.json";
            

            var druidLevelSpells = Druid_Spells_Json_Loader.LoaddruidSpellData(jsonFilePathDruidSpells);



            if (druidLevelSpells != null && druidLevelSpells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Druid Spells:");
                foreach (var spell in druidLevelSpells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");
                }
            }
        }
    }
}
