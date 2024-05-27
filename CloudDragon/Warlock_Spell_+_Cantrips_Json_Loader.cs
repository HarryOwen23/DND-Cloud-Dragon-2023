using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    // Class to represent Warlock Cantrips
    public class WarlockCantrips
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

    // Class to represent Warlock Spells
    public class WarlockSpells
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

    // Class to represent categories of Warlock Cantrips
    public class WarlockCantripCategory
    {
        [JsonPropertyName("WarlockCantrips")]
        public List<WarlockCantrips> Cantrips { get; set; }
    }

    // Class to represent categories of Warlock Spells
    public class WarlockSpellCategory
    {
        [JsonPropertyName("WarlockSpells")]
        public List<WarlockSpells> Spells { get; set; }
    }

    // Class to represent Warlock Cantrip Data
    public class WarlockCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<WarlockCantripCategory> CantripCategories { get; set; }
    }

    // Class to represent Warlock Spell Data
    public class WarlockSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<WarlockSpellCategory> SpellCategories { get; set; }
    }

    // Class to load Warlock Warlock JSON data
    internal class WarlockCantripsJsonLoader
    {
        public static WarlockCantripCategory LoadWarlockCantripData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new WarlockCantripCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<WarlockCantripCategory>(jsonData) ?? new WarlockCantripCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Class to load Warlock Spell JSON data
    internal class WarlockSpellsJsonLoader
    {
        public static WarlockSpellCategory LoadWarlockSpellData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new WarlockSpellCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<WarlockSpellCategory>(jsonData) ?? new WarlockSpellCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Loader class for Warlock Cantrips
    internal class WarlockCantripLoader : ILoader
    {
        private const string JsonFilePathWarlockCantrip = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Cantrips.json";

        public void Load()
        {
            Console.WriteLine("Loading Warlock Cantrip Data ...");
            var warlockcantrips = WarlockCantripsJsonLoader.LoadWarlockCantripData(JsonFilePathWarlockCantrip);

            if (warlockcantrips?.Cantrips != null)
            {
                Console.WriteLine("Warlock Cantrips:");
                foreach (var cantrip in warlockcantrips.Cantrips)
                {
                    Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
                }

            }
        }
    }

    // Loader class for Warlock Spells
    internal class WarlockSpellLoader : ILoader
    {
        private const string JsonFilePathWarlockSpells = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Spells.json";

        public void Load()
        {
            Console.WriteLine("Loading Warlock Spell Data ...");
            var warlockSpells = WarlockSpellsJsonLoader.LoadWarlockSpellData(JsonFilePathWarlockSpells);

            if (warlockSpells?.Spells != null)
            {
                Console.WriteLine("Warlock Spells:");
                foreach (var spell in warlockSpells.Spells)
                {

                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

                }
            }
        }
    }
}
