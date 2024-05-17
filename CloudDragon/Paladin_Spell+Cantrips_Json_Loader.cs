using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class PaladinCantrips
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

    public class PaladinCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<PaladinCantrips> Cantrips { get; set; }
    }

    public class PaladinSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<PaladinSpells> Spells { get; set; }
    }

    public class PaladinCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<PaladinCantrips> CantripCategories { get; set; }
    }

    public class PaladinSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<PaladinSpells> SpellCategories { get; set; }
    }

    internal class Paladin_Cantrips_Json_Loader
    {
        public static PaladinCantripData LoadpaladinCantripData(string jsonFilePath)
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
                    return new PaladinCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new PaladinCantripData();
                }

                var paladinCantripData = JsonSerializer.Deserialize<PaladinCantripData>(jsonData);

                if (paladinCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new PaladinCantripData();
                }

                return paladinCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Paladin_Spells_Json_Loader
    {
        public static PaladinSpellData LoadpaladinSpellData(string jsonFilePath)
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
                    return new PaladinSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new PaladinSpellData();
                }

                var paladinSpellData = JsonSerializer.Deserialize<PaladinSpellData>(jsonData);

                if (paladinSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new PaladinSpellData();
                }

                return paladinSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class PaladinCantripLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Paladin Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathPaladinCantrip = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Cantrips.json";

            var cantripsPaladin = Paladin_Cantrips_Json_Loader.LoadpaladinCantripData(jsonFilePathPaladinCantrip);


            // Display the data for Paladin Cantrips
            if (cantripsPaladin != null && cantripsPaladin.CantripCategories != null)
            {
                Console.WriteLine("Paladin Cantrips:");
                foreach (var palaCans in cantripsPaladin.CantripCategories)
                {
                    Console.WriteLine($"- Name: {palaCans.Name}, Source: {palaCans.Source}, School: {palaCans.School}, Cast_Time: {palaCans.Cast_Time}, Components: {palaCans.Components}, Duration: {palaCans.Duration}, Description: {palaCans.Description}, Spell_Lists: {palaCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class PaladinSpellLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            // Define paths to the Paladin spell Json files
            string jsonFilePathPaladinLevel1 = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Spells.json";

            var level1paladinspells = Paladin_Spells_Json_Loader.LoadpaladinSpellData(jsonFilePathPaladinLevel1);


            // Display the data for Level 1 spells
            if (level1paladinspells != null && level1paladinspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Paladin Spells:");
                foreach (var paladinSpell1 in level1paladinspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {paladinSpell1.Name}, School: {paladinSpell1.School}, Description: {paladinSpell1.Description}, Level: {paladinSpell1.Level} ");
                }
            }

        }
    }
}
