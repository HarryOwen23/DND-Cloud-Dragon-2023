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
        void ILoader.Load()
        {
            Console.WriteLine("Loading Paladin Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathPaladinCantrip = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Cantrips.json";

            var cantripsPaladin = Paladin_Cantrips_Json_Loader.LoadpaladinCantripData(jsonFilePathPaladinCantrip);


            // Display the data for Paladin Cantrips
            if (cantripsPaladin != null)
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
        void ILoader.Load()
        {
            Console.WriteLine("Loading Bard Spell Data");
            // Define paths to the Paladin spell Json files
            string jsonFilePathPaladinLevel1 = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Level_1_Spells.json";
            string jsonFilePathPaladinLevel2 = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Level_2_Spells.json";
            string jsonFilePathPaladinLevel3 = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Level_3_Spells.json";
            string jsonFilePathPaladinLevel4 = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Level_4_Spells.json";
            string jsonFilePathPaladinLevel5 = "Spells+Cantrips\\Paladin_Cantrips_+_Spells\\Paladin_Level_5_Spells.json";


            var level1paladinspells = Paladin_Spells_Json_Loader.LoadpaladinSpellData(jsonFilePathPaladinLevel1);
            var level2paladinspells = Paladin_Spells_Json_Loader.LoadpaladinSpellData(jsonFilePathPaladinLevel2);
            var level3paladinspells = Paladin_Spells_Json_Loader.LoadpaladinSpellData(jsonFilePathPaladinLevel3);
            var level4paladinspells = Paladin_Spells_Json_Loader.LoadpaladinSpellData(jsonFilePathPaladinLevel4);
            var level5paladinspells = Paladin_Spells_Json_Loader.LoadpaladinSpellData(jsonFilePathPaladinLevel5);



            // Display the data for Level 1 spells
            if (level1paladinspells != null)
            {
                Console.WriteLine("Level 1 Paladin Spells:");
                foreach (var paladinSpell1 in level1paladinspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {paladinSpell1.Name}, School: {paladinSpell1.School}, Description: {paladinSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2paladinspells != null)
            {
                Console.WriteLine("Level 2 Paladin Spells:");
                foreach (var paladinSpell2 in level2paladinspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {paladinSpell2.Name}, School: {paladinSpell2.School}, Description: {paladinSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3paladinspells != null)
            {
                Console.WriteLine("Level 3 Paladin Spells:");
                foreach (var paladinSpell3 in level3paladinspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {paladinSpell3.Name}, School: {paladinSpell3.School}, Description: {paladinSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4paladinspells != null)
            {
                Console.WriteLine("Level 4 Paladin Spells:");
                foreach (var paladinSpell4 in level4paladinspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {paladinSpell4.Name}, School: {paladinSpell4.School}, Description: {paladinSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5paladinspells != null)
            {
                Console.WriteLine("Level 5 Paladin Spells:");
                foreach (var paladinSpell5 in level5paladinspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {paladinSpell5.Name}, School: {paladinSpell5.School}, Description: {paladinSpell5.Description} ");
                }
            }
        }
    }
}
