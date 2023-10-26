using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Trinket
    {
        public int DiceNumber { get; set; }
        public string TrinketDescription { get; set; }
    }

    public class TrinketCategory
    {
        public List<Trinket> Trinkets { get; set; }
    }

    public class TrinketData
    {
        public Dictionary<string, List<Trinket>> TrinketCategories { get; set; }
    }

    internal class TrinketJsonLoader
    {
        public TrinketData LoadTrinketData(List<string> filePaths)
        {
            // Create a TrinketData object to store all the categories
            TrinketData trinketData = new TrinketData
            {
                TrinketCategories = new Dictionary<string, List<Trinket>>()
            };

            foreach (var filePath in filePaths)
            {
                if (File.Exists(filePath))
                {
                    // Read the JSON data from the file
                    string trinketJson = File.ReadAllText(filePath);

                    // Deserialize the JSON into C# objects
                    TrinketCategory trinketCategory = JsonSerializer.Deserialize<TrinketCategory>(trinketJson);

                    // Extract the category name from the file path
                    string categoryName = Path.GetFileNameWithoutExtension(filePath);

                    // Add the category to the TrinketData object
                    trinketData.TrinketCategories.Add(categoryName, trinketCategory.Trinkets);
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }

            return trinketData;
        }
    }
}
