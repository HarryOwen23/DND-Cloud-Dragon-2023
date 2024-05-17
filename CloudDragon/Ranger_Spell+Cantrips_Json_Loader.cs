using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class RangerSpells
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

    public class RangerSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<RangerSpells> Spells { get; set; }
    }

    public class RangerSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<RangerSpells> SpellCategories { get; set; }
    }

    internal class Ranger_Spells_Json_Loader
    {
        public static RangerSpellData LoadRangerSpellData(string jsonFilePath)
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
                    return new RangerSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new RangerSpellData();
                }

                var rangerSpellData = JsonSerializer.Deserialize<RangerSpellData>(jsonData);

                if (rangerSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new RangerSpellData();
                }

                return rangerSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class RangerSpellLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Ranger Spell Data");
            // Define paths to the Ranger spell Json files
            string jsonFilePathRangerLevel1 = "Spells+Cantrips\\Ranger_Cantrips_+Spells\\Ranger_Spells.json";

            var level1rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel1);

            // Display the data for Level 1 spells
            if (level1rangerspells != null && level1rangerspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Ranger Spells:");
                foreach (var rangerSpell1 in level1rangerspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {rangerSpell1.Name}, School: {rangerSpell1.School}, Description: {rangerSpell1.Description} ");
                }
            }
        }
    }
}
