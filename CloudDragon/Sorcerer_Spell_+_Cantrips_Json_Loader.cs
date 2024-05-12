using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudDragon;

namespace CloudDragon
{
    public class SorcererCantrips
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
    public class SorcererSpells
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

    public class SorcererCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<SorcererCantrips> Cantrips { get; set; }
    }

    public class SorcererSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<SorcererSpells> Spells { get; set; }
    }

    public class SorcererCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<SorcererCantrips> CantripCategories { get; set; }
    }

    public class SorcererSpellData
{
        [JsonPropertyName("Spell Categories")]
        public List<SorcererSpells> SpellCategories { get; set; }
    }

    internal class Sorcerer_Cantrips_Json_Loader
{
        public static SorcererCantripData LoadsorcererCantripData(string jsonFilePath)
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
                    return new SorcererCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new SorcererCantripData();
                }

                var sorcererCantripData = JsonSerializer.Deserialize<SorcererCantripData>(jsonData);

                if (sorcererCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new SorcererCantripData();
                }

                return sorcererCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Sorcerer_Spells_Json_Loader
    {
        public static SorcererSpellData LoadsorcererSpellData(string jsonFilePath)
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
                    return new SorcererSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new SorcererSpellData();
                }

                var sorcererCantripData = JsonSerializer.Deserialize<SorcererSpellData>(jsonData);

                if (sorcererCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new SorcererSpellData();
                }

                return sorcererCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class SorcererCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Sorcerer Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathSorcererCantrip = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Cantrips.json";

            var cantripsSorcerer = Sorcerer_Cantrips_Json_Loader.LoadsorcererCantripData(jsonFilePathSorcererCantrip);


            // Display the data for Sorcerer Cantrips
            if (cantripsSorcerer != null && cantripsSorcerer.CantripCategories != null)
            {
                Console.WriteLine("Sorcerer Cantrips:");
                foreach (var sorcererCans in cantripsSorcerer.CantripCategories)
                {
                    Console.WriteLine($"- Name: {sorcererCans.Name}, Source: {sorcererCans.Source}, School: {sorcererCans.School}, Cast_Time: {sorcererCans.Cast_Time}, Components: {sorcererCans.Components}, Duration: {sorcererCans.Duration}, Description: {sorcererCans.Description}, Spell_Lists: {sorcererCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class SorcererSpellLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Sorcerer Spell Data");
            // Define paths to the sorcerer spell Json files
            string jsonFilePathSorcererLevel1 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Spells.json";
            
            var level1sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel1);

            // Display the data for Level 1 spells
            if (level1sorcererspells != null && level1sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Sorcerer Spells:");
                foreach (var sorcererSpell1 in level1sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell1.Name}, School: {sorcererSpell1.School}, Description: {sorcererSpell1.Description}, Level: {sorcererSpell1.Level} ");
                }
            }

            
        }
    }
}
