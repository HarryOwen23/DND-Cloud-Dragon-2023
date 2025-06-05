using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudDragonLib.Models
{
    [ModelContext]
    public class Character
    {
        // Identity
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ModelField("The full name of the character.")]
        public string Name { get; set; }

        [ModelField("The race of the character, such as Elf, Dwarf, or Human.")]
        public string Race { get; set; }

        [ModelField("The age of the character.")]
        public int? Age { get; set; }

        [ModelField("The class of the character, such as Ranger, Wizard, or Paladin.")]
        public string Class { get; set; }
        public string Subclass { get; set; }

        // Multiclassing (optional)
        public Dictionary<string, int> Classes { get; set; } = new();
        public Dictionary<string, string> Subclasses { get; set; } = new();

        [ModelField("The level of the character.")]
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
        [ModelField("The character's personality traits, goals, or quirks.")]
        public string? Personality { get; set; }

        [ModelField("A physical description of the character's appearance.")]
        public string? Appearance { get; set; }

        [ModelField("The backstory of the character.")]
        public string? Backstory { get; set; }

        [ModelField("A quote that reflects this character's essence.")]
        public string? FlavorText { get; set; }

        // Character mechanics
        [ModelField("The character's core attributes such as strength, dexterity, etc.")]
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
                : Class;
    }
}
