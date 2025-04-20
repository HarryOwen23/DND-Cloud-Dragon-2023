using CloudDragonApi.Models; 
namespace CloudDragon.Equipment
{
    public class EquipmentService
    {
        public bool Equip(Character character, EquipmentItem item)
        {
            character.Equipped ??= new Dictionary<string, EquipmentItem>();

            if (character.Equipped.ContainsKey(item.Slot))
                throw new InvalidOperationException($"Slot '{item.Slot}' is already occupied.");

            character.Equipped[item.Slot] = item;
            character.CarriedWeight += item.Weight;

            if (item.Slot == "Armor" && item.ACBonus.HasValue)
                character.AC = 10 + item.ACBonus.Value;

            return true;
        }

        public bool Unequip(Character character, string slot)
        {
            if (character.Equipped == null || !character.Equipped.ContainsKey(slot))
                return false;

            var item = character.Equipped[slot];
            character.CarriedWeight -= item.Weight;
            character.Equipped.Remove(slot);

            if (slot == "Armor")
                character.AC = 10;

            return true;
        }
    }
}