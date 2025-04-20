using System;
using System.Collections.Generic;
using CloudDragonLib.Models;

namespace CloudDragon.Equipment
{
    public class EquipmentService
    {
        public bool Equip(Character character, EquipmentItem item, bool overwrite = false)
        {
            character.Equipped ??= new Dictionary<string, EquipmentItem>();

            if (character.Equipped.ContainsKey(item.Slot))
            {
                if (!overwrite)
                    throw new InvalidOperationException($"Slot '{item.Slot}' is already occupied.");

                // Unequip current item in slot
                Unequip(character, item.Slot);
            }

            character.Equipped[item.Slot] = item;
            character.CarriedWeight += item.Weight;

            if (item.Slot == "Armor" && item.ACBonus.HasValue)
                character.AC = 10 + item.ACBonus.Value;

            return true;
        }

        public bool Unequip(Character character, string slot)
        {
            if (character.Equipped == null || !character.Equipped.TryGetValue(slot, out var item))
                return false;

            character.Equipped.Remove(slot);
            character.CarriedWeight = Math.Max(0, character.CarriedWeight - item.Weight);

            if (slot == "Armor")
                character.AC = 10;

            return true;
        }

        public bool IsItemEquipped(Character character, string itemId)
        {
            return character.Equipped?.Values.Any(i => i.Id == itemId) ?? false;
        }
    }
}
