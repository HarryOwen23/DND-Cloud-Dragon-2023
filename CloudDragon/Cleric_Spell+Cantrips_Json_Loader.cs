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

        [JsonPropertyName("Level")]
        public int Level { get; set; }
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
        public List<ClericCantrips> CantripCategories { get; set; }
    }

    public class ClericSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<ClericSpells> SpellCategories { get; set; }
    }

    internal class Cleric_Cantrips_Json_Loader
    {
        public static ClericCantripData LoadclericCantripData(string jsonFilePath)
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
                    return new ClericCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new ClericCantripData();
                }

                var clericCantripData = JsonSerializer.Deserialize<ClericCantripData>(jsonData);

                if (clericCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new ClericCantripData();
                }

                return clericCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Cleric_Spells_Json_Loader
    {
        public static ClericSpellData LoadclericSpellData(string jsonFilePath)
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
                    return new ClericSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new ClericSpellData();
                }

                var clericSpellData = JsonSerializer.Deserialize<ClericSpellData>(jsonData);

                if (clericSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new ClericSpellData();
                }

                return clericSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class ClericCantripLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Cleric Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathClericCantrip = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Cantrips.json";

            var cantripsCleric = Cleric_Cantrips_Json_Loader.LoadclericCantripData(jsonFilePathClericCantrip);


            // Display the data for Cleric Cantrips
            if (cantripsCleric != null && cantripsCleric.CantripCategories != null)
            {
                Console.WriteLine("Cleric Cantrips:");
                foreach (var clericCans in cantripsCleric.CantripCategories)
                {
                    Console.WriteLine($"- Name: {clericCans.Name}, Source: {clericCans.Source}, School: {clericCans.School}, Cast_Time: {clericCans.Cast_Time}, Components: {clericCans.Components}, Duration: {clericCans.Duration}, Description: {clericCans.Description}, Spell_Lists: {clericCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class ClericSpellLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Trinket Data ...");

            // Define the paths to the JSON files
            string jsonFilePathClericSpells = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Spells";
            

            // Load the cleric spell data using the Bard spell JsonLoader
            var clericLevelSpells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpells);

            if (clericLevelSpells != null && clericLevelSpells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Cleric Spells:");
                foreach (var spell in clericLevelSpells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");
                }
            }

     
        }
    }
}
