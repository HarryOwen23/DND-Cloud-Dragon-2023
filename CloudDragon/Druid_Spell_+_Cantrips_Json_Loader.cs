using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    // Class to represent Druid Cantrips
    public class DruidCantrips
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Casting Time")]
        public string CastTime { get; set; }

        [JsonPropertyName("Range")]
        public string Range { get; set; }

        [JsonPropertyName("Components")]
        public string Components { get; set; }

        [JsonPropertyName("Duration")]
        public string Duration { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Spell Lists")]
        public List<string> SpellLists { get; set; }
    }

    // Class to represent Druid Spells
    public class DruidSpells
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

    // Class to represent categories of Druid Cantrips
    public class DruidCantripCategory
    {
        [JsonPropertyName("DruidCantrips")]
        public List<DruidCantrips> Cantrips { get; set; }
    }

    // Class to represent categories of Druid Spells
    public class DruidSpellCategory
    {
        [JsonPropertyName("DruidSpells")]
        public List<ClericSpells> Spells { get; set; }
    }

    // Class to represent Cleric Cantrip Data
    public class DruidCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<DruidCantripCategory> CantripCategories { get; set; }
    }

    // Class to represent Cleric Spell Data
    public class DruidSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<DruidSpellCategory> SpellCategories { get; set; }
    }

    // Class to load Cleric Cantrip JSON data
    internal class DruidCantripsJsonLoader
    {
        public static DruidCantripCategory LoadDruidCantripData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new DruidCantripCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<DruidCantripCategory>(jsonData) ?? new DruidCantripCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Class to load Cleric Spell JSON data
    internal class DruidSpellsJsonLoader
    {
        public static DruidSpellCategory LoadDruidSpellData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new DruidSpellCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<DruidSpellCategory>(jsonData) ?? new DruidSpellCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Loader class for Druid Cantrips
    internal class DruidCantripLoader : ILoader
    {
        private const string JsonFilePathDruidCantrip = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Cantrips.json";

        public void Load()
        {
            Console.WriteLine("Loading Druid Cantrip Data ...");
            var druidcantrips = DruidCantripsJsonLoader.LoadDruidCantripData(JsonFilePathDruidCantrip);

            if (druidcantrips?.Cantrips != null)
            {
                Console.WriteLine("Druid Cantrips:");
                foreach (var cantrip in druidcantrips.Cantrips)
                {
                    Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
                }

            }
        }
    }

    // Loader class for Druid Spells
    internal class DruidSpellLoader : ILoader
    {
        private const string JsonFilePathDruidSpells = "Spells+Cantrips\\Druid_Cantrips_+_Spells\\Druid_Spells.json";

        public void Load()
        {
            Console.WriteLine("Loading Druid Spell Data ...");
            var druidSpells = DruidSpellsJsonLoader.LoadDruidSpellData(JsonFilePathDruidSpells);

            if (druidSpells?.Spells != null)
            {
                Console.WriteLine("Druid Spells:");
                foreach (var spell in druidSpells.Spells)
                {

                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

                }
            }
        }
    }
}
