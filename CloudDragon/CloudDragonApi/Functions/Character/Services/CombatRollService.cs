using System;
using System.Collections.Generic;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    /// <summary>
    /// Provides dice rolling helpers for combat scenarios.
    /// </summary>
    public static class CombatRollService
    {
        private static readonly Random rng = Random.Shared;

        /// <summary>
        /// Rolls a d20 and logs the result.
        /// </summary>
        /// <returns>Random value between 1 and 20.</returns>
        public static int RollD20()
        {
            int roll = rng.Next(1, 21);
            DebugLogger.Log($"Rolled d20: {roll}");
            return roll;
        }

        /// <summary>
        /// Performs a saving throw roll for the specified ability.
        /// </summary>
        /// <param name="target">Character attempting the save.</param>
        /// <param name="ability">Ability name used for the save.</param>
        /// <param name="dc">Difficulty class to beat.</param>
        /// <returns>Tuple containing roll, total and success flag.</returns>
        public static (int roll, int total, bool success) RollSavingThrow(CharacterModel target, string ability, int dc)
        {
            target.Stats ??= new Dictionary<string, int>();

            int modifier = 0;
            if (target.Stats.TryGetValue(ability, out var statScore))
                modifier = (int)Math.Floor((statScore - 10) / 2.0);

            int roll = RollD20();
            int total = roll + modifier;
            bool success = total >= dc;
            DebugLogger.Log($"Saving throw for {ability}: roll={roll}, modifier={modifier}, total={total}, dc={dc}, success={success}");

            return (roll, total, success);
        }

        /// <summary>
        /// Performs an attack roll against a target AC.
        /// </summary>
        /// <param name="attacker">Attacking character.</param>
        /// <param name="targetAC">Armor class of the target.</param>
        /// <param name="attackModifier">Attack bonus to include.</param>
        /// <returns>Tuple containing roll, total and hit flag.</returns>
        public static (int roll, int total, bool hit) RollAttack(CharacterModel attacker, int targetAC, int attackModifier = 0)
        {
            int roll = RollD20();
            int total = roll + attackModifier;
            bool hit = total >= targetAC;
            DebugLogger.Log($"Attack roll: roll={roll}, attackModifier={attackModifier}, total={total}, targetAC={targetAC}, hit={hit}");

            return (roll, total, hit);
        }

        /// <summary>
        /// Rolls damage using standard dice notation (e.g. 2d6).
        /// </summary>
        /// <param name="damageDice">Damage dice expression.</param>
        /// <returns>Total damage rolled.</returns>
        public static int RollDamage(string damageDice)
        {
            if (string.IsNullOrWhiteSpace(damageDice))
                return 1;

            var parts = damageDice.ToLower().Split('d');
            if (parts.Length != 2 || !int.TryParse(parts[0], out var numDice) || !int.TryParse(parts[1], out var dieSize))
                return rng.Next(1, 5); // fallback 1d4

            int total = 0;
            for (int i = 0; i < numDice; i++)
            {
                int part = rng.Next(1, dieSize + 1);
                total += part;
                DebugLogger.Log($"Damage dice {i + 1}/{numDice}: {part}");
            }

            DebugLogger.Log($"Total damage for {damageDice}: {total}");

            return total;
        }
    }
}
