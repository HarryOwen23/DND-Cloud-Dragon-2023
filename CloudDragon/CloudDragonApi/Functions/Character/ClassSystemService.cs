using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudDragonLib.Models;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    public static class ClassSystemService
    {
        private static readonly Dictionary<string, int> SubclassUnlockLevels = new()
        {
            { "fighter", 3 },
            { "wizard", 2 },
            { "cleric", 1 },
            { "druid", 2 },
            { "sorcerer", 1 },
            { "warlock", 1 },
            { "rogue", 3 },
            { "ranger", 3 },
            { "barbarian", 3 },
            { "monk", 3 },
            { "paladin", 3 },
            { "bard", 3 },
            { "artificer", 3 }
        };

        public static void AssignClass(CharacterModel character, string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                throw new ArgumentException("Class name cannot be empty.");

            character.Class = className;
            character.Classes ??= new Dictionary<string, int>();
            character.Classes[className] = 1; // Start at level 1
        }

        public static void AssignSubclass(CharacterModel character, string subclassName)
        {
            if (string.IsNullOrWhiteSpace(subclassName))
                throw new ArgumentException("Subclass name cannot be empty.");

            if (character.Classes == null || character.Classes.Count == 0)
                throw new InvalidOperationException("Character must have at least one class to assign a subclass.");

            // Assume primary class for now
            string primaryClass = character.Class?.ToLower();
            if (string.IsNullOrEmpty(primaryClass))
                throw new InvalidOperationException("Character has no assigned class.");

            if (character.Level < GetSubclassUnlockLevel(primaryClass))
                throw new InvalidOperationException($"Character must be at least level {GetSubclassUnlockLevel(primaryClass)} to pick a subclass.");

            character.Subclass = subclassName;
            character.Subclasses ??= new Dictionary<string, string>();
            character.Subclasses[primaryClass] = subclassName;
        }

        public static bool ValidateMulticlass(CharacterModel character, string newClass)
        {
            if (character.Classes.ContainsKey(newClass.ToLower()))
                return false; // Can't multiclass into the same class again

            return true;
        }

        private static int GetSubclassUnlockLevel(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return 3;

            className = className.ToLower();
            return SubclassUnlockLevels.ContainsKey(className) ? SubclassUnlockLevels[className] : 3;
        }
    }
}
