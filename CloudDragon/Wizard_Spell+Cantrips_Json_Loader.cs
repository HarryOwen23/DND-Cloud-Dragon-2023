using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudDragon;

namespace CloudDragon
{
    public class WizardCantrips
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
    public class WizardSpells
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("School")]
        public string School { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Level")]
        public int Level { get; set; }
    }

    public class WizardCantripcategory
    {
        [JsonPropertyName("Cantrips")]
        public List<WizardCantrips> Cantrips { get; set; }
    }

    public class WizardSpellcategory
    {
        [JsonPropertyName("Spells")]
        public List<WizardSpells> Spells { get; set; }
    }

    public class WizardCantripData
    {
        [JsonPropertyName("Cantrip Categories")]
        public List<WizardCantrips> CantripCategories { get; set; }
    }

    public class WizardSpellData
    {
        [JsonPropertyName("Spell Categories")]
        public List<WizardSpells> SpellCategories { get; set; }
    }

    internal class Wizard_Cantrips_Json_Loader
    {
        public static WizardCantripData LoadwizardCantripData(string jsonFilePath)
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
                    return new WizardCantripData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new WizardCantripData();
                }

                var wizardCantripData = JsonSerializer.Deserialize<WizardCantripData>(jsonData);

                if (wizardCantripData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new WizardCantripData();
                }

                return wizardCantripData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
            }
    }

    internal class Wizard_Spells_Json_Loader
    {
        public static WizardSpellData LoadwizardSpellData(string jsonFilePath)
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
                    return new WizardSpellData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new WizardSpellData();
                }

                var wizardSpellData = JsonSerializer.Deserialize<WizardSpellData>(jsonData);

                if (wizardSpellData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new WizardSpellData();
                }

                return wizardSpellData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class WizardCantripLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Wizard Cantrip Data ...");
            // Define the paths to the JSON files
            string jsonFilePathWizardCantrip = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Cantrips.json";

            var cantripsWizard = Wizard_Cantrips_Json_Loader.LoadwizardCantripData(jsonFilePathWizardCantrip);


            // Display the data for Wizard Cantrips
            if (cantripsWizard != null && cantripsWizard.CantripCategories != null)
            {
                Console.WriteLine("Wizard Cantrips:");
                foreach (var wizardCans in cantripsWizard.CantripCategories)
                {
                    Console.WriteLine($"- Name: {wizardCans.Name}, Source: {wizardCans.Source}, School: {wizardCans.School}, Cast_Time: {wizardCans.Cast_Time}, Components: {wizardCans.Components}, Duration: {wizardCans.Duration}, Description: {wizardCans.Description}, Spell_Lists: {wizardCans.Spell_Lists} ");
                }
            }
        }
    }

    internal class WizardSpellLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Wizard Spell Data");
            // Define paths to the Wizard spell Json files
            string jsonFilePathWizardLevel1 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Spells.json";


            var level1wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel1);


            // Display the data for Level 1 spells
            if (level1wizardspells != null && level1wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Wizard Spells:");
                foreach (var wizardSpell1 in level1wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell1.Name}, School: {wizardSpell1.School}, Description: {wizardSpell1.Description}, Level: {wizardSpell1.Level} ");
                }
            }

        }
    }
}
