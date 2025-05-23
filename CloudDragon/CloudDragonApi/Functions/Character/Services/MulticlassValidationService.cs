using System;
using System.Collections.Generic;
using System.Linq;
using CloudDragonLib.Models;

namespace CloudDragonApi.Services
{
    public static class CharacterValidationService
    {
        public static List<string> ValidateCharacter(Character character)
        {
            var errors = new List<string>();

            if (character == null)
            {
                errors.Add("Character is null.");
                return errors;
            }

            // Validate basic fields
            if (string.IsNullOrWhiteSpace(character.Name))
                errors.Add("Name is missing.");

            if (string.IsNullOrWhiteSpace(character.Race))
                errors.Add("Race is missing.");

            if (string.IsNullOrWhiteSpace(character.Class))
                errors.Add("Class is missing.");

            // Validate stats
            if (character.Stats == null || character.Stats.Count != 6)
                errors.Add("Stats must have exactly 6 attributes.");
            else
            {
                foreach (var stat in character.Stats)
                {
                    if (stat.Value < 1 || stat.Value > 30)
                        errors.Add($"Stat {stat.Key} has invalid value {stat.Value} (must be between 1 and 30).");
                }
            }

            // Validate level
            if (character.Level <= 0 || character.Level > 20)
                errors.Add("Level must be between 1 and 20.");

            // Validate AC
            if (character.AC < 10 || character.AC > 30)
                errors.Add("Armor Class (AC) seems invalid.");

            // Validate carried weight
            if (character.CarriedWeight < 0)
                errors.Add("Carried weight cannot be negative.");

            // Validate inventory
            if (character.Inventory == null)
                errors.Add("Inventory is missing.");

            // Validate multiclass logic
            if (character.Classes != null)
            {
                int totalLevels = character.Classes.Values.Sum();
                if (totalLevels != character.Level)
                    errors.Add("Multiclass levels do not add up to total character level.");
            }

            // Validate spell slots if caster
            if (character.SpellSlots != null)
            {
                foreach (var slot in character.SpellSlots)
                {
                    if (slot.Key < 1 || slot.Key > 9 || slot.Value < 0)
                        errors.Add($"Invalid spell slot entry: Level {slot.Key} = {slot.Value}.");
                }
            }

            return errors;
        }
    }
}