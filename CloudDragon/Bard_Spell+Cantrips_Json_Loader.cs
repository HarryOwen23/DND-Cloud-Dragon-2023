using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    // Class to represent Bardic Cantrips
    public class BardicCantrips
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

    // Class to represent Bardic Spells
    public class BardicSpells
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

    // Class to represent categories of Bardic Cantrips
    public class BardCantripCategory
    {
        [JsonPropertyName("BardCantrips")]
        public List<BardicCantrips> Cantrips { get; set; }
    }

    // Class to represent categories of Bardic Spells
    public class BardSpellCategory
    {
        [JsonPropertyName("BardSpells")]
        public List<BardicSpells> Spells { get; set; }
    }

    // Class to represent Bard Cantrip Data
    public class BardCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<BardCantripCategory> CantripCategories { get; set; }
    }

    // Class to represent Bard Spell Data
    public class BardSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<BardSpellCategory> SpellCategories { get; set; }
    }

    // Class to load Bard Cantrip JSON data
    internal class BardCantripsJsonLoader
    {
        public static BardCantripCategory LoadBardCantripData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new BardCantripCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<BardCantripCategory>(jsonData) ?? new BardCantripCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Class to load Bard Spell JSON data
    internal class BardSpellsJsonLoader
    {
        public static BardSpellCategory LoadBardSpellData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new BardSpellCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<BardSpellCategory>(jsonData) ?? new BardSpellCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Loader class for Bard Cantrips
    internal class BardCantripLoader : ILoader
    {
        private const string JsonFilePathBardCantrip = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Cantrips.json";

        public void Load()
        {
            Console.WriteLine("Loading Bard Cantrip Data ...");
            var bardcantrips = BardCantripsJsonLoader.LoadBardCantripData(JsonFilePathBardCantrip);

            if (bardcantrips?.Cantrips != null)
            {
                Console.WriteLine("Bard Cantrips:");
                foreach (var cantrip in bardcantrips.Cantrips)
                {
                   Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
                }
                
            }
        }
    }

    // Loader class for Bard Spells
    internal class BardSpellLoader : ILoader
    {
        private const string JsonFilePathBardSpells = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Spells.json";

        public void Load()
        {
            Console.WriteLine("Loading Bard Spell Data ...");
            var bardSpells = BardSpellsJsonLoader.LoadBardSpellData(JsonFilePathBardSpells);

            if (bardSpells?.Spells != null)
            {
                Console.WriteLine("Bard Spells:");
                foreach (var spell in bardSpells.Spells)
                {

                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

                }
            }
        }
    }
}
