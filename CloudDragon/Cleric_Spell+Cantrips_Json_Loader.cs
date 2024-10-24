//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace CloudDragon
//{
//    // Class to represent Bardic Cantrips
//    public class ClericCantrips
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

//    // Class to represent Bardic Spells
//    public class ClericSpells
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

//    // Class to represent categories of Bardic Cantrips
//    public class ClericCantripCategory
//    {
//        [JsonPropertyName("ClericCantrips")]
//        public List<BardicCantrips> Cantrips { get; set; }
//    }

//    // Class to represent categories of Bardic Spells
//    public class ClericSpellCategory
//    {
//        [JsonPropertyName("ClericSpells")]
//        public List<ClericSpells> Spells { get; set; }
//    }

//    // Class to represent Cleric Cantrip Data
//    public class ClericCantripData
//    {
//        [JsonPropertyName("Cantrip Categories")]
//        public List<ClericCantripCategory> CantripCategories { get; set; }
//    }

//    // Class to represent Cleric Spell Data
//    public class ClericSpellData
//    {
//        [JsonPropertyName("Spell Categories")]
//        public List<ClericSpellCategory> SpellCategories { get; set; }
//    }

//    // Class to load Bard Cantrip JSON data
//    internal class ClericCantripsJsonLoader
//    {
//        public static ClericCantripCategory LoadClericCantripData(string jsonFilePath)
//        {
//            if (string.IsNullOrWhiteSpace(jsonFilePath))
//            {
//                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
//            }

//            if (!File.Exists(jsonFilePath))
//            {
//                Console.WriteLine($"File not found: {jsonFilePath}");
//                return new ClericCantripCategory();
//            }

//            try
//            {
//                string jsonData = File.ReadAllText(jsonFilePath);
//                return JsonSerializer.Deserialize<ClericCantripCategory>(jsonData) ?? new ClericCantripCategory();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error loading JSON file: {ex.Message}");
//                throw;
//            }
//        }
//    }

//    // Class to load Bard Spell JSON data
//    internal class ClericSpellsJsonLoader
//    {
//        public static ClericSpellCategory LoadBardSpellData(string jsonFilePath)
//        {
//            if (string.IsNullOrWhiteSpace(jsonFilePath))
//            {
//                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
//            }

//            if (!File.Exists(jsonFilePath))
//            {
//                Console.WriteLine($"File not found: {jsonFilePath}");
//                return new ClericSpellCategory();
//            }

//            try
//            {
//                string jsonData = File.ReadAllText(jsonFilePath);
//                return JsonSerializer.Deserialize<ClericSpellCategory>(jsonData) ?? new ClericSpellCategory();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error loading JSON file: {ex.Message}");
//                throw;
//            }
//        }
//    }

//    // Loader class for Cleric Cantrips
//    internal class ClericCantripLoader : ILoader
//    {
//        private const string JsonFilePathClericCantrip = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Cantrips.json";

//        public void Load()
//        {
//            Console.WriteLine("Loading Cleric Cantrip Data ...");
//            var clericcantrips = ClericCantripsJsonLoader.LoadClericCantripData(JsonFilePathClericCantrip);

//            if (clericcantrips?.Cantrips != null)
//            {
//                Console.WriteLine("Cleric Cantrips:");
//                foreach (var cantrip in clericcantrips.Cantrips)
//                {
//                    Console.WriteLine($"- Name: {cantrip.Name}, Source: {cantrip.Source}, School: {cantrip.School}, CastTime: {cantrip.CastTime}, Components: {cantrip.Components}, Duration: {cantrip.Duration}, Description: {cantrip.Description}, SpellLists: {cantrip.SpellLists}");
//                }

//            }
//        }
//    }

//    // Loader class for Cleric Spells
//    internal class ClericSpellLoader : ILoader
//    {
//        private const string JsonFilePathClericSpells = "Spells+Cantrips\\Cleric_Cantrips_+_Spells\\Cleric_Spells.json";

//        public void Load()
//        {
//            Console.WriteLine("Loading Cleric Spell Data ...");
//            var clericSpells = ClericSpellsJsonLoader.LoadBardSpellData(JsonFilePathClericSpells);

//            if (clericSpells?.Spells != null)
//            {
//                Console.WriteLine("Cleric Spells:");
//                foreach (var spell in clericSpells.Spells)
//                {

//                    Console.WriteLine($"- Name: {spell.Name}, School: {spell.School}, Description: {spell.Description}, Level: {spell.Level}");

//                }
//            }
//        }
//    }
//}
