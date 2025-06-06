using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragonApi;
using CloudDragonLib.Models;
using CloudDragonApi.Utils;


namespace CloudDragonApi
{

        public interface IRacesPopulator
        {
                Task<List<Races>> Populate(string fileName);
        }

	public class RacesPopulator : IRacesPopulator
    {
        public async Task<List<Races>> Populate(string fileName)
        {
            DebugLogger.Log($"Populating races from {fileName}");
            var races = new List<Races>();

            // Human
            Races humanRace = new Races("Human");
            humanRace.AddAbilityBonus("STR", 1);
            humanRace.AddAbilityBonus("DEX", 1);
            humanRace.AddAbilityBonus("CON", 1);
            humanRace.AddAbilityBonus("INT", 1);
            humanRace.AddAbilityBonus("WIS", 1);
            humanRace.AddAbilityBonus("CHA", 1);
            races.Add(humanRace);

            // Dwarves
            var dwarfHill = new Races("Dwarf (Hill)");
            dwarfHill.AddAbilityBonus("CON", 2);
            races.Add(dwarfHill);

            var dwarfMountain = new Races("Dwarf (Mountain)");
            dwarfMountain.AddAbilityBonus("STR", 2);
            dwarfMountain.AddAbilityBonus("CON", 2); // Seems like a bug in original, added CON again here
            races.Add(dwarfMountain);

            var dwarfGray = new Races("Dwarf (Gray/Duergar)");
            dwarfGray.AddAbilityBonus("STR", 1);
            dwarfGray.AddAbilityBonus("CON", 2);
            races.Add(dwarfGray);

            var dwarfDuergar = new Races("Dwarf (Duergar)");
            dwarfDuergar.AddAbilityBonus("STR", 1);
            dwarfDuergar.AddAbilityBonus("CON", 2);
            races.Add(dwarfDuergar);

            // Elves (a few to demo)
            var elfHigh = new Races("Elf (High)");
            elfHigh.AddAbilityBonus("DEX", 2);
            elfHigh.AddAbilityBonus("INT", 1);
            races.Add(elfHigh);

            var elfWood = new Races("Elf (Wood)");
            elfWood.AddAbilityBonus("DEX", 2);
            elfWood.AddAbilityBonus("WIS", 1);
            races.Add(elfWood);

            var elfDrow = new Races("Elf (Drow)");
            elfDrow.AddAbilityBonus("DEX", 2);
            elfDrow.AddAbilityBonus("CHA", 1);
            races.Add(elfDrow);

            // Tiefling
            var tiefling = new Races("Tiefling");
            tiefling.AddAbilityBonus("INT", 1);
            tiefling.AddAbilityBonus("CHA", 2);
            races.Add(tiefling);

            var tieflingFeral = new Races("Tiefling (Feral)");
            tieflingFeral.AddAbilityBonus("DEX", 2);
            tieflingFeral.AddAbilityBonus("INT", 1);
            races.Add(tieflingFeral);

            // Satyr
            var satyr = new Races("Satyr");
            satyr.AddAbilityBonus("DEX", 1);
            satyr.AddAbilityBonus("CHA", 2);
            races.Add(satyr);

            // Add more races as needed using the same pattern

            DebugLogger.Log($"Populated {races.Count} races");
            return await Task.FromResult(races);
        }
    }
}