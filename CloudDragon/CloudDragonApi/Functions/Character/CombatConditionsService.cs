using System.Collections.Generic;
using CloudDragon.CloudDragonApi.Functions.Combat;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    public static class CombatConditionsService
    {
        public static void ApplyCondition(Combatant combatant, string condition)
        {
            if (combatant == null || string.IsNullOrWhiteSpace(condition))
                return;

            combatant.Conditions ??= new List<string>();

            if (!combatant.Conditions.Contains(condition))
                combatant.Conditions.Add(condition);
        }

        public static void RemoveCondition(Combatant combatant, string condition)
        {
            if (combatant?.Conditions == null)
                return;

            combatant.Conditions.Remove(condition);
        }

        public static bool HasCondition(Combatant combatant, string condition)
        {
            return combatant?.Conditions != null && combatant.Conditions.Contains(condition);
        }

        public static void ClearAllConditions(Combatant combatant)
        {
            combatant?.Conditions?.Clear();
        }
    }
}
