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
        // BardCantripData
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
        void ILoader.Load()
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
        void ILoader.Load()
        {
            Console.WriteLine("Loading Trinket Data ...");

            // Define the paths to the JSON files
            string jsonFilePathClericSpellsLevel1 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_1_Spells.json";
            string jsonFilePathClericSpellsLevel2 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_2_Spells.json";
            string jsonFilePathClericSpellsLevel3 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_3_Spells.json";
            string jsonFilePathClericSpellsLevel4 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_4_Spells.json";
            string jsonFilePathClericSpellsLevel5 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_5_Spells.json";
            string jsonFilePathClericSpellsLevel6 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_6_Spells.json";
            string jsonFilePathClericSpellsLevel7 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_7_Spells.json";
            string jsonFilePathClericSpellsLevel8 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_8_Spells.json";
            string jsonFilePathClericSpellsLevel9 = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Level_9_Spells.json";

            // Load the cleric spell data using the Bard spell JsonLoader
            var clericLevel1Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel1);
            var clericLevel2Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel2);
            var clericLevel3Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel3);
            var clericLevel4Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel4);
            var clericLevel5Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel5);
            var clericLevel6Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel6);
            var clericLevel7Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel7);
            var clericLevel8Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel8);
            var clericLevel9Spells = Cleric_Spells_Json_Loader.LoadclericSpellData(jsonFilePathClericSpellsLevel9);

            if (clericLevel1Spells != null && clericLevel1Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Cleric Spells:");
                foreach (var spell in clericLevel1Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel2Spells != null && clericLevel2Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Cleric Spells:");
                foreach (var spell in clericLevel2Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel3Spells != null && clericLevel3Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Cleric Spells:");
                foreach (var spell in clericLevel3Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel4Spells != null && clericLevel4Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Cleric Spells:");
                foreach (var spell in clericLevel4Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel5Spells != null && clericLevel5Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Cleric Spells:");
                foreach (var spell in clericLevel5Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel6Spells != null && clericLevel6Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 6 Cleric Spells:");
                foreach (var spell in clericLevel6Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel7Spells != null && clericLevel7Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 7 Cleric Spells:");
                foreach (var spell in clericLevel7Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel8Spells != null && clericLevel8Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 8 Cleric Spells:");
                foreach (var spell in clericLevel8Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (clericLevel9Spells != null && clericLevel9Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 9 Cleric Spells:");
                foreach (var spell in clericLevel9Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }
        }
    }
}
