using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudDragonLib.Models
{
    public class Character
    {
        // Identity
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        // Single-class mode (default)
        public string ClassName { get; set; }        public string Subclass { get; set; }

        // Multiclassing (optional)
        public Dictionary<string, int> Classes { get; set; } = new();
        public Dictionary<string, string> Subclasses { get; set; } = new();

        // Race and Level
        public string Race { get; set; }

        // Dynamically calculated level
        public int Level
        {
            get
            {
                if (Classes != null && Classes.Count > 0)
                    return Classes.Values.Sum();
                return _level;
            }
            set
            {
                _level = value;
            }
        }
        private int _level;

        // Personality and flavor
        public string Personality { get; set; }
        public string? Appearance { get; set; }
        public string? Backstory { get; set; }
        public string? FlavorText { get; set; }

        // Character mechanics
        public Dictionary<string, int> Stats { get; set; } = new();
        public Dictionary<string, EquipmentItem> Equipped { get; set; } = new();
        public List<Item> Inventory { get; set; } = new();
        public int AC { get; set; } = 10;
        public float CarriedWeight { get; set; } = 0;

        // Spellcasting
        public Dictionary<int, int> SpellSlots { get; set; } = new(); // Key: Spell Level, Value: Slots Remaining
        public List<string> PreparedSpells { get; set; } = new();
        public List<string> CastedSpells { get; set; } = new();

        // Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Convenience: get most prominent class
        public string PrimaryClass =>
            Classes != null && Classes.Count > 0
                ? Classes.OrderByDescending(c => c.Value).First().Key
                : ClassName;
    }
}
