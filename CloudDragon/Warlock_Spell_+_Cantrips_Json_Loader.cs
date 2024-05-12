using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudDragon;

namespace CloudDragon
{
    public class WarlockCantrips
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

    public class WarlockCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<WarlockCantrips> Cantrips { get; set; }
    }

    public class WarlockSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<WarlockSpells> Spells { get; set; }
    }

    public class WarlockCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<WarlockCantrips> CantripCategories { get; set; }
    }

    public class WarlockSpellData
{
        [JsonPropertyName("Spell Categories")]
        public List<WarlockSpells> SpellCategories { get; set; }
    }

    internal class Warlock_Cantrips_Json_Loader
{
        public static WarlockCantripData LoadwarlockCantripData(string jsonFilePath)
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
                    return new WarlockCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new WarlockCantripData();
                }

                var warlockCantripData = JsonSerializer.Deserialize<WarlockCantripData>(jsonData);

                if (warlockCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new WarlockCantripData();
                }

                return warlockCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Warlock_Spells_Json_Loader
    {
        public static WarlockSpellData LoadwarlockSpellData(string jsonFilePath)
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
                    return new WarlockSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new WarlockSpellData();
                }

                var warlockSpellData = JsonSerializer.Deserialize<WarlockSpellData>(jsonData);

                if (warlockSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new WarlockSpellData();
                }

                return warlockSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class WarlockCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Warlock Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathWarlockCantrip = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Cantrips.json";

            var cantripsWarlock = Warlock_Cantrips_Json_Loader.LoadwarlockCantripData(jsonFilePathWarlockCantrip);


            // Display the data for Warlock Cantrips
            if (cantripsWarlock != null && cantripsWarlock.CantripCategories != null)
            {
                Console.WriteLine("Warlock Cantrips:");
                foreach (var warlockCans in cantripsWarlock.CantripCategories)
                {
                    Console.WriteLine($"- Name: {warlockCans.Name}, Source: {warlockCans.Source}, School: {warlockCans.School}, Cast_Time: {warlockCans.Cast_Time}, Components: {warlockCans.Components}, Duration: {warlockCans.Duration}, Description: {warlockCans.Description}, Spell_Lists: {warlockCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class WarlockSpellLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Warlock Spell Data");
            // Define paths to the Warlock spell Json files
            string jsonFilePathWarlockLevel1 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Spells.json";
            

            var level1warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel1);


            // Display the data for Level 1 spells
            if (level1warlockspells != null && level1warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Warlock Spells:");
                foreach (var warlockSpell1 in level1warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell1.Name}, School: {warlockSpell1.School}, Description: {warlockSpell1.Description}, Level: {warlockSpell1.Level} ");
                }
            }
        }
    }
}
