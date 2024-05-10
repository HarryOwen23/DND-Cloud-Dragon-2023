using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class BardicCantrips
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

    public class BardCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<BardicCantrips> Cantrips { get; set; }
    }

    public class BardSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<BardicSpells> Spells { get; set; }
    }

    public class BardCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<BardicCantrips> CantripCategories { get; set; }
    }

    public class BardSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<BardicSpells> SpellCategories { get; set; }
    }

    internal class Bard_Cantrips_Json_Loader
    {
        // BardCantripData
        public static BardCantripData LoadbardCantripData(string jsonFilePath)
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
                    return new BardCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new BardCantripData();
                }

                var bardCantripData = JsonSerializer.Deserialize<BardCantripData>(jsonData);

                if (bardCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new BardCantripData();
                }

                return bardCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Bard_Spells_Json_Loader
    {
        public static BardSpellData LoadbardSpellData(string jsonFilePath)
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
                    return new BardSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new BardSpellData();
                }

                var bardSpellData = JsonSerializer.Deserialize<BardSpellData>(jsonData);

                if (bardSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new BardSpellData();
                }

                return bardSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class BardCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathBardCantrip = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Cantrips.json";

            var cantripsBard = Bard_Cantrips_Json_Loader.LoadbardCantripData(jsonFilePathBardCantrip);


            // Display the data for Bard Cantrips
            if (cantripsBard != null && cantripsBard.CantripCategories != null)
            {
                Console.WriteLine("Bard Cantrips:");
                foreach (var bardCans in cantripsBard.CantripCategories)
                {
                    Console.WriteLine($"- Name: {bardCans.Name}, Source: {bardCans.Source}, School: {bardCans.School}, Cast_Time: {bardCans.Cast_Time}, Components: {bardCans.Components}, Duration: {bardCans.Duration}, Description: {bardCans.Description}, Spell_Lists: {bardCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class BardSpellLoader : ILoader
    {
        private static void DisplayBardicSpells(string spellLevel, List<BardicSpells> spellCategories)
        {
            Console.WriteLine($"Level {spellLevel} Bard Spells:");
            foreach (var bardSpell in spellCategories)
            {
                Console.WriteLine($"- Name: {bardSpell.Name}, School: {bardSpell.School}, Description: {bardSpell.Description}, Level: {bardSpell.Level} ");
            }
        }
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            string jsonFilePathBardSpells = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Spells.json";

            // Load the bard spell data using the Bard spell JsonLoader
            var bardSpells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpells);

            if (bardSpells != null && bardSpells.SpellCategories != null)
            {
                Console.WriteLine("Bard Spells:");
                foreach (var spell in bardSpells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");
                }
            }
        }

    }
}
