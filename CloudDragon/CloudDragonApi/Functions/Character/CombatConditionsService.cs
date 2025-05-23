using System.Collections.Generic;
using CloudDragonLib.Models;

namespace CloudDragonApi.Services
{
    public static class CombatConditionsService
    {
        public static void ApplyCondition(Character combatant, string condition)
        {
            if (combatant == null || string.IsNullOrWhiteSpace(condition))
                return;

            combatant.Conditions ??= new List<string>();

            if (!combatant.Conditions.Contains(condition))
                combatant.Conditions.Add(condition);
        }

        public static void RemoveCondition(Character combatant, string condition)
        {
            if (combatant?.Conditions == null)
                return;

            combatant.Conditions.Remove(condition);
        }

        public static bool HasCondition(Character combatant, string condition)
        {
            return combatant?.Conditions != null && combatant.Conditions.Contains(condition);
        }

        public static void ClearAllConditions(Character combatant)
        {
            combatant?.Conditions?.Clear();
        }
    }
}