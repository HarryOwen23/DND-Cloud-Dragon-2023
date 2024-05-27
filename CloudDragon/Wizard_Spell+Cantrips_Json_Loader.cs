using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    // Class to represent Wizard Cantrips
    public class WizardCantrips
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

    // Class to represent Wizard Spells
    public class WizardSpells
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

    // Class to represent categories of Wizard Cantrips
    public class WizardCantripCategory
    {
        [JsonPropertyName("WizardCantrips")]
        public List<WizardCantrips> Cantrips { get; set; }
    }

    // Class to represent categories of Wizard Spells
    public class WizardSpellCategory
    {
        [JsonPropertyName("WizardSpells")]
        public List<WarlockSpells> Spells { get; set; }
    }

    // Class to represent Wizard Cantrip Data
    public class WizardCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<WizardCantripCategory> CantripCategories { get; set; }
    }

    // Class to represent Wizard Spell Data
    public class WizardSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<WizardSpellCategory> SpellCategories { get; set; }
    }

    // Class to load Wizard Warlock JSON data
    internal class WizardCantripsJsonLoader
    {
        public static WizardCantripCategory LoadWizardCantripData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new WizardCantripCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<WizardCantripCategory>(jsonData) ?? new WizardCantripCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Class to load Wizard Spell JSON data
    internal class WizardSpellsJsonLoader
    {
        public static WizardSpellCategory LoadWizardSpellData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new WizardSpellCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<WizardSpellCategory>(jsonData) ?? new WizardSpellCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    // Loader class for Wizard Cantrips
    internal class WizardCantripLoader : ILoader
    {
        private const string JsonFilePathWizardCantrip = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Cantrips.json";

        public void Load()
        {
            Console.WriteLine("Loading Wizard Cantrip Data ...");
            var warlockcantrips = WizardCantripsJsonLoader.LoadWizardCantripData(JsonFilePathWizardCantrip);

            if (warlockcantrips?.Cantrips != null)
            {
                Console.WriteLine("Wizard Cantrips:");
                foreach (var cantrip in warlockcantrips.Cantrips)
                {
                    Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
                }

            }
        }
    }

    // Loader class for Wizard Spells
    internal class WizardSpellLoader : ILoader
    {
        private const string JsonFilePathWizardSpells = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Spells.json";

        public void Load()
        {
            Console.WriteLine("Loading Wizard Spell Data ...");
            var warlockSpells = WizardSpellsJsonLoader.LoadWizardSpellData(JsonFilePathWizardSpells);

            if (warlockSpells?.Spells != null)
            {
                Console.WriteLine("Wizard Spells:");
                foreach (var spell in warlockSpells.Spells)
                {

                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

                }
            }
        }
    }
}
