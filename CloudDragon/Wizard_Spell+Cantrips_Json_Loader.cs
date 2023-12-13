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
        void ILoader.Load()
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
            string jsonFilePathWizardLevel1 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_1_Spells.json";
            string jsonFilePathWizardLevel2 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_2_Spells.json";
            string jsonFilePathWizardLevel3 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_3_Spells.json";
            string jsonFilePathWizardLevel4 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_4_Spells.json";
            string jsonFilePathWizardLevel5 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_5_Spells.json";
            string jsonFilePathWizardLevel6 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_6_Spells.json";
            string jsonFilePathWizardLevel7 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_7_Spells.json";
            string jsonFilePathWizardLevel8 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_8_Spells.json";
            string jsonFilePathWizardLevel9 = "Spells+Cantrips\\Wizard_Cantrips_+_Spells\\Wizard_Level_9_Spells.json";

            var level1wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel1);
            var level2wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel2);
            var level3wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel3);
            var level4wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel4);
            var level5wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel5);
            var level6wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel6);
            var level7wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel7);
            var level8wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel8);
            var level9wizardspells = Wizard_Spells_Json_Loader.LoadwizardSpellData(jsonFilePathWizardLevel9);


            // Display the data for Level 1 spells
            if (level1wizardspells != null && level1wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 1 Wizard Spells:");
                foreach (var wizardSpell1 in level1wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell1.Name}, School: {wizardSpell1.School}, Description: {wizardSpell1.Description} ");
                }
            }

            // Display the data for Level 2 spells
            if (level2wizardspells != null && level2wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 2 Wizard Spells:");
                foreach (var wizardSpell2 in level2wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell2.Name}, School: {wizardSpell2.School}, Description: {wizardSpell2.Description} ");
                }
            }

            // Display the data for Level 3 spells
            if (level3wizardspells != null && level3wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 3 Wizard Spells:");
                foreach (var wizardSpell3 in level3wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell3.Name}, School: {wizardSpell3.School} , Description:  {wizardSpell3.Description} ");
                }
            }

            // Display the data for Level 4 spells
            if (level4wizardspells != null && level4wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 4 Wizard Spells:");
                foreach (var wizardSpell4 in level4wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell4.Name}, School: {wizardSpell4.School}, Description: {wizardSpell4.Description} ");
                }
            }

            // Display the data for Level 5 spells
            if (level5wizardspells != null && level5wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 5 Wizard Spells:");
                foreach (var wizardSpell5 in level5wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell5.Name}, School: {wizardSpell5.School} , Description:  {wizardSpell5.Description} ");
                }
            }

            // Display the data for Level 6 spells
            if (level6wizardspells != null && level6wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 6 Wizard Spells:");
                foreach (var wizardSpell6 in level6wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell6.Name}, School: {wizardSpell6.School}  , Description:   {wizardSpell6.Description} ");
                }
            }

            // Display the data for Level 7 spells
            if (level7wizardspells != null && level7wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 7 Wizard Spells:");
                foreach (var wizardSpell7 in level7wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell7.Name}, School: {wizardSpell7.School} , Description:  {wizardSpell7.Description} ");
                }
            }

            // Display the data for Level 8 spells
            if (level8wizardspells != null && level8wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 8 Wizard Spells:");
                foreach (var wizardSpell8 in level8wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell8.Name}, School: {wizardSpell8.School}, Description: {wizardSpell8.Description} ");
                }
            }

            // Display the data for Level 9 spells
            if (level9wizardspells != null && level9wizardspells.SpellCategories != null)
            {
                Console.WriteLine("Level 9 Wizard Spells:");
                foreach (var wizardSpell9 in level9wizardspells.SpellCategories)
                {
                    Console.WriteLine($"- Name: {wizardSpell9.Name}, School: {wizardSpell9.School}, Description: {wizardSpell9.Description} ");
                }
            }
        }
    }
}
