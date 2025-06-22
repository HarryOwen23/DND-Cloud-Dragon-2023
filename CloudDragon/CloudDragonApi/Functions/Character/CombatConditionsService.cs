using System.Collections.Generic;
using CloudDragon.CloudDragonApi.Functions.Combat;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    /// <summary>
    /// Helper methods for managing combat conditions on characters.
    /// </summary>
    public static class CombatConditionsService
    {
        /// <summary>
        /// Adds a condition to the combatant if not already present.
        /// </summary>
        /// <param name="combatant">Target combatant.</param>
        /// <param name="condition">Condition name.</param>
        public static void ApplyCondition(Combatant combatant, string condition)
        {
            if (combatant == null || string.IsNullOrWhiteSpace(condition))
                return;

            combatant.Conditions ??= new List<string>();

            if (!combatant.Conditions.Contains(condition))
                combatant.Conditions.Add(condition);
        }

        /// <summary>
        /// Removes the specified condition from the combatant.
        /// </summary>
        /// <param name="combatant">Target combatant.</param>
        /// <param name="condition">Condition to remove.</param>
        public static void RemoveCondition(Combatant combatant, string condition)
        {
            if (combatant?.Conditions == null)
                return;

            combatant.Conditions.Remove(condition);
        }

        /// <summary>
        /// Checks if the combatant currently has the given condition.
        /// </summary>
        /// <param name="combatant">Target combatant.</param>
        /// <param name="condition">Condition to check.</param>
        /// <returns><c>true</c> if present; otherwise <c>false</c>.</returns>
        public static bool HasCondition(Combatant combatant, string condition)
        {
            return combatant?.Conditions != null && combatant.Conditions.Contains(condition);
        }

        /// <summary>
        /// Removes all conditions from the combatant.
        /// </summary>
        /// <param name="combatant">Target combatant.</param>
        public static void ClearAllConditions(Combatant combatant)
        {
            combatant?.Conditions?.Clear();
        }
    }
}
