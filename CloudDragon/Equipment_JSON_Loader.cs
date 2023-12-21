using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudDragon;

namespace CloudDragon
{
    public class EquipmentData
    {
        [JsonPropertyName("ArcaneFocus")]
        public EquipmentCategory ArcaneFocus { get; set; }

        [JsonPropertyName("PackName")]
        public EquipmentCategory PackName { get; set; }

        [JsonPropertyName("Contents")]
        public EquipmentCategory Contents { get; set; }
    }

    public class EquipmentCategory
    {
        [JsonPropertyName("Items")]
        public List<EquipmentItem> Items { get; set; }
    }

    public class EquipmentItem
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Item")]
        public string Item { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }
    }


    internal class EquipmentJsonLoader
    {
        public static EquipmentCategory LoadEquipmentData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var equipmentData = JsonSerializer.Deserialize<EquipmentCategory>(jsonData);
                return equipmentData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception object
            }
        }
    }

    internal class EquipmentLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Equipment Data ...");

            // Define the paths to the JSON files 
            string jsonFilePathArcaneFocus = "Equipment\\Equipment_Arcane_Focus.json";
            string jsonFilePathBurglarPack = "Equipment\\Equipment_Burglars_Pack.json";
            string jsonFilePathClothes = "Equipment\\Equipment_Clothes.json";
            string jsonFilePathCommonItems = "Equipment\\Equipment_Common_Items.json";
            string jsonFilePathContainers = "Equipment\\Equipment_Containers.json";
            string jsonFilePathDiplomatsPack = "Equipment\\Equipment_Diplomats_Pack.json";
            string jsonFilePathDragonlance = "Equipment\\Equipment_Dragonlance.json";
            string jsonFilePathDruidicFocus = "Equipment\\Equipment_Druidic_Focus.json";
            string jsonFilePathDungeoneersPack = "Equipment\\Equipment_Dungeoneers_Pack.json";
            string jsonFilePathEntertainersPack = "Equipment\\Equipment_Entertainers_Pack.json";
            string jsonFilePathExplorersPack = "Equipment\\Equipment_Explorers_Pack.json";
            string jsonFilePathHolySymbols = "Equipment\\Equipment_Holy_Symbols.json";
            string jsonFilePathPreistsPack = "Equipment\\Equipment_Priests_Pack.json";
            string jsonFilePathScholarsPack = "Equipment\\Equipment_Scholars_Pack.json";
            string jsonFilePathUsableItems = "Equipment\\Equipment_Usable_Items.json";

            // Load the equipment data using the EquipmentJsonLoader
            var equipmentDataArcaneFocus = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathArcaneFocus);
            var equipmentDataBurglarPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathBurglarPack);
            var equipmentDataClothes = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathClothes);
            var equipmentDataCommonItems = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathCommonItems);
            var equipmentDataContainers = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathContainers);
            var equipmentDataDiplomatsPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathDiplomatsPack);
            var equipmentDataDragonlance = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathDragonlance);
            var equipmentDataDruidicFocus = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathDruidicFocus);
            var equipmentDataDungeoneersPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathDungeoneersPack);
            var equipmentDataEntertainersPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathEntertainersPack);
            var equipmentDataExplorersPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathExplorersPack);
            var equipmentDataHolySymbols = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathHolySymbols);
            var equipmentDataPreistsPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathPreistsPack);
            var equipmentDataScholarsPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathScholarsPack);
            var equipmentDataUsableItems = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathUsableItems);

            if (equipmentDataArcaneFocus != null && equipmentDataArcaneFocus.Items != null)
            {
                Console.WriteLine("Arcane Focus Equipment:");
                foreach (var magicitem in equipmentDataArcaneFocus.Items)
                {
                    Console.WriteLine($"- {magicitem.Name}: {magicitem.Cost}, {magicitem.Description}");
                }
            }

            if (equipmentDataBurglarPack != null && equipmentDataBurglarPack.Items != null)
            {
                Console.WriteLine("Burglar's Pack Equipment:");
                foreach (var buritem in equipmentDataBurglarPack.Items)
                {
                    Console.WriteLine($"- {buritem.Quantity}x {buritem.Item}");
                }
            }

            if (equipmentDataCommonItems != null && equipmentDataCommonItems.Items != null)
            {
                Console.WriteLine("Common Items Equipment:");
                foreach (var comitem in equipmentDataCommonItems.Items)
                {
                    Console.WriteLine($" Name: {comitem.Name}  Cost:{comitem.Cost} Description:{comitem.Description}");
                }
            }


            if (equipmentDataContainers != null && equipmentDataContainers.Items != null)
            {
                Console.WriteLine("Containers");
                foreach (var containers in equipmentDataContainers.Items)
                {
                    Console.WriteLine($" Name: {containers.Name}, Cost: {containers.Cost}, Weight{containers.Weight}, Description{containers.Description}");
                }
            }

            if (equipmentDataDiplomatsPack != null && equipmentDataDiplomatsPack.Items != null)
            {
                Console.WriteLine("Diplomats Pack");
                foreach (var dipPack in equipmentDataDiplomatsPack.Items)
                {
                    Console.WriteLine($"- {dipPack.Quantity}x {dipPack.Item}");
                }
            }

            if (equipmentDataDragonlance != null && equipmentDataDragonlance.Items != null)
            {
                Console.WriteLine("Dragonlance");
                foreach (var dragonItems in equipmentDataDragonlance.Items)
                {
                    Console.WriteLine($"- {dragonItems.Name}x {dragonItems.Description}");
                }
            }

            if (equipmentDataDruidicFocus != null && equipmentDataDruidicFocus.Items != null)
            {
                Console.WriteLine("Druidic Focus Items");
                foreach (var druidItem in equipmentDataDruidicFocus.Items)
                {
                    Console.WriteLine($"- {druidItem.Quantity}x {druidItem.Item}");
                }
            }

            if (equipmentDataDungeoneersPack != null && equipmentDataDungeoneersPack.Items != null)
            {
                Console.WriteLine("Dungeoneers Pack");
                foreach (var dungeonItem in equipmentDataDungeoneersPack.Items)
                {
                    Console.WriteLine($"- {dungeonItem.Quantity}x {dungeonItem.Item}");
                }
            }

            if (equipmentDataEntertainersPack != null && equipmentDataEntertainersPack.Items != null)
            {
                Console.WriteLine("Contents:");
                foreach (var entertainItem in equipmentDataEntertainersPack.Items)
                {
                    Console.WriteLine($"- {entertainItem.Quantity}x {entertainItem.Item}");
                }
            }

            if (equipmentDataExplorersPack != null && equipmentDataExplorersPack.Items != null)
            {
                Console.WriteLine("Explorers Pack");
                foreach (var explorerItem in equipmentDataExplorersPack.Items)
                {
                    Console.WriteLine($"- {explorerItem.Quantity}x {explorerItem.Item}");
                }
            }

            if (equipmentDataHolySymbols != null && equipmentDataHolySymbols.Items != null)
            {
                Console.WriteLine("Holy Symbols");
                foreach (var holyItem in equipmentDataHolySymbols.Items)
                {
                    Console.WriteLine($"- {holyItem.Quantity}x {holyItem.Item}");
                }
            }

            if (equipmentDataPreistsPack != null && equipmentDataPreistsPack.Items != null)
            {
                Console.WriteLine("Priest Pack");
                foreach (var priestItem in equipmentDataPreistsPack.Items)
                {
                    Console.WriteLine($"- {priestItem.Quantity}x {priestItem.Item}");
                }
            }

            if (equipmentDataScholarsPack != null && equipmentDataScholarsPack.Items != null)
            {
                Console.WriteLine("Scholars Pack");
                foreach (var scholarsItem in equipmentDataScholarsPack.Items)
                {
                    Console.WriteLine($"- {scholarsItem.Quantity}x {scholarsItem.Item}");
                }
            }

            if (equipmentDataUsableItems != null && equipmentDataUsableItems.Items != null)
            {
                Console.WriteLine("Usable Items");
                foreach (var usableItem in equipmentDataUsableItems.Items)
                {
                    Console.WriteLine($"- {usableItem.Quantity}x {usableItem.Item}");
                }
            }

        }
    }
}

