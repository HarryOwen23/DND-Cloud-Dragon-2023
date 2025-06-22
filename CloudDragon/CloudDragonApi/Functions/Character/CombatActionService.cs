using System;
using System.Collections.Generic;
using CloudDragon.CloudDragonApi.Functions.Combat;
using CloudDragon.Models;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    /// <summary>
    /// Utility methods for resolving basic combat actions.
    /// </summary>
    public static class CombatActionService
    {
        private static readonly Random rng = Random.Shared;

        /// <summary>
        /// Performs an attack roll against a defender.
        /// </summary>
        /// <param name="attacker">The attacking combatant.</param>
        /// <param name="defender">The target combatant.</param>
        /// <param name="attackModifier">Attack bonus to apply.</param>
        /// <returns>Tuple describing hit result and roll totals.</returns>
        public static (bool hit, int roll, int total) ResolveAttackRoll(Combatant attacker, Combatant defender, int attackModifier = 0)
        {
            int roll = rng.Next(1, 21); // d20
            int total = roll + attackModifier;
            bool hit = total >= defender.AC;
            return (hit, roll, total);
        }

        /// <summary>
        /// Applies a combat condition if not already present.
        /// </summary>
        /// <param name="combatant">Target combatant.</param>
        /// <param name="condition">Condition name.</param>
        public static void ApplyCondition(Combatant combatant, string condition)
        {
            if (combatant == null || string.IsNullOrEmpty(condition))
                return;

            combatant.Conditions ??= new List<string>();

            if (!combatant.Conditions.Contains(condition))
                combatant.Conditions.Add(condition);
        }

        /// <summary>
        /// Removes a specific condition from the combatant.
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
        /// Applies a cover AC bonus and annotates the condition.
        /// </summary>
        /// <param name="combatant">Combatant receiving cover.</param>
        /// <param name="coverType">Type of cover (half or three-quarters).</param>
        public static void ApplyCoverBonus(Combatant combatant, string coverType)
        {
            if (combatant == null || string.IsNullOrWhiteSpace(coverType))
                return;

            int bonus = coverType.ToLower() switch
            {
                "half" => 2,
                "three-quarters" => 5,
                _ => 0
            };

            combatant.AC += bonus;
            ApplyCondition(combatant, $"Cover ({coverType})");
        }

        /// <summary>
        /// Marks the combatant as dodging until their next turn.
        /// </summary>
        /// <param name="combatant">Combatant taking the dodge action.</param>
        public static void HandleDodge(Combatant combatant)
        {
            ApplyCondition(combatant, "Dodging");
        }

        /// <summary>
        /// Applies damage to the target and handles unconsciousness.
        /// </summary>
        /// <param name="target">Combatant taking damage.</param>
        /// <param name="damage">Amount of hit point damage.</param>
        public static void ApplyDamage(Combatant target, int damage)
        {
            target.HP -= damage;
            target.Conditions ??= new List<string>();
            if (target.HP <= 0)
            {
                target.HP = 0;
                if (!target.Conditions.Contains("Unconscious"))
                    target.Conditions.Add("Unconscious");
            }
        }
    }
}
