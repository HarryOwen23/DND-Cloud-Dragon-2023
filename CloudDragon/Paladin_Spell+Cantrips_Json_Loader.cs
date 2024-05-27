using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    // Class to represent Paladin Cantrips
    public class PaladinCantrips
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

    // Class to represent Paladin Spells
    public class PaladinSpells
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

    // Class to represent categories of Paladin Cantrips
    public class PaladinCantripCategory
    {
        [JsonPropertyName("PaladinCantrips")]
        public List<PaladinCantrips> Cantrips { get; set; }
    }

    // Class to represent categories of Paladin Spells
    public class PaladinSpellCategory
    {
        [JsonPropertyName("PaladinSpells")]
        public List<PaladinSpells> Spells { get; set; }
    }

    // Class to represent Paladin Cantrip Data
    public class PaladinCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<PaladinCantripCategory> CantripCategories { get; set; }
    }

    // Class to represent Paladin Spell Data
    public class PaladinSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<PaladinSpellCategory> SpellCategories { get; set; }
    }

    // Class to load Paladin Paladin JSON data
    internal class PaladinCantripsJsonLoader
    {
        public static PaladinCantripCategory LoadPaladinCantripData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new PaladinCantripCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<PaladinCantripCategory>(jsonData) ?? new PaladinCantripCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Class to load Paladin Spell JSON data
    internal class PaladinSpellsJsonLoader
    {
        public static PaladinSpellCategory LoadPaladinSpellData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new PaladinSpellCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<PaladinSpellCategory>(jsonData) ?? new PaladinSpellCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Loader class for Paladin Cantrips
    internal class PaladinCantripLoader : ILoader
    {
        private const string JsonFilePathPaladinCantrip = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Cantrips.json";

        public void Load()
        {
            Console.WriteLine("Loading Paladin Cantrip Data ...");
            var paladincantrips = PaladinCantripsJsonLoader.LoadPaladinCantripData(JsonFilePathPaladinCantrip);

            if (paladincantrips?.Cantrips != null)
            {
                Console.WriteLine("Paladin Cantrips:");
                foreach (var cantrip in paladincantrips.Cantrips)
                {
                    Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
                }

            }
        }
    }

    // Loader class for Paladin Spells
    internal class PaladinSpellLoader : ILoader
    {
        private const string JsonFilePathPaladinSpells = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Spells.json";

        public void Load()
        {
            Console.WriteLine("Loading Paladin Spell Data ...");
            var palaSpells = PaladinSpellsJsonLoader.LoadPaladinSpellData(JsonFilePathPaladinSpells);

            if (palaSpells?.Spells != null)
            {
                Console.WriteLine("Paladin Spells:");
                foreach (var spell in palaSpells.Spells)
                {

                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

                }
            }
        }
    }
}
