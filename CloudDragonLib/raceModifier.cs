using System;
using CloudDragonLib.Models;

class raceModifier
{
    static void Main()
    {
        // Creating races and adding ability score bonuses
        Races humanRace = new Races("Human");
        humanRace.AddAbilityBonus("STR", 1);
        humanRace.AddAbilityBonus("DEX", 1);
        humanRace.AddAbilityBonus("CON", 1);
        humanRace.AddAbilityBonus("INT", 1);
        humanRace.AddAbilityBonus("WIS", 1);
        humanRace.AddAbilityBonus("CHA", 1);

        // Dwarf Races
        Races dwarfHillRace = new Races("Dwarf (Hill)");
        dwarfHillRace.AddAbilityBonus("CON", 2);

        Races dwarfMountainRace = new Races("Dwarf (Mountain)");
        dwarfMountainRace.AddAbilityBonus("STR", 2);
        dwarfHillRace.AddAbilityBonus("CON", 2);

        Races dwarfGrayRace = new Races("Dwarf (Gray/Duergar)");
        dwarfGrayRace.AddAbilityBonus("STR", 1);
        dwarfGrayRace.AddAbilityBonus("CON", 2);

        Races dwarfDuergarRace = new Races("Dwarf (Duergar)");
        dwarfDuergarRace.AddAbilityBonus("STR", 1);
        dwarfDuergarRace.AddAbilityBonus("CON", 2);

        // Elf Races
        Races elfHighRace = new Races("Elf (High)");
        elfHighRace.AddAbilityBonus("DEX", 2);
        elfHighRace.AddAbilityBonus("INT", 1);

        Races elfWoodRace = new Races("Elf (Wood)");
        elfWoodRace.AddAbilityBonus("DEX", 2);
        elfWoodRace.AddAbilityBonus("WIS", 1);

        Races elfDrowRace = new Races("Elf (Drow)");
        elfDrowRace.AddAbilityBonus("DEX", 2);
        elfDrowRace.AddAbilityBonus("CHA", 1);

        Races elfEladrinRace = new Races("Elf (Eladrin)");
        elfEladrinRace.AddAbilityBonus("DEX", 2);
        elfEladrinRace.AddAbilityBonus("INT", 1);

        Races elfSeaRace = new Races("Elf (Sea)");
        elfSeaRace.AddAbilityBonus("DEX", 2);
        elfSeaRace.AddAbilityBonus("CON", 1);

        Races elfShadarRace = new Races("Elf (Shadar-kai)");
        elfShadarRace.AddAbilityBonus("DEX", 2);
        elfShadarRace.AddAbilityBonus("CON", 1);

        // Halfling Races 
        Races halflingLfRace = new Races("Halfling (Lightfoot)");
        halflingLfRace.AddAbilityBonus("DEX", 2);
        halflingLfRace.AddAbilityBonus("CHA", 1);

        Races halflingStoutRace = new Races("Halfling (Stout)");
        halflingStoutRace.AddAbilityBonus("DEX", 2);
        halflingStoutRace.AddAbilityBonus("CON", 1);

        Races halflingGhostwiseRace = new Races("Halfling (Ghostwise)");
        halflingGhostwiseRace.AddAbilityBonus("DEX", 2);
        halflingGhostwiseRace.AddAbilityBonus("WIS", 1);

        // Gnome Races 
        Races gnomeForestRace = new Races("Gnome (Forest)");
        gnomeForestRace.AddAbilityBonus("DEX", 1);
        gnomeForestRace.AddAbilityBonus("INT", 2);

        Races gnomeRockRace = new Races("Gnome (Rock)");
        gnomeRockRace.AddAbilityBonus("CON", 1);
        gnomeRockRace.AddAbilityBonus("INT", 2);

        Races gnomeDeepRace = new Races("Gnome (Deep)");
        gnomeDeepRace.AddAbilityBonus("DEX", 1);
        gnomeDeepRace.AddAbilityBonus("INT", 2);

        // Orc Races 
        Races orcRace = new Races("Orc");
        orcRace.AddAbilityBonus("STR", 2);
        orcRace.AddAbilityBonus("CON", 1);

        Races halfOrcRace = new Races("Half Orc");
        halfOrcRace.AddAbilityBonus("STR", 2);
        halfOrcRace.AddAbilityBonus("CON", 1);

        // Tiefling Race
        Races tieflingRace = new Races("Tiefling");
        tieflingRace.AddAbilityBonus("INT", 1);
        tieflingRace.AddAbilityBonus("CHA", 2);

        Races tieflingFeralRace = new Races("Tiefling (Feral)");
        tieflingFeralRace.AddAbilityBonus("DEX", 2);
        tieflingFeralRace.AddAbilityBonus("INT", 1);

        // Aasimar Races
        Races aasimarRace = new Races("Aasimar");
        aasimarRace.AddAbilityBonus("WIS", 1);
        aasimarRace.AddAbilityBonus("CHA", 2);

        Races aasimarProtectorRace = new Races("Aasimar (Protector)");
        aasimarProtectorRace.AddAbilityBonus("WIS", 1);
        aasimarProtectorRace.AddAbilityBonus("CHA", 2);

        Races aasimarScourgeRace = new Races("Aasimar (Scourge)");
        aasimarScourgeRace.AddAbilityBonus("CON", 1);
        aasimarScourgeRace.AddAbilityBonus("CHA", 2);

        Races aasimarFallenRace = new Races("Aasimar (Fallen)");
        aasimarFallenRace.AddAbilityBonus("STR", 1);
        aasimarFallenRace.AddAbilityBonus("CHA", 2);

        // Aarakocra Race
        Races aarakocraRace = new Races("Aarakocra");
        aarakocraRace.AddAbilityBonus("DEX", 2);
        aarakocraRace.AddAbilityBonus("WIS", 1);

        // Gensai Races
        Races gensaiAirRace = new Races("Genasi (Air)");
        gensaiAirRace.AddAbilityBonus("DEX", 1);
        gensaiAirRace.AddAbilityBonus("CON", 2);

        Races gensaiEarthRace = new Races("Genasi (Earth)");
        gensaiEarthRace.AddAbilityBonus("STR", 1);
        gensaiEarthRace.AddAbilityBonus("CON", 2);

        Races gensaiFireRace = new Races("Genasi (Fire)");
        gensaiFireRace.AddAbilityBonus("CON", 2);
        gensaiFireRace.AddAbilityBonus("INT", 1);

        Races gensaiWaterRace = new Races("Genasi (Water)");
        gensaiWaterRace.AddAbilityBonus("CON", 2);
        gensaiWaterRace.AddAbilityBonus("WIS", 1);

        // Goliath Race 
        Races goliathRace = new Races("Goliath");
        goliathRace.AddAbilityBonus("STR", 2);
        goliathRace.AddAbilityBonus("CON", 1);

        // Firbolg
        Races firbolgRace = new Races("Firbolg");
        firbolgRace.AddAbilityBonus("STR", 1);
        firbolgRace.AddAbilityBonus("WIS", 2);

        // Kenku
        Races kenkuRace = new Races("Kenku");
        kenkuRace.AddAbilityBonus("DEX", 2);
        kenkuRace.AddAbilityBonus("WIS",1);

        // Lizardfolk 
        Races lizardFolkRace = new Races("Lizardfolk");
        lizardFolkRace.AddAbilityBonus("CON", 2);
        lizardFolkRace.AddAbilityBonus("WIS", 1);

        // Tabaxi 
        Races tabaxiRace = new Races("Tabaxi");
        tabaxiRace.AddAbilityBonus("DEX", 2);
        tabaxiRace.AddAbilityBonus("CHA", 1);

        // Triton 
        Races tritonRace = new Races("Triton");
        tritonRace.AddAbilityBonus("STR", 2);
        tritonRace.AddAbilityBonus("CON", 1);
        tritonRace.AddAbilityBonus("CHA", 1);

        // Bugbear
        Races bugBearRace = new Races("Bugbear");
        bugBearRace.AddAbilityBonus("STR", 2);
        bugBearRace.AddAbilityBonus("CON", 1);

        // Goblin Races 
        Races goblinRace = new Races("Goblin");
        goblinRace.AddAbilityBonus("DEX", 2);
        goblinRace.AddAbilityBonus("CON", 1);

        Races hobGoblinRace = new Races("Hobgoblin");
        hobGoblinRace.AddAbilityBonus("CON", 2);
        hobGoblinRace.AddAbilityBonus("INT", 1);

        // Kobold Race 
        Races koboldRace = new Races("Kobold");
        koboldRace.AddAbilityBonus("DEX", 2);

        // Yuan-ti Pureblood Race 
        Races yuantiPbRace = new Races("Yuan-ti Pureblood");
        yuantiPbRace.AddAbilityBonus("INT", 1);
        yuantiPbRace.AddAbilityBonus("CHA", 1);

        // Githyanki
        Races githyankiRace = new Races("Githyanki");
        githyankiRace.AddAbilityBonus("STR", 2);
        githyankiRace.AddAbilityBonus("INT", 1);

        // Githzerai	
        Races githzeraiRace = new Races("Githzerai");
        githzeraiRace.AddAbilityBonus("INT", 1);
        githzeraiRace.AddAbilityBonus("WIS", 2);

        // Tortle 
        Races tortleRace = new Races("Tortle");
        tortleRace.AddAbilityBonus("STR", 2);
        tortleRace.AddAbilityBonus("WIS", 1);

        // Verdan
        Races verdanRace = new Races("Verdan");
        verdanRace.AddAbilityBonus("CON", 2);
        verdanRace.AddAbilityBonus("CHA", 2);

        // Kalashtar
        Races kalashtarRace = new Races("Kalashtar");
        kalashtarRace.AddAbilityBonus("CON", 2);
        kalashtarRace.AddAbilityBonus("CHA", 1);

        // Shifter Races 
        Races shifterBeasthideRace = new Races("Shifter (Beasthide)");
        shifterBeasthideRace.AddAbilityBonus("STR", 1);
        shifterBeasthideRace.AddAbilityBonus("CON", 2);

        Races shifterLongToothRace = new Races("Shifter (Longtooth)");
        shifterLongToothRace.AddAbilityBonus("STR", 2);
        shifterLongToothRace.AddAbilityBonus("DEX", 1);

        Races shifterSwiftstrideRace = new Races("Shifter (Swiftstride)");
        shifterSwiftstrideRace.AddAbilityBonus("DEX", 2);
        shifterSwiftstrideRace.AddAbilityBonus("CHA", 1);

        Races shifterWildhuntRace = new Races("Shifter (Wildhunt)");
        shifterWildhuntRace.AddAbilityBonus("DEX", 1);
        shifterWildhuntRace.AddAbilityBonus("WIS", 2);

        // Centaur 
        Races centaurRace = new Races("Centaur");
        centaurRace.AddAbilityBonus("STR", 2);
        centaurRace.AddAbilityBonus("WIS", 1);

        // Loxodon 
        Races loxodonRace = new Races("Loxodon");
        loxodonRace.AddAbilityBonus("CON", 2);
        loxodonRace.AddAbilityBonus("WIS", 1);

        // Minotaur 
        Races minotaurRace = new Races("Minotaur");
        minotaurRace.AddAbilityBonus("STR", 2);
        minotaurRace.AddAbilityBonus("CON", 1);

        // Vedalken 
        Races vedalkenRace = new Races("Vedalken");
        vedalkenRace.AddAbilityBonus("INT", 2);
        vedalkenRace.AddAbilityBonus("WIS", 1);


        // Leonin
        Races leoninRace = new Races("Leonin");
        leoninRace.AddAbilityBonus("STR", 1);
        leoninRace.AddAbilityBonus("CON", 2);


        // Satyr 
        Races SatyrRace = new Races("Satyr");
        SatyrRace.AddAbilityBonus("DEX", 1);
        SatyrRace.AddAbilityBonus("CHA", 2);

        // Example usage
        Console.WriteLine("Welcome to the character creation screen!");

        // Let's say the player chooses to play an Elf character
        Races selectedRace = SatyrRace;

        Console.WriteLine("You've selected the " + selectedRace.Name + " race.");
        Console.WriteLine("Ability Score Bonuses:");

        foreach (var bonus in selectedRace.AbilityBonuses)
        {
            Console.WriteLine(bonus.Key + ": " + bonus.Value);
        }
    }
}
