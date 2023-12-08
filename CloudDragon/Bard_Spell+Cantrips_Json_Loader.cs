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
        private static void DisplayBardicSpells(string spellLevel, Dictionary<string, List<BardicSpells>> spellCategories)
        {
            Console.WriteLine($"Level {spellLevel} Bard Spells:");
            foreach (var bardSpell in spellCategories[$"Level {spellLevel} Bard Spells"])
            {
                Console.WriteLine($"- Name: {bardSpell.Name}, School: {bardSpell.School}, Description: {bardSpell.Description} ");
            }
        }
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            string jsonFilePathBardSpellsLevel1 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_1_Spells.json";
            string jsonFilePathBardSpellsLevel2 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_2_Spells.json";
            string jsonFilePathBardSpellsLevel3 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_3_Spells.json";
            string jsonFilePathBardSpellsLevel4 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_4_Spells.json";
            string jsonFilePathBardSpellsLevel5 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_5_Spells.json";
            string jsonFilePathBardSpellsLevel6 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_6_Spells.json";
            string jsonFilePathBardSpellsLevel7 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_7_Spells.json";
            string jsonFilePathBardSpellsLevel8 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_8_Spells.json";
            string jsonFilePathBardSpellsLevel9 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_9_Spells.json";

            // Load the bard spell data using the Bard spell JsonLoader
            var bardLevel1Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel1);
            var bardLevel2Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel2);
            var bardLevel3Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel3);
            var bardLevel4Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel4);
            var bardLevel5Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel5);
            var bardLevel6Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel6);
            var bardLevel7Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel7);
            var bardLevel8Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel8);
            var bardLevel9Spells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardSpellsLevel9);

            if (bardLevel1Spells != null && bardLevel1Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Bard Spells:");
                foreach (var spell in bardLevel1Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel2Spells != null && bardLevel2Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Bard Spells:");
                foreach (var spell in bardLevel2Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel3Spells != null && bardLevel3Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Bard Spells:");
                foreach (var spell in bardLevel3Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel4Spells != null && bardLevel4Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Bard Spells:");
                foreach (var spell in bardLevel4Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel5Spells != null && bardLevel5Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Bard Spells:");
                foreach (var spell in bardLevel5Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel6Spells != null && bardLevel6Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 6 Bard Spells:");
                foreach (var spell in bardLevel6Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel7Spells != null && bardLevel7Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 7 Bard Spells:");
                foreach (var spell in bardLevel7Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel8Spells != null && bardLevel8Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 8 Bard Spells:");
                foreach (var spell in bardLevel8Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

            if (bardLevel9Spells != null && bardLevel9Spells.SpellCategories != null)
            {
                Console.WriteLine("Level 9 Bard Spells:");
                foreach (var spell in bardLevel9Spells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}");
                }
            }

        }

        
    }
}
