using System;
using System.Collections.Generic;
using CloudDragon.CloudDragonApi.Functions.Combat;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    public static class CombatActionService
    {
        private static readonly Random rng = Random.Shared;

        public static (bool hit, int roll, int total) ResolveAttackRoll(Combatant attacker, Combatant defender, int attackModifier = 0)
        {
            int roll = rng.Next(1, 21); // d20
            int total = roll + attackModifier;
            bool hit = total >= defender.AC;
            return (hit, roll, total);
        }

        public static void ApplyCondition(Combatant combatant, string condition)
        {
            if (combatant == null || string.IsNullOrEmpty(condition))
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

        public static void HandleDodge(Combatant combatant)
        {
            ApplyCondition(combatant, "Dodging");
        }

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
