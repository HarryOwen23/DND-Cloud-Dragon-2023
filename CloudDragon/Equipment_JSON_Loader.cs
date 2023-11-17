using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudDragon;

namespace CloudDragon
{
    public class Equipment
    {
        public List<EquipmentItem> EquipmentCategories { get; set; }
    }

    public class EquipmentCategory
    {
        [JsonPropertyName("Arcane Focus")]
public List<EquipmentItem>? ArcaneFocus { get; set; }

        [JsonPropertyName("Pack Name")]
        public string PackName { get; set; }

        [JsonPropertyName("Contents")]
        public List<EquipmentItem> Contents { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }
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
    }

    internal class EquipmentJsonLoader
    {
        public static EquipmentCategory? LoadEquipmentData(string jsonFilePath)
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
            // string jsonFilePathClothes = "Equipment\\Equipment_Clothes.json";
            string jsonFilePathCommonItems = "Equipment\\Equipment_Common_Items.json";
            string jsonFilePathContainers = "Equipment\\Equipment_Containers.json";
            string jsonFilePathDiplomatsPack = "Equipment\\Equipment_Diplomat\'s_Pack.json";
            string jsonFilePathDragonlance = "Equipment\\Equipment_Dragonlance.json";
            string jsonFilePathDruidicFocus = "Equipment\\Equipment_Druidic_Focus.json";
            string jsonFilePathDungeoneersPack = "Equipment\\Equipment_Dungeoneer\'s_Pack.json";
            string jsonFilePathEntertainersPack = "Equipment\\Equipment_Entertainer\'s_Pack.json";
            string jsonFilePathExplorersPack = "Equipment\\Equipment_Explorers_Pack.json";
            string jsonFilePathHolySymbols = "Equipment\\Equipment_Holy_Symbols.json";
            string jsonFilePathPreistsPack = "Equipment\\Equipment_Priest\'s_Pack.json";
            string jsonFilePathScholarsPack = "Equipment\\Equipment_Scholar\'s_Pack.json";
            string jsonFilePathUsableItems = "Equipment\\Equipment_Usable_Items.json";

            // Load the equipment data using the EquipmentJsonLoader
            var equipmentDataArcaneFocus = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathArcaneFocus);
            var equipmentDataBurglarPack = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathBurglarPack);
            // var equipmentDataClothes = EquipmentJsonLoader.LoadEquipmentData(jsonFilePathClothes);
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


            if (equipmentDataArcaneFocus != null)
            {
                Console.WriteLine("Arcane Focus Equipment:");
                foreach (var item in equipmentDataArcaneFocus.ArcaneFocus)
                {
                    Console.WriteLine($"- {item.Name}: {item.Cost}, {item.Description}");
                }
            }

            if (equipmentDataBurglarPack != null)
            {
                Console.WriteLine("Burglar's Pack Equipment:");
                var burglarPack = equipmentDataBurglarPack;
                Console.WriteLine($"Pack Name: {burglarPack.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in burglarPack.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            //if (equipmentDataClothes != null)
            //{
            //    Console.WriteLine("Clothes:");
            //    foreach (var item in equipmentDataClothes.EquipmentCategories[0].Clothes)
            //    {
            //        Console.WriteLine($"- {item.Name}: {item.Cost}, {item.Weight}");
            //    }
            //}

            if (equipmentDataCommonItems != null)
            {
                Console.WriteLine("Common Items Equipment:");
                var commonItems = equipmentDataCommonItems;
                Console.WriteLine($"Pack Name: {commonItems.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in commonItems.Contents)
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }

            if (equipmentDataContainers != null)
            {
                Console.WriteLine("Containers");
                var containers = equipmentDataContainers;
                Console.WriteLine($"Pack Name: {containers.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in containers.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataDiplomatsPack != null)
            {
                Console.WriteLine("Diplomats Pack");
                var diplomatsPack = equipmentDataDiplomatsPack;
                Console.WriteLine($"Pack Name: {diplomatsPack.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in diplomatsPack.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataDragonlance != null)
            {
                Console.WriteLine("Dragonlance");
                var dragonlance = equipmentDataDragonlance;
                Console.WriteLine($"Pack Name: {dragonlance.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in dragonlance.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataDruidicFocus != null)
            {
                Console.WriteLine("Druidic Focus Items");
                var druidicFocus = equipmentDataDruidicFocus;
                Console.WriteLine($"Pack Name: {druidicFocus.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in druidicFocus.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataDungeoneersPack != null)
            {
                Console.WriteLine("Dungeoneers Pack");
                var dungeoneersPack = equipmentDataDungeoneersPack;
                Console.WriteLine($"Pack Name: {dungeoneersPack.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in dungeoneersPack.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataDungeoneersPack != null)
            {
                Console.WriteLine("Entertainers Pack");
                var entertainersPack = equipmentDataDungeoneersPack; // Corrected variable name
                Console.WriteLine($"Pack Name: {entertainersPack.PackName}");

                // Additional null check for Contents
                if (entertainersPack.Contents != null)
                {
                    Console.WriteLine("Contents:");
                    foreach (var item in entertainersPack.Contents)
                    {
                        Console.WriteLine($"- {item.Quantity}x {item.Item}");
                    }
                }
                else
                {
                    Console.WriteLine("Contents are null.");
                }
            }

            if (equipmentDataExplorersPack != null)
            {
                Console.WriteLine("Explorers Pack");
                var explorersPack = equipmentDataExplorersPack;
                Console.WriteLine($"Pack Name: {explorersPack.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in explorersPack.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataHolySymbols != null)
            {
                Console.WriteLine("Holy Symbols");
                var holySymbols = equipmentDataHolySymbols;
                Console.WriteLine($"Pack Name: {holySymbols.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in holySymbols.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataPreistsPack != null)
            {
                Console.WriteLine("Priest Pack");
                var preistPack = equipmentDataPreistsPack;
                Console.WriteLine($"Pack Name: {preistPack.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in preistPack.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataScholarsPack != null)
            {
                Console.WriteLine("Scholars Pack");
                var scholarsPack = equipmentDataScholarsPack;
                Console.WriteLine($"Pack Name: {scholarsPack.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in scholarsPack.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }

            if (equipmentDataUsableItems != null)
            {
                Console.WriteLine("Usable Items");
                var usableItems = equipmentDataUsableItems;
                Console.WriteLine($"Pack Name: {usableItems.PackName}");
                Console.WriteLine("Contents:");
                foreach (var item in usableItems.Contents)
                {
                    Console.WriteLine($"- {item.Quantity}x {item.Item}");
                }
            }
        }
    }
}

