namespace CloudDragon.Models
{
    /// <summary>
    /// Basic representation of an inventory item.
    /// </summary>
    public class Item
    {
        /// <summary>Name of the item.</summary>
        public string Name { get; set; }

        /// <summary>Item category (e.g. Weapon, Armor, Potion).</summary>
        public string Type { get; set; }

        /// <summary>Weight in pounds.</summary>
        public float Weight { get; set; }

        /// <summary>Rarity classification.</summary>
        public string Rarity { get; set; }

        /// <summary>Text description.</summary>
        public string Description { get; set; }
    }
}
