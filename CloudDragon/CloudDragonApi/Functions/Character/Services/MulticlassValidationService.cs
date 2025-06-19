using System;
using System.Collections.Generic;
using System.Linq;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    public static class MulticlassValidationService
    {
        public static List<string> ValidateMulticlass(CharacterModel character)
        {
            var errors = new List<string>();

            if (character == null)
            {
                errors.Add("Character is null.");
                return errors;
            }

            // If not multiclassing, skip validation
            if (character.Classes == null || character.Classes.Count == 0)
                return errors;

            // Validate total levels match character level
            int totalClassLevels = character.Classes.Values.Sum();
            if (totalClassLevels != character.Level)
            {
                errors.Add($"Total class levels ({totalClassLevels}) do not match character level ({character.Level}).");
            }

            // Validate each class has a valid name and level > 0
            foreach (var kvp in character.Classes)
            {
                string className = kvp.Key?.Trim();
                int level = kvp.Value;

                if (string.IsNullOrWhiteSpace(className))
                    errors.Add("A class name is missing or empty.");

                if (level <= 0)
                    errors.Add($"Class '{className}' has invalid level {level} (must be > 0).");

                if (level > 20)
                    errors.Add($"Class '{className}' exceeds maximum level (20).");
            }

            // Future: add class-specific rules like prerequisites
            // e.g., Require INT ≥ 13 for Wizard, STR ≥ 13 for Fighter

            return errors;
        }
    }
}
