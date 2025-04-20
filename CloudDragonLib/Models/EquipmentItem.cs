namespace CloudDragonLib.Models
{
    public class EquipmentItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }         // Armor, Weapon, etc.
        public string Category { get; set; }     // HeavyArmor, SimpleMelee, etc.
        public string Slot { get; set; }         // Armor, MainHand, etc.
        public int? ACBonus { get; set; }
        public string Damage { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
    }
}
