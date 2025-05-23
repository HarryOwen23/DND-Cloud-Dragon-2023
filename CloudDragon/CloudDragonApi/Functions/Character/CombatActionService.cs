using System;
using System.Collections.Generic;
using CloudDragonLib.Models;

namespace CloudDragonApi.Services
{
    public static class CombatActionService
    {
        private static readonly Random rng = new Random();

        public static (bool hit, int roll, int total) ResolveAttackRoll(Character attacker, Character defender, int attackModifier = 0)
        {
            int roll = rng.Next(1, 21); // d20
            int total = roll + attackModifier;
            bool hit = total >= defender.AC;
            return (hit, roll, total);
        }

        public static void ApplyCondition(Character combatant, string condition)
        {
            if (combatant == null || string.IsNullOrEmpty(condition))
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

        public static void ApplyCoverBonus(Character combatant, string coverType)
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

        public static void HandleDodge(Character combatant)
        {
            ApplyCondition(combatant, "Dodging");
        }

       public static void ApplyDamage(Character target, int damage)
        {
            target.HP -= damage;
            if (target.HP <= 0)
            {
                target.HP = 0;
                if (!target.Conditions.Contains("Unconscious"))
                    target.Conditions.Add("Unconscious");
            }
        }
    }
}
