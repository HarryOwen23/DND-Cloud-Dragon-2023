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
        public Dictionary<string, List<BardicCantrips>> CantripCategories { get; set; }
    }

    public class BardSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public Dictionary<string, List<BardicSpells>> SpellCategories { get; set; }
    }

    internal class Bard_Cantrips_Json_Loader
    {
        public static BardCantripData LoadbardCantripData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var bardCantripCategory = JsonSerializer.Deserialize<BardCantripcategory>(jsonData);
                return new BardCantripData
                {
                    CantripCategories = new Dictionary<string, List<BardicCantrips>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), bardCantripCategory.Cantrips }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                return null;
            }
        }
    }

    internal class Bard_Spells_Json_Loader
    {
        public static BardSpellData LoadbardSpellData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var bardSpellCategory = JsonSerializer.Deserialize<BardSpellcategory>(jsonData);
                return new BardSpellData
                {
                    SpellCategories = new Dictionary<string, List<BardicSpells>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), bardSpellCategory.Spells }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                return null;
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


            // Display the Armor data for Bard Cantrips
            if (cantripsBard != null)
            {
                Console.WriteLine("Bard Cantrips:");
                foreach (var bardCans in cantripsBard.CantripCategories["Bard Cantrips"])
                {
                    Console.WriteLine($"- Name: {bardCans.Name}, Source: {bardCans.Source}, School: {bardCans.School}, Cast_Time: {bardCans.Cast_Time}, Components: {bardCans.Components}, Duration: {bardCans.Duration}, Description: {bardCans.Description}, Spell_Lists: {bardCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class BardSpellLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            // Define paths to the bard spell Json files
            string jsonFilePathBardLevel1 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_1_Spells.json";
            string jsonFilePathBardLevel2 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_2_Spells.json";
            string jsonFilePathBardLevel3 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_3_Spells.json";
            string jsonFilePathBardLevel4 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_4_Spells.json";
            string jsonFilePathBardLevel5 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_5_Spells.json";
            string jsonFilePathBardLevel6 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_6_Spells.json";
            string jsonFilePathBardLevel7 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_7_Spells.json";
            string jsonFilePathBardLevel8 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_8_Spells.json";
            string jsonFilePathBardLevel9 = "Spells+Cantrips\\Bard_Cantrips_+_Spells\\Bard_Level_9_Spells.json";

            var level1bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel1);
            var level2bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel2);
            var level3bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel3);
            var level4bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel4);
            var level5bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel5);
            var level6bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel6);
            var level7bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel7);
            var level8bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel8);
            var level9bardspells = Bard_Spells_Json_Loader.LoadbardSpellData(jsonFilePathBardLevel9);


            // Display the Armor data for Level 1 spells
            if (level1bardspells != null)
            {
                Console.WriteLine("Level 1 Bard Spells:");
                foreach (var bardSpell1 in level1bardspells.SpellCategories["Level 1 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell1.Name}, School: {bardSpell1.School}, Description: {bardSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2bardspells != null)
            {
                Console.WriteLine("Level 2 Bard Spells:");
                foreach (var bardSpell2 in level2bardspells.SpellCategories["Level 2 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell2.Name}, School: {bardSpell2.School}, Description: {bardSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3bardspells != null)
            {
                Console.WriteLine("Level 3 Bard Spells:");
                foreach (var bardSpell3 in level3bardspells.SpellCategories["Level 3 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell3.Name}, School: {bardSpell3.School}, Description: {bardSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4bardspells != null)
            {
                Console.WriteLine("Level 4 Bard Spells:");
                foreach (var bardSpell4 in level4bardspells.SpellCategories["Level 4 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell4.Name}, School: {bardSpell4.School}, Description: {bardSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5bardspells != null)
            {
                Console.WriteLine("Level 5 Bard Spells:");
                foreach (var bardSpell5 in level5bardspells.SpellCategories["Level 5 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell5.Name}, School: {bardSpell5.School}, Description: {bardSpell5.Description} ");
                }
            }

            // Display the data for Level 6 spells
            if (level6bardspells != null)
            {
                Console.WriteLine("Level 6 Bard Spells:");
                foreach (var bardSpell6 in level6bardspells.SpellCategories["Level 6 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell6.Name}, School: {bardSpell6.School}, Description: {bardSpell6.Description} ");
                }
            }

            // Display the data for Level 7 spells
            if (level7bardspells != null)
            {
                Console.WriteLine("Level 7 Bard Spells:");
                foreach (var bardSpell7 in level7bardspells.SpellCategories["Level 7 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell7.Name}, School: {bardSpell7.School}, Description: {bardSpell7.Description} ");
                }
            }

            // Display the data for Level 8 spells
            if (level8bardspells != null)
            {
                Console.WriteLine("Level 8 Bard Spells:");
                foreach (var bardSpell8 in level8bardspells.SpellCategories["Level 8 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell8.Name}, School: {bardSpell8.School}, Description: {bardSpell8.Description} ");
                }
            }

            // Display the data for Level 9 spells
            if (level9bardspells != null)
            {
                Console.WriteLine("Level 9 Bard Spells:");
                foreach (var bardSpell9 in level9bardspells.SpellCategories["Level 9 Bard Spells"])
                {
                    Console.WriteLine($"- Name: {bardSpell9.Name}, School: {bardSpell9.School}, Description: {bardSpell9.Description} ");
                }
            }
        }
    }
}
