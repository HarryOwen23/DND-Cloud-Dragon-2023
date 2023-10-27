using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Equipment
    {
        public List<EquipmentCategory> EquipmentCategories { get; set; }
    }

    public class EquipmentCategory
    {
        [JsonPropertyName("Pack Name")]
        public string PackName { get; set; }

        [JsonPropertyName("Contents")]
        public List<EquipmentItem> Contents { get; set; }
    }

    public class EquipmentItem
    {
        [JsonPropertyName("Item")]
        public string Item { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }

    internal class EquipmentJsonLoader : ILoader
    {
        public Equipment LoadEquipmentData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var equipmentData = JsonSerializer.Deserialize<Equipment>(jsonData);
                return equipmentData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                return null;
            }
        }

        public void Load()
        {
            Console.WriteLine("Loading Equipment Data...");

            string jsonFilePath = "EquipmentData.json";

            var equipmentData = LoadEquipmentData(jsonFilePath);

            if (equipmentData != null)
            {
                Console.WriteLine("Equipment Data Loaded Successfully!");
                // You can access and process the equipment data here
            }
        }
    }
}
