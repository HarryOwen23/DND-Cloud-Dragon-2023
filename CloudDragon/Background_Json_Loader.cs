using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Background
    {
        [JsonPropertyName("Background Name")]
        public string BackgroundName { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Skill Proficiencies")]
        public List<string> SkillProficiencies { get; set; }

        [JsonPropertyName("Languages")]
        public List<string> Languages { get; set; }

        [JsonPropertyName("Starting Equipment")]
        public List<string> StartingEquipment { get; set; }

        [JsonPropertyName("Feature")]
        public string Feature { get; set; }

        [JsonPropertyName("Feature Description")]
        public string FeatureDescription { get; set; }

        [JsonPropertyName("Personality Traits")]
        public List<string> PersonalityTraits { get; set; }

        [JsonPropertyName("Ideals")]
        public List<string> Ideals { get; set; }

        [JsonPropertyName("Bonds")]
        public List<string> Bonds { get; set; }

        [JsonPropertyName("Flaws")]
        public List<string> Flaws { get; set; }
    }
    public class BackgroundCategory
    {
        [JsonPropertyName("Backgrounds")]
        public List<Background> BackgroundCategories { get; set; }
    }

    internal class Background_Json_Loader
    {
        public static BackgroundCategory LoadBackgroundData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new BackgroundCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<BackgroundCategory>(jsonData) ?? new BackgroundCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }
    // Loader class for Backgrounds
    internal class BackgroundLoader : ILoader
    {
        private const string JsonFilePathBackground = "Background\\Backgrounds.json";
        public void Load()
        {
            Console.WriteLine("Loading Background Data ...");
            var allBackground = Background_Json_Loader.LoadBackgroundData(JsonFilePathBackground);

            if (allBackground?.BackgroundCategories != null)
            {
                Console.WriteLine("Backgrounds:");
                foreach (var background in allBackground.BackgroundCategories)
                {
                    Console.WriteLine($"- Background Name: {background.BackgroundName}, Description: {background.Description}, Skill Proficiencies: {background.SkillProficiencies}, Languages: {string.Join(", ", background.Languages)}, Starting Equipment: {string.Join(", ", background.StartingEquipment)}, Feature: {background.Feature}, Feature Description: {background.FeatureDescription}, Personality Traits: {string.Join(", ", background.PersonalityTraits)}, Ideals: {string.Join(", ", background.Ideals)}, Bonds: {string.Join(", ", background.Bonds)}, Flaws: {string.Join(", ", background.Flaws)}");
                }
            }

        }
    }

}
