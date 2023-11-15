using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class RangerCantrips
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
    public class RangerSpells
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class RangerCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<RangerCantrips> Cantrips { get; set; }
    }

    public class RangerSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<RangerSpells> Spells { get; set; }
    }

    public class RangerCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public Dictionary<string, List<RangerCantrips>> CantripCategories { get; set; }
    }

    public class RangerSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public Dictionary<string, List<RangerSpells>> SpellCategories { get; set; }
    }

    internal class Ranger_Cantrips_Json_Loader
    {
        public static RangerCantripData LoadRangerCantripData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var rangerCantripCategory = JsonSerializer.Deserialize<RangerCantripcategory>(jsonData);
                return new RangerCantripData
                {
                    CantripCategories = new Dictionary<string, List<RangerCantrips>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), rangerCantripCategory.Cantrips }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw e;
            }
        }
    }

    internal class Ranger_Spells_Json_Loader
    {
        public static RangerSpellData LoadRangerSpellData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var rangerSpellCategory = JsonSerializer.Deserialize<RangerSpellcategory>(jsonData);
                return new RangerSpellData
                {
                    SpellCategories = new Dictionary<string, List<RangerSpells>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), rangerSpellCategory.Spells }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw e;
            }
        }
    }

    internal class RangerCantripLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Ranger Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathRangerCantrip = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Cantrips.json";

            var cantripsRanger = Ranger_Cantrips_Json_Loader.LoadRangerCantripData(jsonFilePathRangerCantrip);


            // Display the Ranger data for Ranger Cantrips
            if (cantripsRanger != null)
            {
                Console.WriteLine("Ranger Cantrips:");
                foreach (var palaCans in cantripsRanger.CantripCategories["Ranger Cantrips"])
                {
                    Console.WriteLine($"- Name: {palaCans.Name}, Source: {palaCans.Source}, School: {palaCans.School}, Cast_Time: {palaCans.Cast_Time}, Components: {palaCans.Components}, Duration: {palaCans.Duration}, Description: {palaCans.Description}, Spell_Lists: {palaCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class RangerSpellLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Ranger Spell Data");
            // Define paths to the Ranger spell Json files
            string jsonFilePathRangerLevel1 = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Level_1_Spells.json";
            string jsonFilePathRangerLevel2 = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Level_2_Spells.json";
            string jsonFilePathRangerLevel3 = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Level_3_Spells.json";
            string jsonFilePathRangerLevel4 = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Level_4_Spells.json";
            string jsonFilePathRangerLevel5 = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Level_5_Spells.json";


            var level1rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel1);
            var level2rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel2);
            var level3rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel3);
            var level4rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel4);
            var level5rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel5);



            // Display the data for Level 1 spells
            if (level1rangerspells != null)
            {
                Console.WriteLine("Level 1 Ranger Spells:");
                foreach (var rangerSpell1 in level1rangerspells.SpellCategories["Level 1 Ranger Spells"])
                {
                    Console.WriteLine($"- Name: {rangerSpell1.Name}, School: {rangerSpell1.School}, Description: {rangerSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2rangerspells != null)
            {
                Console.WriteLine("Level 2 Ranger Spells:");
                foreach (var rangerSpell2 in level2rangerspells.SpellCategories["Level 2 Ranger Spells"])
                {
                    Console.WriteLine($"- Name: {rangerSpell2.Name}, School: {rangerSpell2.School}, Description: {rangerSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3rangerspells != null)
            {
                Console.WriteLine("Level 3 Ranger Spells:");
                foreach (var rangerSpell3 in level3rangerspells.SpellCategories["Level 3 Ranger Spells"])
                {
                    Console.WriteLine($"- Name: {rangerSpell3.Name}, School: {rangerSpell3.School}, Description: {rangerSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4rangerspells != null)
            {
                Console.WriteLine("Level 4 Ranger Spells:");
                foreach (var rangerSpell4 in level4rangerspells.SpellCategories["Level 4 Ranger Spells"])
                {
                    Console.WriteLine($"- Name: {rangerSpell4.Name}, School: {rangerSpell4.School}, Description: {rangerSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5rangerspells != null)
            {
                Console.WriteLine("Level 5 Ranger Spells:");
                foreach (var rangerSpell5 in level5rangerspells.SpellCategories["Level 5 Ranger Spells"])
                {
                    Console.WriteLine($"- Name: {rangerSpell5.Name}, School: {rangerSpell5.School} , Description:  {rangerSpell5.Description} ");
                }
            }
        }
    }
}
