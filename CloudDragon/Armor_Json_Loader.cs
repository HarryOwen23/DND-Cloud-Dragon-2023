using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Armor
    {
        public string Name { get; set; }
        public string ArmorClass { get; set; }
        public string Strength { get; set; }
        public string Stealth { get; set; }
        public string Weight { get; set; }
        public string Cost { get; set; }
    }

    public class ArmorCategory
    {
        public List<Armor> Armors { get; set; }
    }

    public class ArmorData
    {
        public Dictionary<string, List<Armor>> ArmorCategories { get; set; }
    }
    internal class Armor_Json_Loader
    {
        public ArmorData LoadArmorData()
        {
            // Read the JSON data from your files or strings
            string heavyArmorJson = File.ReadAllText("heavy_armor.json");
            string lightArmorJson = File.ReadAllText("light_armor.json");
            string mediumArmorJson = File.ReadAllText("medium_armor.json");

            // Deserialize the JSON into C# objects
            ArmorCategory heavyArmor = JsonSerializer.Deserialize<ArmorCategory>(heavyArmorJson);
            ArmorCategory lightArmor = JsonSerializer.Deserialize<ArmorCategory>(lightArmorJson);
            ArmorCategory mediumArmor = JsonSerializer.Deserialize<ArmorCategory>(mediumArmorJson);

            // Create an ArmorData object to store all categories
            ArmorData armorData = new ArmorData
            {
                ArmorCategories = new Dictionary<string, List<Armor>>
                {
                    { "Heavy Armor", heavyArmor.Armors },
                    { "Light Armor", lightArmor.Armors },
                    { "Medium Armor", mediumArmor.Armors }
                }
            };

            return armorData;
        }
    }
}
