﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Background
    {
        [JsonPropertyName("Background Name")]
        public string Background_Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Skill Proficiencies")]
        public string Skill_Proficiencies { get; set; }

        [JsonPropertyName("Languages")]
        public List<string> Languages { get; set; }

        [JsonPropertyName("Starting Equipment")]
        public List<string> Starting_Equipment { get; set; }

        [JsonPropertyName("Feature")]
        public string Feature { get; set; }

        [JsonPropertyName("Feature Description")]
        public string Feature_Description { get; set; }

        [JsonPropertyName("Personality Traits")]
        public List<string> Personality_Traits { get; set; }
        
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
        private const string JsonFilePathAcolyteBackground = "Background\\\\Background_Acolyte.json";

        public void Load()
        {
            Console.WriteLine("Loading Acolyte Background Data ...");
            var acolyteBackground = JsonFilePathAcolyteBackground.LoadBackgroundData(JsonFilePathAcolyteBackground);

            if (acolyteBackground?.Backgrounds != null)
            {
                Console.WriteLine("Acolyte:");
                foreach (var backgrounds in acolyteBackground.Backgrounds)
                {
                    Console.WriteLine($"- Background Name: {backgrounds.Background_Name}, Description: {backgrounds.Source}, Skill Proficiencies: {backgrounds.Skill_Proficiencies}, Languages: {backgrounds.Languages}, Starting Equipment: {backgrounds.Starting_Equipment}, Feature: {backgrounds.Feature}, Feature Description: {backgrounds.Feature_Description}, Personality Traits: {backgrounds.Personality_Traits},  Ideals: {backgrounds.Ideals}, Bonds: {backgrounds.Bonds}, Flaws: {backgrounds.Flaws} ");
                }

            }
        }
    }
}
