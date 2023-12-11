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
            if (cantripsSorcerer != null)
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
            string jsonFilePathSorcererLevel1 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_1_Spells.json";
            string jsonFilePathSorcererLevel2 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_2_Spells.json";
            string jsonFilePathSorcererLevel3 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_3_Spells.json";
            string jsonFilePathSorcererLevel4 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_4_Spells.json";
            string jsonFilePathSorcererLevel5 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_5_Spells.json";
            string jsonFilePathSorcererLevel6 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_6_Spells.json";
            string jsonFilePathSorcererLevel7 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_7_Spells.json";
            string jsonFilePathSorcererLevel8 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_8_Spells.json";
            string jsonFilePathSorcererLevel9 = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Level_9_Spells.json";

            var level1sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel1);
            var level2sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel2);
            var level3sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel3);
            var level4sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel4);
            var level5sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel5);
            var level6sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel6);
            var level7sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel7);
            var level8sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel8);
            var level9sorcererspells = Sorcerer_Spells_Json_Loader.LoadsorcererSpellData(jsonFilePathSorcererLevel9);


            // Display the data for Level 1 spells
            if (level1sorcererspells != null && level1sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Sorcerer Spells:");
                foreach (var sorcererSpell1 in level1sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell1.Name}, School: {sorcererSpell1.School}, Description: {sorcererSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2sorcererspells != null && level2sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Sorcerer Spells:");
                foreach (var sorcererSpell2 in level2sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell2.Name}, School: {sorcererSpell2.School}, Description: {sorcererSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3sorcererspells != null && level3sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Sorcerer Spells:");
                foreach (var sorcererSpell3 in level3sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell3.Name}, School: {sorcererSpell3.School}, Description: {sorcererSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4sorcererspells != null && level4sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Sorcerer Spells:");
                foreach (var sorcererSpell4 in level4sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell4.Name}, School: {sorcererSpell4.School}, Description: {sorcererSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5sorcererspells != null && level5sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Sorcerer Spells:");
                foreach (var sorcererSpell5 in level5sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell5.Name}, School: {sorcererSpell5.School} , Description:  {sorcererSpell5.Description} ");
                }
            }

            // Display the data for Level 6 spells
            if (level6sorcererspells != null && level6sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 6 Sorcerer Spells:");
                foreach (var sorcererSpell6 in level6sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell6.Name}, School: {sorcererSpell6.School}, Description: {sorcererSpell6.Description} ");
                }
            }

            // Display the data for Level 7 spells
            if (level7sorcererspells != null && level7sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 7 Sorcerer Spells:");
                foreach (var sorcererSpell7 in level7sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell7.Name}, School: {sorcererSpell7.School}, Description: {sorcererSpell7.Description} ");
                }
            }

            // Display the data for Level 8 spells
            if (level8sorcererspells != null && level8sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 8 Sorcerer Spells:");
                foreach (var sorcererSpell8 in level8sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell8.Name}, School: {sorcererSpell8.School}, Description: {sorcererSpell8.Description} ");
                }
            }

            // Display the data for Level 9 spells
            if (level9sorcererspells != null && level9sorcererspells.SpellCategories != null)
            {
                Console.WriteLine("Level 9 Sorcerer Spells:");
                foreach (var sorcererSpell9 in level9sorcererspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {sorcererSpell9.Name}, School: {sorcererSpell9.School}, Description: {sorcererSpell9.Description} ");
                }
            }
        }
    }
}
