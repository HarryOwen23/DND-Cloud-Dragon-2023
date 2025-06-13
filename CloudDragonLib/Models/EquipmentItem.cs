namespace CloudDragonLib.Models
{
    /// <summary>
    /// Represents a single piece of equipment that can be used by a character.
    /// </summary>
    public class EquipmentItem
    {
        /// <summary>
        /// Unique identifier for the item.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Display name of the equipment item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Broad item type such as Armor or Weapon.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Specific category like HeavyArmor or SimpleMelee.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Slot the item occupies (Armor, MainHand, etc.).
        /// </summary>
        public string Slot { get; set; }

        /// <summary>
        /// AC bonus provided by the item, if any.
        /// </summary>
        public int? ACBonus { get; set; }

        /// <summary>
        /// Damage string for weapons (e.g. "1d8 slashing").
        /// </summary>
        public string Damage { get; set; }

        /// <summary>
        /// Weight in pounds.
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Additional descriptive text.
        /// </summary>
        public string Description { get; set; }
    }
}
