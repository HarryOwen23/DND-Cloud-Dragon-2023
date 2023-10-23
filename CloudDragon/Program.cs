using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Character_Class
    {
        [JsonPropertyName("Class Name")]
        public string ClassName { get; set; }

        [JsonPropertyName("HitDice")]
        public string HitDice { get; set; }

        [JsonPropertyName("HitPoints")]
        public HitPoints HitPoints { get; set; }

        [JsonPropertyName("Proficiencies")]
        public Proficiencies Proficiencies { get; set; }

        [JsonPropertyName("Starting Equipment")]
        public List<string> StartingEquipment { get; set; }

        [JsonPropertyName("Fighting Styles")]
        public List<FightingStyle> FightingStyles { get; set; }

        [JsonPropertyName("Class Features")]
        public List<ClassFeature> ClassFeatures { get; set; }

        // Add a list of sub-archetypes
        [JsonPropertyName("SubArchetypes")]
        public List<SubArchetype> SubArchetypes { get; set; }
    }

    public class HitPoints
    {
        [JsonPropertyName("At 1st Level")]
        public string At1stLevel { get; set; }

        [JsonPropertyName("At Higher Levels")]
        public string AtHigherLevels { get; set; }
    }

    public class Proficiencies
    {
        [JsonPropertyName("Armor")]
        public string Armor { get; set; }

        [JsonPropertyName("Weapons")]
        public string Weapons { get; set; }

        [JsonPropertyName("Tools")]
        public string Tools { get; set; }

        [JsonPropertyName("Saving Throws")]
        public string SavingThrows { get; set; }

        [JsonPropertyName("Skills")]
        public string Skills { get; set; }
    }

    public class FightingStyle
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class ClassFeature
    {
        [JsonPropertyName("Feature")]
        public string Feature { get; set; }

        [JsonPropertyName("Description")]
        public object Description { get; set; } // The description field can contain either a string or an array of strings
    }

    // Add a class for sub-archetypes
    public class SubArchetype
    {
        [JsonPropertyName("Archetype Name")]
        public string ArchetypeName { get; set; }

        [JsonPropertyName("Archetype Features")]
        public List<ArchetypeFeature> ArchetypeFeatures { get; set; }
    }

    // Add a class for archetype features
    public class ArchetypeFeature
    {
        [JsonPropertyName("Archetype Feature")]
        public string FeatureName { get; set; }

        [JsonPropertyName("Archetype Feature Description")]
        public List<object> FeatureDescription { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading Cloud Dragon ....");

            string jsonFilePath = "path_to_your_json_file.json";

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                var cloudDragonData = JsonSerializer.Deserialize<List<CloudDragonData>>(jsonData);

                // Access and display some properties
                Console.WriteLine($"Class Name: {cloudDragonData[0].ClassName}");
                Console.WriteLine($"Hit Dice: {cloudDragonData[0].HitDice}");
                Console.WriteLine($"Proficiencies (Armor): {cloudDragonData[0].Proficiencies.Armor}");

                // You can access other properties as needed

                // Example: Loop through and display Fighting Styles
                Console.WriteLine("Fighting Styles:");
                foreach (var style in cloudDragonData[0].FightingStyles)
                {
                    Console.WriteLine($"- {style.Name}: {style.Description}");
                }

                // Example: Loop through and display sub-archetypes and their features
                Console.WriteLine("Sub-Archetypes:");
                foreach (var subArchetype in cloudDragonData[0].SubArchetypes)
                {
                    Console.WriteLine($"- {subArchetype.ArchetypeName}");

                    foreach (var feature in subArchetype.ArchetypeFeatures)
                    {
                        Console.WriteLine($"  - {feature.FeatureName}");

                        foreach (var description in feature.FeatureDescription)
                        {
                            if (description is string desc)
                            {
                                Console.WriteLine($"    - {desc}");
                            }
                            else if (description is List<object> descList)
                            {
                                foreach (var item in descList)
                                {
                                    Console.WriteLine($"    - {item}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
            }
        }
    }
}
