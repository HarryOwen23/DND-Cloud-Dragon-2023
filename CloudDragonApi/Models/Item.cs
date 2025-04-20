namespace CloudDragonApi.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; } // Weapon, Armor, Potion
        public float Weight { get; set; }
        public string Rarity { get; set; }
        public string Description { get; set; }
    }
}
