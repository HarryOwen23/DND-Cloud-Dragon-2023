using System;
using Xunit;
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using CloudDragonLib.Models;
using System.Collections.Generic;

namespace CloudDragon.Tests
{
    public class CombatRollServiceTests
    {
        [Fact]
        public void RollD20_returns_value_between_1_and_20()
        {
            for (int i = 0; i < 100; i++)
            {
                int roll = CombatRollService.RollD20();
                Assert.InRange(roll, 1, 20);
            }
        }

        [Fact]
        public void RollSavingThrow_applies_modifier_and_checks_range()
        {
            var character = new Character
            {
                Stats = new Dictionary<string, int>
                {
                    { "Strength", 15 }
                }
            };

            var (roll, total, success) = CombatRollService.RollSavingThrow(character, "Strength", 12);

            Assert.InRange(roll, 1, 20);
            int expectedModifier = (int)Math.Floor((15 - 10) / 2.0);
            Assert.Equal(roll + expectedModifier, total);
            Assert.Equal(total >= 12, success);
        }

        [Fact]
        public void RollDamage_returns_value_within_expected_range()
        {
            for (int i = 0; i < 100; i++)
            {
                int result = CombatRollService.RollDamage("2d6");
                Assert.InRange(result, 2, 12);
            }
        }
    }
}
