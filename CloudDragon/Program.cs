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

    // Add class for class spells + cantrips 
    public class PaladinSpellsCantrips
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

    //internal class CharacterLoader : ILoader
    //{
    //    void ILoader.Load()
    //    {
    //        Console.WriteLine("Loading Cloud Dragon ....");

    //        string jsonFilePath = "path_to_your_json_file.json";

    //        try
    //        {
    //            string jsonData = File.ReadAllText(jsonFilePath);

    //            var cloudDragonData = JsonSerializer.Deserialize<List<Character_Class>>(jsonData);

    //            if (cloudDragonData != null && cloudDragonData.Count > 0)
    //            {
    //                // Access and display some properties
    //                Console.WriteLine($"Class Name: {cloudDragonData[0]?.ClassName}");
    //                Console.WriteLine($"Hit Dice: {cloudDragonData[0]?.HitDice}");

    //                var proficienciesArmor = cloudDragonData[0]?.Proficiencies?.Armor;
    //                Console.WriteLine($"Proficiencies (Armor): {proficienciesArmor ?? "N/A"}");

    //                // Loop through and display Fighting Styles
    //                Console.WriteLine("Fighting Styles:");
    //                foreach (var style in cloudDragonData[0]?.FightingStyles ?? Enumerable.Empty<FightingStyle>())
    //                {
    //                    Console.WriteLine($"- {style?.Name}: {style?.Description}");
    //                }

    //                // Loop through and display sub-archetypes and their features
    //                Console.WriteLine("Sub-Archetypes:");
    //                foreach (var subArchetype in cloudDragonData[0]?.SubArchetypes ?? Enumerable.Empty<SubArchetype>())
    //                {
    //                    Console.WriteLine($"- {subArchetype?.ArchetypeName}");

    //                    foreach (var feature in subArchetype?.ArchetypeFeatures ?? Enumerable.Empty<ArchetypeFeature>())
    //                    {
    //                        Console.WriteLine($"  - {feature?.FeatureName}");

    //                        foreach (var description in feature?.FeatureDescription ?? Enumerable.Empty<object>())
    //                        {
    //                            if (description is string desc)
    //                            {
    //                                Console.WriteLine($"    - {desc}");
    //                            }
    //                            else
    //                            {
    //                                Console.WriteLine($"    - {description}");
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine("Deserialization failed or the list is empty.");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine("Error loading JSON file: " + e.Message);
    //        }
    //    }
    //}

    internal class Program
    {
        static void Main(string[] args)
        {

            //ILoader characterLoader = new CharacterLoader();
            //characterLoader.Load();

            ILoader EquipmentLoader = new EquipmentLoader();
            EquipmentLoader.Load();

            ILoader armorLoader = new ArmorLoader();
            armorLoader.Load();

            ILoader currencyLoader = new CurrencyLoader();
            currencyLoader.Load();

            ILoader TrinketLoader = new TrinketLoader();
            TrinketLoader.Load();

            ILoader MagicalItemLoader = new MagicalItemLoader();
            MagicalItemLoader.Load();

            ILoader BardCantripLoader = new BardCantripLoader();
            BardCantripLoader.Load();

            ILoader BardSpellLoader = new BardSpellLoader();
            BardSpellLoader.Load();

            ILoader ClericCantripLoader = new ClericCantripLoader();
            ClericCantripLoader.Load();

            ILoader ClericSpellLoader = new ClericSpellLoader();
            ClericSpellLoader.Load();

            ILoader DruidCantripLoader = new DruidCantripLoader();
            DruidCantripLoader.Load();

            ILoader DruidSpellLoader = new DruidSpellLoader();
            DruidSpellLoader.Load();

            ILoader PaladinCantripLoader = new PaladinCantripLoader();
            PaladinCantripLoader.Load();

            ILoader PaladinSpellLoader = new PaladinSpellLoader();
            PaladinSpellLoader.Load();

            ILoader RangerSpellLoader = new RangerSpellLoader();
            RangerSpellLoader.Load();

            ILoader SorcererCantripLoader = new SorcererCantripLoader();
            SorcererCantripLoader.Load();

            ILoader SorcererSpellLoader = new SorcererSpellLoader();
            SorcererSpellLoader.Load();

            ILoader WarlockCantripLoader = new WarlockCantripLoader();
            WarlockCantripLoader.Load();

            ILoader WarlockSpellLoader = new WarlockSpellLoader();
            WarlockSpellLoader.Load();

            ILoader WizardCantripLoader = new WizardCantripLoader();
            WizardCantripLoader.Load();

            ILoader WizardSpellLoader = new WizardSpellLoader();
            WizardSpellLoader.Load();

            ILoader PoisonLoader = new PoisonLoader();
            PoisonLoader.Load();
        }
    }
}
