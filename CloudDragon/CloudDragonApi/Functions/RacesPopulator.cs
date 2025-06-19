using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.CloudDragonApi;
using CloudDragonLib.Models;
using RacesModel = CloudDragonLib.Models.Races;
using CloudDragon.CloudDragonApi.Utils;


namespace CloudDragon.CloudDragonApi.Functions
{

    /// <summary>
    /// Provides races data used by various functions.
    /// </summary>
    public interface IRacesPopulator
    {
        /// <summary>
        /// Populates a list of races. The file name parameter is currently ignored.
        /// </summary>
        /// <param name="fileName">Optional source file name.</param>
        /// <returns>List of races.</returns>
        Task<List<RacesModel>> Populate(string fileName);
    }

    /// <summary>
    /// Basic in-memory implementation of <see cref="IRacesPopulator"/>.
    /// </summary>
    public class RacesPopulator : IRacesPopulator
    {
        public async Task<List<RacesModel>> Populate(string fileName)
        {
            DebugLogger.Log($"Populating races from {fileName}");
            var races = new List<RacesModel>();

            // Human
            RacesModel humanRace = new RacesModel("Human");
            humanRace.AddAbilityBonus("STR", 1);
            humanRace.AddAbilityBonus("DEX", 1);
            humanRace.AddAbilityBonus("CON", 1);
            humanRace.AddAbilityBonus("INT", 1);
            humanRace.AddAbilityBonus("WIS", 1);
            humanRace.AddAbilityBonus("CHA", 1);
            races.Add(humanRace);

            // Dwarves
            var dwarfHill = new RacesModel("Dwarf (Hill)");
            dwarfHill.AddAbilityBonus("CON", 2);
            races.Add(dwarfHill);

            var dwarfMountain = new RacesModel("Dwarf (Mountain)");
            dwarfMountain.AddAbilityBonus("STR", 2);
            dwarfMountain.AddAbilityBonus("CON", 2); // Seems like a bug in original, added CON again here
            races.Add(dwarfMountain);

            var dwarfGray = new RacesModel("Dwarf (Gray/Duergar)");
            dwarfGray.AddAbilityBonus("STR", 1);
            dwarfGray.AddAbilityBonus("CON", 2);
            races.Add(dwarfGray);

            var dwarfDuergar = new RacesModel("Dwarf (Duergar)");
            dwarfDuergar.AddAbilityBonus("STR", 1);
            dwarfDuergar.AddAbilityBonus("CON", 2);
            races.Add(dwarfDuergar);

            // Elves (a few to demo)
            var elfHigh = new RacesModel("Elf (High)");
            elfHigh.AddAbilityBonus("DEX", 2);
            elfHigh.AddAbilityBonus("INT", 1);
            races.Add(elfHigh);

            var elfWood = new RacesModel("Elf (Wood)");
            elfWood.AddAbilityBonus("DEX", 2);
            elfWood.AddAbilityBonus("WIS", 1);
            races.Add(elfWood);

            var elfDrow = new RacesModel("Elf (Drow)");
            elfDrow.AddAbilityBonus("DEX", 2);
            elfDrow.AddAbilityBonus("CHA", 1);
            races.Add(elfDrow);

            // Tiefling
            var tiefling = new RacesModel("Tiefling");
            tiefling.AddAbilityBonus("INT", 1);
            tiefling.AddAbilityBonus("CHA", 2);
            races.Add(tiefling);

            var tieflingFeral = new RacesModel("Tiefling (Feral)");
            tieflingFeral.AddAbilityBonus("DEX", 2);
            tieflingFeral.AddAbilityBonus("INT", 1);
            races.Add(tieflingFeral);

            // Satyr
            var satyr = new RacesModel("Satyr");
            satyr.AddAbilityBonus("DEX", 1);
            satyr.AddAbilityBonus("CHA", 2);
            races.Add(satyr);

            // Add more races as needed using the same pattern

            DebugLogger.Log($"Populated {races.Count} races");
            return await Task.FromResult(races);
        }
    }
}