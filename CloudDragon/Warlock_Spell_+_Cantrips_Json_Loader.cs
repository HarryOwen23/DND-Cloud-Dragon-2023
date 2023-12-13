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
            string jsonFilePathWarlockLevel1 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_1_Spells.json";
            string jsonFilePathWarlockLevel2 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_2_Spells.json";
            string jsonFilePathWarlockLevel3 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_3_Spells.json";
            string jsonFilePathWarlockLevel4 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_4_Spells.json";
            string jsonFilePathWarlockLevel5 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_5_Spells.json";
            string jsonFilePathWarlockLevel6 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_6_Spells.json";
            string jsonFilePathWarlockLevel7 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_7_Spells.json";
            string jsonFilePathWarlockLevel8 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_8_Spells.json";
            string jsonFilePathWarlockLevel9 = "Spells+Cantrips\\Warlock_Cantrips_+_Spells\\Warlock_Level_9_Spells.json";

            var level1warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel1);
            var level2warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel2);
            var level3warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel3);
            var level4warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel4);
            var level5warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel5);
            var level6warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel6);
            var level7warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel7);
            var level8warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel8);
            var level9warlockspells = Warlock_Spells_Json_Loader.LoadwarlockSpellData(jsonFilePathWarlockLevel9);


            // Display the data for Level 1 spells
            if (level1warlockspells != null && level1warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Warlock Spells:");
                foreach (var warlockSpell1 in level1warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell1.Name}, School: {warlockSpell1.School}, Description: {warlockSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2warlockspells != null && level2warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Warlock Spells:");
                foreach (var warlockSpell2 in level2warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell2.Name}, School: {warlockSpell2.School}, Description: {warlockSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3warlockspells != null && level3warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Warlock Spells:");
                foreach (var warlockSpell3 in level3warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell3.Name}, School: {warlockSpell3.School} , Description:  {warlockSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4warlockspells != null && level4warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Warlock Spells:");
                foreach (var warlockSpell4 in level4warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell4.Name}, School: {warlockSpell4.School}, Description: {warlockSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5warlockspells != null && level5warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Warlock Spells:");
                foreach (var warlockSpell5 in level5warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell5.Name}, School: {warlockSpell5.School} , Description:  {warlockSpell5.Description} ");
                }
            }

            // Display the data for Level 6 spells
            if (level6warlockspells != null && level6warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 6 Warlock Spells:");
                foreach (var warlockSpell6 in level6warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell6.Name}, School: {warlockSpell6.School}  , Description:   {warlockSpell6.Description} ");
                }
            }

            // Display the data for Level 7 spells
            if (level7warlockspells != null && level7warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 7 Warlock Spells:");
                foreach (var warlockSpell7 in level7warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell7.Name}, School: {warlockSpell7.School} , Description:  {warlockSpell7.Description} ");
                }
            }

            // Display the data for Level 8 spells
            if (level8warlockspells != null && level8warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 8 Warlock Spells:");
                foreach (var warlockSpell8 in level8warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell8.Name}, School: {warlockSpell8.School}, Description: {warlockSpell8.Description} ");
                }
            }

            // Display the data for Level 9 spells
            if (level9warlockspells != null && level9warlockspells.SpellCategories != null)
            {
                Console.WriteLine("Level 9 Warlock Spells:");
                foreach (var warlockSpell9 in level9warlockspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {warlockSpell9.Name}, School: {warlockSpell9.School}, Description: {warlockSpell9.Description} ");
                }
            }
        }
    }
}
