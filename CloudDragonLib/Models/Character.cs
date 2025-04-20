using System;
using System.Collections.Generic;
using CloudDragonLib.Models;

namespace CloudDragonLib.Models
{
    public class Character
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }

        public Dictionary<string, int> Stats { get; set; } = new();
        public Dictionary<string, EquipmentItem> Equipped { get; set; } = new();
        public List<Item> Inventory { get; set; } = new();
        public int AC { get; set; } = 10;
        public float CarriedWeight { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
