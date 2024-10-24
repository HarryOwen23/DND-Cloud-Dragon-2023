//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace CloudDragon
//{
//    public class RangerSpells
//    {
//        [JsonPropertyName("Name")]
//        public string Name { get; set; }

//        [JsonPropertyName("School")]
//        public string School { get; set; }

//        [JsonPropertyName("Description")]
//        public string Description { get; set; }

//        [JsonPropertyName("Level")]
//        public int Level { get; set; }
//    }

//    public class RangerSpellCategory
//    {
//        [JsonPropertyName("RangerSpells")]
//        public List<RangerSpells> Spells { get; set; }
//    }

//    public class RangerSpellData
//    {
//        [JsonPropertyName("Spell Categories")]
//        public List<RangerSpells> SpellCategories { get; set; }
//    }

//    // Class to load Paladin Spell JSON data
//    internal class RangerSpellsJsonLoader
//    {
//        public static RangerSpellCategory LoadRangerSpellData(string jsonFilePath)
//        {
//            if (string.IsNullOrWhiteSpace(jsonFilePath))
//            {
//                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
//            }

//            if (!File.Exists(jsonFilePath))
//            {
//                Console.WriteLine($"File not found: {jsonFilePath}");
//                return new RangerSpellCategory();
//            }

//            try
//            {
//                string jsonData = File.ReadAllText(jsonFilePath);
//                return JsonSerializer.Deserialize<RangerSpellCategory>(jsonData) ?? new RangerSpellCategory();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error loading JSON file: {ex.Message}");
//                throw;
//            }
//        }
//    }

//    // Loader class for Ranger Spells
//    internal class RangerSpellLoader : ILoader
//    {
//        private const string JsonFilePathRangerSpells = "Spells+Cantrips\\Ranger_Cantrips_+_Spells\\Ranger_Spells.json";

//        public void Load()
//        {
//            Console.WriteLine("Loading Ranger Spell Data ...");
//            var rangerSpells = RangerSpellsJsonLoader.LoadRangerSpellData(JsonFilePathRangerSpells);

//            if (rangerSpells?.Spells != null)
//            {
//                Console.WriteLine("Cleric Spells:");
//                foreach (var spell in rangerSpells.Spells)
//                {

//                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

//                }
//            }
//        }
//    }
//}
