using System;
using CloudDragonLib.Models;

namespace CloudDragonApi.Services
{
    public static class CombatRollService
    {
        private static readonly Random rng = new();

        public static int RollD20()
        {
            return rng.Next(1, 21);
        }

        public static (int roll, int total, bool success) RollSavingThrow(Character target, string ability, int dc)
        {
            target.Stats ??= new Dictionary<string, int>();

            int modifier = 0;
            if (target.Stats.TryGetValue(ability, out var statScore))
                modifier = (int)Math.Floor((statScore - 10) / 2.0);

            int roll = RollD20();
            int total = roll + modifier;
            bool success = total >= dc;

            return (roll, total, success);
        }

        public static (int roll, int total, bool hit) RollAttack(Character attacker, int targetAC, int attackModifier = 0)
        {
            int roll = RollD20();
            int total = roll + attackModifier;
            bool hit = total >= targetAC;

            return (roll, total, hit);
        }

        public static int RollDamage(string damageDice)
        {
            if (string.IsNullOrWhiteSpace(damageDice))
                return 1; // fallback minimal

            var parts = damageDice.ToLower().Split('d');
            if (parts.Length != 2 || !int.TryParse(parts[0], out var numDice) || !int.TryParse(parts[1], out var dieSize))
                return rng.Next(1, 5); // fallback 1d4

            int total = 0;
            for (int i = 0; i < numDice; i++)
                total += rng.Next(1, dieSize + 1);

            return total;
        }
    }
}
