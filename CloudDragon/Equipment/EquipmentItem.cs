namespace CloudDragon.Equipment
{
    public class EquipmentItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }         // Weapon, Armor, etc.
        public string Category { get; set; }     // LightArmor, MartialMelee, etc.
        public string Slot { get; set; }         // Armor, MainHand, OffHand

        public int? ACBonus { get; set; }
        public string Damage { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
    }
}
