//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace CloudDragon
//{
//    // Class to represent Sorcerer Cantrips
//    public class SorcererCantrips
//    {
//        [JsonPropertyName("Name")]
//        public string Name { get; set; }

//        [JsonPropertyName("Source")]
//        public string Source { get; set; }

//        [JsonPropertyName("School")]
//        public string School { get; set; }

//        [JsonPropertyName("Casting Time")]
//        public string CastTime { get; set; }

//        [JsonPropertyName("Range")]
//        public string Range { get; set; }

//        [JsonPropertyName("Components")]
//        public string Components { get; set; }

//        [JsonPropertyName("Duration")]
//        public string Duration { get; set; }

//        [JsonPropertyName("Description")]
//        public string Description { get; set; }

//        [JsonPropertyName("Spell Lists")]
//        public List<string> SpellLists { get; set; }
//    }

//    // Class to represent Sorcerer Spells
//    public class SorcererSpells
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

//    // Class to represent categories of Sorcerer Cantrips
//    public class SorcererCantripCategory
//    {
//        [JsonPropertyName("SorcererCantrips")]
//        public List<SorcererCantrips> Cantrips { get; set; }
//    }

//    // Class to represent categories of Sorcerer Spells
//    public class SorcererSpellCategory
//    {
//        [JsonPropertyName("SorcererSpells")]
//        public List<SorcererSpells> Spells { get; set; }
//    }

//    // Class to represent Sorcerer Cantrip Data
//    public class SorcererCantripData
//    {
//        [JsonPropertyName("Cantrip Categories")]
//        public List<SorcererCantripCategory> CantripCategories { get; set; }
//    }

//    // Class to represent Sorcerer Spell Data
//    public class SorcererSpellData
//    {
//        [JsonPropertyName("Spell Categories")]
//        public List<SorcererSpellCategory> SpellCategories { get; set; }
//    }

//    // Class to load Sorcerer Sorcerer JSON data
//    internal class SorcererCantripsJsonLoader
//    {
//        public static SorcererCantripCategory LoadSorcererCantripData(string jsonFilePath)
//        {
//            if (string.IsNullOrWhiteSpace(jsonFilePath))
//            {
//                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
//            }

//            if (!File.Exists(jsonFilePath))
//            {
//                Console.WriteLine($"File not found: {jsonFilePath}");
//                return new SorcererCantripCategory();
//            }

//            try
//            {
//                string jsonData = File.ReadAllText(jsonFilePath);
//                return JsonSerializer.Deserialize<SorcererCantripCategory>(jsonData) ?? new SorcererCantripCategory();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error loading JSON file: {ex.Message}");
//                throw;
//            }
//        }
//    }

//    // Class to load Sorcerer Spell JSON data
//    internal class SorcererSpellsJsonLoader
//    {
//        public static SorcererSpellCategory LoadSorcererSpellData(string jsonFilePath)
//        {
//            if (string.IsNullOrWhiteSpace(jsonFilePath))
//            {
//                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
//            }

//            if (!File.Exists(jsonFilePath))
//            {
//                Console.WriteLine($"File not found: {jsonFilePath}");
//                return new SorcererSpellCategory();
//            }

//            try
//            {
//                string jsonData = File.ReadAllText(jsonFilePath);
//                return JsonSerializer.Deserialize<SorcererSpellCategory>(jsonData) ?? new SorcererSpellCategory();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error loading JSON file: {ex.Message}");
//                throw;
//            }
//        }
//    }

//    // Loader class for Sorcerer Cantrips
//    internal class SorcererCantripLoader : ILoader
//    {
//        private const string JsonFilePathSorcererCantrip = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Cantrips.json";

//        public void Load()
//        {
//            Console.WriteLine("Loading Sorcerer Cantrip Data ...");
//            var sorcerercantrips = SorcererCantripsJsonLoader.LoadSorcererCantripData(JsonFilePathSorcererCantrip);

//            if (sorcerercantrips?.Cantrips != null)
//            {
//                Console.WriteLine("Sorcerer Cantrips:");
//                foreach (var cantrip in sorcerercantrips.Cantrips)
//                {
//                    Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
//                }

//            }
//        }
//    }

//    // Loader class for Sorcerer Spells
//    internal class SorcererSpellLoader : ILoader
//    {
//        private const string JsonFilePathSorcererSpells = "Spells+Cantrips\\Sorcerer_Cantrips_+_Spells\\Sorcerer_Spells.json";

//        public void Load()
//        {
//            Console.WriteLine("Loading Sorcerer Spell Data ...");
//            var sorcererSpells = SorcererSpellsJsonLoader.LoadSorcererSpellData(JsonFilePathSorcererSpells);

//            if (sorcererSpells?.Spells != null)
//            {
//                Console.WriteLine("Sorcerer Spells:");
//                foreach (var spell in sorcererSpells.Spells)
//                {

//                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

//                }
//            }
//        }
//    }
//}
