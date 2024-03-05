using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class MagicItems
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Type")]
        public string Type { get; set; }
        [JsonPropertyName("Attunement")]
        public string Attunement { get; set; }
        [JsonPropertyName("Description")]
        public string Description{ get; set; }
    }

    public class MICategory
    {
        [JsonPropertyName("magicalItems")]
        public List<MagicItems> MagicalItems { get; set; }
    }

    public class MagicalItemData
    {
        [JsonPropertyName("Magical Item Categories")]
        public List<MagicItems> MiCategories { get; set; }
    }

    internal class Magical_Items_Json_Loader
    {
        public static MagicalItemData LoadMagicalItemData(string jsonFilePath)
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
                    return new MagicalItemData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new MagicalItemData();
                }

                var magicalItemData = JsonSerializer.Deserialize<MagicalItemData>(jsonData);

                if (magicalItemData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MagicalItemData.");
                    return new MagicalItemData();
                }

                return magicalItemData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; 
            }
        }
    }

    internal class MagicalItemLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Magical Item Data ...");
            string jsonFilePathCommon = "Magical Items\\Magical_Items._Common.json";
            string jsonFilePathUncommon = "Magical Items\\Magical_Items_Uncommon.json";
            string jsonFilePathRare = "Magical Items\\Magical_Items_Rare.json";
            string jsonFilePathVeryRare = "Magical Items\\Magical_Items_Very_Rare.json";
            string jsonFilePathLegendary = "Magical Items\\Magical_Items_Legendary.json";
            string jsonFilePathUnknown = "Magical Items\\Magical_Items_Unknown.json";
            string jsonFilePathUnique = "Magical Items\\Magical_Items_Unique.json";
            string jsonFilePathArtifact = "Magical Items\\Magical_Items_Artifact.json";

            var commonItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathCommon);
            var uncommonItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathUncommon);
            var rareItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathRare);
            var veryrareItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathVeryRare);
            var legendaryItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathLegendary);
            var unknownItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathUnknown);
            var uniqueItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathUnique);
            var artifactItems = Magical_Items_Json_Loader.LoadMagicalItemData(jsonFilePathArtifact);


            // Display the Armor data for common magical items
            if (commonItems != null && commonItems.MiCategories != null)
            {
                Console.WriteLine("Common Magical Items:");
                foreach (var magicitems in commonItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for uncommon magical items
            if (uncommonItems != null && uncommonItems.MiCategories != null)
            {
                Console.WriteLine("Uncommon Magical Items:");
                foreach (var magicitems in uncommonItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for rare magical items
            if (rareItems != null && rareItems.MiCategories != null)
            {
                Console.WriteLine("Rare Magical Items:");
                foreach (var magicitems in rareItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for very rare magical items
            if (veryrareItems != null && veryrareItems.MiCategories != null)
            {
                Console.WriteLine("Rare Magical Items:");
                foreach (var magicitems in veryrareItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for legendary magical items
            if (legendaryItems != null && legendaryItems.MiCategories != null)
            {
                Console.WriteLine("Legendary Magical Items:");
                foreach (var magicitems in legendaryItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for artifact magical items
            if (artifactItems != null && artifactItems.MiCategories != null)
            {
                Console.WriteLine("Artifact Magical Items:");
                foreach (var magicitems in artifactItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for unknown magical items
            if (unknownItems != null && unknownItems.MiCategories != null)
            {
                Console.WriteLine("Unknown Magical Items:");
                foreach (var magicitems in unknownItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }

            // Display the Armor data for unique magical items
            if (uniqueItems != null && uniqueItems.MiCategories != null)
            {
                Console.WriteLine("Unique Magical Items:");
                foreach (var magicitems in uniqueItems.MiCategories)
                {
                    Console.WriteLine($"- Name: {magicitems.Name}, Type: {magicitems.Type}, Attunement: {magicitems.Attunement}, Description: {magicitems.Description} ");
                }
            }
        }
    }
}
