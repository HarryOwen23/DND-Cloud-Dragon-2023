using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CloudDragonLib.Models;

namespace CloudDragonLib
{
    /// <summary>
    /// Demonstrates creation of <see cref="Races"/> objects using a data driven
    /// approach rather than many hard coded statements.
    /// </summary>
    internal static class RaceModifier
    {
        /// <summary>
        /// Map of race names to their ability score bonuses.
        /// </summary>
        private static readonly Dictionary<string, Dictionary<StringMarshalling int>> RaceDefinitions = new()
        {
            ["Human"] = new()
            {
                ["STR"] = 1,
                ["DEX"] = 1,
                ["CON"] = 1,
                ["INT"] = 1,
                ["WIS"] = 1,
                ["CHA"] = 1,
            },
            ["Dwarf (Hill)"] = new() { ["CON"] = 2 },
            ["Dwarf (Mountain)"] = new() { ["STR"] = 2, ["CON"] = 2 },
            ["Dwarf (Gray/Duergar)"] = new() { ["STR"] = 1, ["CON"] = 2 },
            ["Dwarf (Duergar)"] = new() { ["STR"] = 1, ["CON"] = 2 },
            ["Elf (High)"] = new() { ["DEX"] = 2, ["INT"] = 1 },
            ["Elf (Wood)"] = new() { ["DEX"] = 2, ["WIS"] = 1 },
            ["Elf (Drow)"] = new() { ["DEX"] = 2, ["CHA"] = 1 },
            ["Elf (Eladrin)"] = new() { ["DEX"] = 2, ["INT"] = 1 },
            ["Elf (Sea)"] = new() { ["DEX"] = 2, ["CON"] = 1 },
            ["Elf (Shadar-kai)"] = new() { ["DEX"] = 2, ["CON"] = 1 },
            ["Halfling (Lightfoot)"] = new() { ["DEX"] = 2, ["CHA"] = 1 },
            ["Halfling (Stout)"] = new() { ["DEX"] = 2, ["CON"] = 1 },
            ["Halfling (Ghostwise)"] = new() { ["DEX"] = 2, ["WIS"] = 1 },
            ["Gnome (Forest)"] = new() { ["DEX"] = 1, ["INT"] = 2 },
            ["Gnome (Rock)"] = new() { ["CON"] = 1, ["INT"] = 2 },
            ["Gnome (Deep)"] = new() { ["DEX"] = 1, ["INT"] = 2 },
            ["Orc"] = new() { ["STR"] = 2, ["CON"] = 1 },
            ["Half Orc"] = new() { ["STR"] = 2, ["CON"] = 1 },
            ["Tiefling"] = new() { ["INT"] = 1, ["CHA"] = 2 },
            ["Tiefling (Feral)"] = new() { ["DEX"] = 2, ["INT"] = 1 },
            ["Aasimar"] = new() { ["WIS"] = 1, ["CHA"] = 2 },
            ["Aasimar (Protector)"] = new() { ["WIS"] = 1, ["CHA"] = 2 },
            ["Aasimar (Scourge)"] = new() { ["CON"] = 1, ["CHA"] = 2 },
            ["Aasimar (Fallen)"] = new() { ["STR"] = 1, ["CHA"] = 2 },
            ["Aarakocra"] = new() { ["DEX"] = 2, ["WIS"] = 1 },
            ["Genasi (Air)"] = new() { ["DEX"] = 1, ["CON"] = 2 },
            ["Genasi (Earth)"] = new() { ["STR"] = 1, ["CON"] = 2 },
            ["Genasi (Fire)"] = new() { ["CON"] = 2, ["INT"] = 1 },
            ["Genasi (Water)"] = new() { ["CON"] = 2, ["WIS"] = 1 },
            ["Goliath"] = new() { ["STR"] = 2, ["CON"] = 1 },
            ["Firbolg"] = new() { ["STR"] = 1, ["WIS"] = 2 },
            ["Kenku"] = new() { ["DEX"] = 2, ["WIS"] = 1 },
            ["Lizardfolk"] = new() { ["CON"] = 2, ["WIS"] = 1 },
            ["Tabaxi"] = new() { ["DEX"] = 2, ["CHA"] = 1 },
            ["Triton"] = new() { ["STR"] = 2, ["CON"] = 1, ["CHA"] = 1 },
            ["Bugbear"] = new() { ["STR"] = 2, ["CON"] = 1 },
            ["Goblin"] = new() { ["DEX"] = 2, ["CON"] = 1 },
            ["Hobgoblin"] = new() { ["CON"] = 2, ["INT"] = 1 },
            ["Kobold"] = new() { ["DEX"] = 2 },
            ["Yuan-ti Pureblood"] = new() { ["INT"] = 1, ["CHA"] = 1 },
            ["Githyanki"] = new() { ["STR"] = 2, ["INT"] = 1 },
            ["Githzerai"] = new() { ["INT"] = 1, ["WIS"] = 2 },
            ["Tortle"] = new() { ["STR"] = 2, ["WIS"] = 1 },
            ["Verdan"] = new() { ["CON"] = 2, ["CHA"] = 2 },
            ["Kalashtar"] = new() { ["CON"] = 2, ["CHA"] = 1 },
            ["Shifter (Beasthide)"] = new() { ["STR"] = 1, ["CON"] = 2 },
            ["Shifter (Longtooth)"] = new() { ["STR"] = 2, ["DEX"] = 1 },
            ["Shifter (Swiftstride)"] = new() { ["DEX"] = 2, ["CHA"] = 1 },
            ["Shifter (Wildhunt)"] = new() { ["DEX"] = 1, ["WIS"] = 2 },
            ["Centaur"] = new() { ["STR"] = 2, ["WIS"] = 1 },
            ["Loxodon"] = new() { ["CON"] = 2, ["WIS"] = 1 },
            ["Minotaur"] = new() { ["STR"] = 2, ["CON"] = 1 },
            ["Vedalken"] = new() { ["INT"] = 2, ["WIS"] = 1 },
            ["Leonin"] = new() { ["STR"] = 1, ["CON"] = 2 },
            ["Satyr"] = new() { ["DEX"] = 1, ["CHA"] = 2 },
        };

        /// <summary>
        /// Entry point demonstrating selection of a race + output of it's bonuses. 
        /// </summary>
        private static void Main()
        {
            var races = RaceDefinitions
                .Select(d => CreateRace(d.Key, d.Value))
                .ToDictionary(r => r.Name, r => r);

            Console.WriteLine("Welcome to the character creation screen!");
            var selectedRace = races["Human"];
            Console.WriteLine($"You've selected the {selectedRace.Name} race.");
            Console.WriteLine("Ability Score Bonuses:");
            foreach (var bonus in selectedRace.AbilityBonuses)
                Console.WriteLine($"{bonus.Key}: {bonus.Value}");
        }

        /// <summary>
        /// Creates a <see cref="Races"/> instance and applies the provided ability bonuses.
        /// </summary>
        /// <param name="name">Race name.</param>
        /// <param name="bonuses">Ability bonuses keyed by ability abbreviation.</param>
        /// <returns>Newly created race.</returns>
        private static Races CreateRace(string name, Dictionary<string, int> bonuses)
        {
            var race = new Races(name);
            foreach (var kv in bonuses)
                race.AddAbilityBonus(kv.Key, kv.Value);
            return race;
        }
    }
}
