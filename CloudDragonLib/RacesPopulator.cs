using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.Lib;


namespace CloudDragon.Lib
{

	public interface IRacesPopulator
	{
		Task<List<Races>> Populate(string fileName);
	}

	public class RacesPopulator : IRacesPopulator
    {
        public async Task<List<Races>> Populate(string fileName)
        {
            var races = new List<Races>();

            // Human
            Races humanRace = new Races("Human");
            humanRace.Add_ability_Bonuses("STR", 1);
            humanRace.Add_ability_Bonuses("DEX", 1);
            humanRace.Add_ability_Bonuses("CON", 1);
            humanRace.Add_ability_Bonuses("INT", 1);
            humanRace.Add_ability_Bonuses("WIS", 1);
            humanRace.Add_ability_Bonuses("CHA", 1);
            races.Add(humanRace);

            // Dwarves
            var dwarfHill = new Races("Dwarf (Hill)");
            dwarfHill.Add_ability_Bonuses("CON", 2);
            races.Add(dwarfHill);

            var dwarfMountain = new Races("Dwarf (Mountain)");
            dwarfMountain.Add_ability_Bonuses("STR", 2);
            dwarfMountain.Add_ability_Bonuses("CON", 2); // Seems like a bug in original, added CON again here
            races.Add(dwarfMountain);

            var dwarfGray = new Races("Dwarf (Gray/Duergar)");
            dwarfGray.Add_ability_Bonuses("STR", 1);
            dwarfGray.Add_ability_Bonuses("CON", 2);
            races.Add(dwarfGray);

            var dwarfDuergar = new Races("Dwarf (Duergar)");
            dwarfDuergar.Add_ability_Bonuses("STR", 1);
            dwarfDuergar.Add_ability_Bonuses("CON", 2);
            races.Add(dwarfDuergar);

            // Elves (a few to demo)
            var elfHigh = new Races("Elf (High)");
            elfHigh.Add_ability_Bonuses("DEX", 2);
            elfHigh.Add_ability_Bonuses("INT", 1);
            races.Add(elfHigh);

            var elfWood = new Races("Elf (Wood)");
            elfWood.Add_ability_Bonuses("DEX", 2);
            elfWood.Add_ability_Bonuses("WIS", 1);
            races.Add(elfWood);

            var elfDrow = new Races("Elf (Drow)");
            elfDrow.Add_ability_Bonuses("DEX", 2);
            elfDrow.Add_ability_Bonuses("CHA", 1);
            races.Add(elfDrow);

            // Tiefling
            var tiefling = new Races("Tiefling");
            tiefling.Add_ability_Bonuses("INT", 1);
            tiefling.Add_ability_Bonuses("CHA", 2);
            races.Add(tiefling);

            var tieflingFeral = new Races("Tiefling (Feral)");
            tieflingFeral.Add_ability_Bonuses("DEX", 2);
            tieflingFeral.Add_ability_Bonuses("INT", 1);
            races.Add(tieflingFeral);

            // Satyr
            var satyr = new Races("Satyr");
            satyr.Add_ability_Bonuses("DEX", 1);
            satyr.Add_ability_Bonuses("CHA", 2);
            races.Add(satyr);

            // Add more races as needed using the same pattern

            return await Task.FromResult(races);
        }
    }
}