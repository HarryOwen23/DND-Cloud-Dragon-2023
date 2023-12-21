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
        void ILoader.Load()
        {
            Console.WriteLine("Loading Ranger Spell Data");
            // Define paths to the Ranger spell Json files
            string jsonFilePathRangerLevel1 = "Spells+Cantrips\\Ranger_Cantrips_+Spells\\Ranger_Level_1_Spells.json";
            string jsonFilePathRangerLevel2 = "Spells+Cantrips\\Ranger_Cantrips_+Spells\\Ranger_Level_2_Spells.json";
            string jsonFilePathRangerLevel3 = "Spells+Cantrips\\Ranger_Cantrips_+Spells\\Ranger_Level_3_Spells.json";
            string jsonFilePathRangerLevel4 = "Spells+Cantrips\\Ranger_Cantrips_+Spells\\Ranger_Level_4_Spells.json";
            string jsonFilePathRangerLevel5 = "Spells+Cantrips\\Ranger_Cantrips_+Spells\\Ranger_Level_5_Spells.json";


            var level1rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel1);
            var level2rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel2);
            var level3rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel3);
            var level4rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel4);
            var level5rangerspells = Ranger_Spells_Json_Loader.LoadRangerSpellData(jsonFilePathRangerLevel5);



            // Display the data for Level 1 spells
            if (level1rangerspells != null && level1rangerspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Ranger Spells:");
                foreach (var rangerSpell1 in level1rangerspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {rangerSpell1.Name}, School: {rangerSpell1.School}, Description: {rangerSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2rangerspells != null && level2rangerspells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Ranger Spells:");
                foreach (var rangerSpell2 in level2rangerspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {rangerSpell2.Name}, School: {rangerSpell2.School}, Description: {rangerSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3rangerspells != null && level3rangerspells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Ranger Spells:");
                foreach (var rangerSpell3 in level3rangerspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {rangerSpell3.Name}, School: {rangerSpell3.School}, Description: {rangerSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4rangerspells != null && level4rangerspells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Ranger Spells:");
                foreach (var rangerSpell4 in level4rangerspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {rangerSpell4.Name}, School: {rangerSpell4.School}, Description: {rangerSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5rangerspells != null && level5rangerspells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Ranger Spells:");
                foreach (var rangerSpell5 in level5rangerspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {rangerSpell5.Name}, School: {rangerSpell5.School} , Description:  {rangerSpell5.Description} ");
                }
            }
        }
    }
}
