using System;

class raceModifier
{
    static void Main()
    {
        // Creating races and adding ability score bonuses
        Races humanRace = new Races("Human");
        humanRace.Add_ability_Bonuses("STR", 1);
        humanRace.Add_ability_Bonuses("DEX", 1);
        humanRace.Add_ability_Bonuses("CON", 1);
        humanRace.Add_ability_Bonuses("INT", 1);
        humanRace.Add_ability_Bonuses("WIS", 1);
        humanRace.Add_ability_Bonuses("CHA", 1);

        // Dwarf Races
        Races dwarfHillRace = new Races("Dwarf (Hill)");
        dwarfHillRace.Add_ability_Bonuses("CON", 2);

        Races dwarfMountainRace = new Races("Dwarf (Mountain)");
        dwarfMountainRace.Add_ability_Bonuses("STR", 2);
        dwarfHillRace.Add_ability_Bonuses("CON", 2);

        Races dwarfGrayRace = new Races("Dwarf (Gray/Duergar)");
        dwarfGrayRace.Add_ability_Bonuses("STR", 1);
        dwarfGrayRace.Add_ability_Bonuses("CON", 2);

        Races dwarfDuergarRace = new Races("Dwarf (Duergar)");
        dwarfDuergarRace.Add_ability_Bonuses("STR", 1);
        dwarfDuergarRace.Add_ability_Bonuses("CON", 2);

        // Elf Races
        Races elfHighRace = new Races("Elf (High)");
        elfHighRace.Add_ability_Bonuses("DEX", 2);
        elfHighRace.Add_ability_Bonuses("INT", 1);

        Races elfWoodRace = new Races("Elf (Wood)");
        elfWoodRace.Add_ability_Bonuses("DEX", 2);
        elfWoodRace.Add_ability_Bonuses("WIS", 1);

        Races elfDrowRace = new Races("Elf (Drow)");
        elfDrowRace.Add_ability_Bonuses("DEX", 2);
        elfDrowRace.Add_ability_Bonuses("CHA", 1);

        Races elfEladrinRace = new Races("Elf (Eladrin)");
        elfEladrinRace.Add_ability_Bonuses("DEX", 2);
        elfEladrinRace.Add_ability_Bonuses("INT", 1);

        Races elfSeaRace = new Races("Elf (Sea)");
        elfSeaRace.Add_ability_Bonuses("DEX", 2);
        elfSeaRace.Add_ability_Bonuses("CON", 1);

        Races elfShadarRace = new Races("Elf (Shadar-kai)");
        elfShadarRace.Add_ability_Bonuses("DEX", 2);
        elfShadarRace.Add_ability_Bonuses("CON", 1);

        // Halfling Races 
        Races halflingLfRace = new Races("Halfling (Lightfoot)");
        halflingLfRace.Add_ability_Bonuses("DEX", 2);
        halflingLfRace.Add_ability_Bonuses("CHA", 1);

        Races halflingStoutRace = new Races("Halfling (Stout)");
        halflingStoutRace.Add_ability_Bonuses("DEX", 2);
        halflingStoutRace.Add_ability_Bonuses("CON", 1);

        Races halflingGhostwiseRace = new Races("Halfling (Ghostwise)");
        halflingGhostwiseRace.Add_ability_Bonuses("DEX", 2);
        halflingGhostwiseRace.Add_ability_Bonuses("WIS", 1);

        // Gnome Races 
        Races GnomeForestRace = new Races("Gnome (Forest)");
        GnomeForestRace.Add_ability_Bonuses("DEX", 1);
        GnomeForestRace.Add_ability_Bonuses("INT", 2);

        Races GnomeRockRace = new Races("Gnome (Rock)");
        GnomeRockRace.Add_ability_Bonuses("CON", 1);
        GnomeRockRace.Add_ability_Bonuses("INT", 2);

        Races GnomeDeepRace = new Races("Gnome (Deep)");
        GnomeRockRace.Add_ability_Bonuses("DEX", 1);
        GnomeRockRace.Add_ability_Bonuses("INT", 2);

        // Orc Races 
        Races OrcRace = new Races("Orc");
        GnomeForestRace.Add_ability_Bonuses("STR", 2);
        GnomeForestRace.Add_ability_Bonuses("CON", 1);

        Races halfOrcRace = new Races("Half Orc");
        halfOrcRace.Add_ability_Bonuses("STR", 2);
        halfOrcRace.Add_ability_Bonuses("CON", 1);

        // Tiefling Race
        Races tieflingRace = new Races("Tiefling");
        tieflingRace.Add_ability_Bonuses("INT", 1);
        tieflingRace.Add_ability_Bonuses("CHA", 2);

        Races tieflingFeralRace = new Races("Tiefling (Feral)");
        tieflingFeralRace.Add_ability_Bonuses("DEX", 2);
        tieflingFeralRace.Add_ability_Bonuses("INT", 1);

        // Aasimar Races
        Races aasimarRace = new Races("Aasimar");
        aasimarRace.Add_ability_Bonuses("WIS", 1);
        aasimarRace.Add_ability_Bonuses("CHA", 2);

        Races aasimarProtectorRace = new Races("Aasimar (Protector)");
        aasimarProtectorRace.Add_ability_Bonuses("WIS", 1);
        aasimarProtectorRace.Add_ability_Bonuses("CHA", 2);

        Races aasimarScourgeRace = new Races("Aasimar (Scourge)");
        aasimarScourgeRace.Add_ability_Bonuses("CON", 1);
        aasimarScourgeRace.Add_ability_Bonuses("CHA", 2);

        Races aasimarFallenRace = new Races("Aasimar (Fallen)");
        aasimarFallenRace.Add_ability_Bonuses("STR", 1);
        aasimarFallenRace.Add_ability_Bonuses("CHA", 2);

        // Aarakocra Race
        Races aarakocraRace = new Races("Aarakocra");
        aarakocraRace.Add_ability_Bonuses("DEX", 2);
        aarakocraRace.Add_ability_Bonuses("WIS", 1);

        // Gensai Races
        Races gensaiAirRace = new Races("Genasi (Air)");
        gensaiAirRace.Add_ability_Bonuses("DEX", 1);
        gensaiAirRace.Add_ability_Bonuses("CON", 2);

        Races gensaiEarthRace = new Races("Genasi (Earth)");
        gensaiEarthRace.Add_ability_Bonuses("STR", 1);
        gensaiEarthRace.Add_ability_Bonuses("CON", 2);

        Races gensaiFireRace = new Races("Genasi (Fire)");
        gensaiFireRace.Add_ability_Bonuses("CON", 2);
        gensaiFireRace.Add_ability_Bonuses("INT", 1);

        Races gensaiWaterRace = new Races("Genasi (Water)");
        gensaiWaterRace.Add_ability_Bonuses("CON", 2);
        gensaiWaterRace.Add_ability_Bonuses("WIS", 1);

        // Goliath Race 
        Races goliathRace = new Races("Goliath");
        goliathRace.Add_ability_Bonuses("STR", 2);
        goliathRace.Add_ability_Bonuses("CON", 1);

        // Firbolg
        Races firbolgRace = new Races("/");
        firbolgRace.Add_ability_Bonuses("STR", 1);
        firbolgRace.Add_ability_Bonuses("WIS", 2);

        // Kenku
        Races kenkuRace = new Races("Kenku");
        kenkuRace.Add_ability_Bonuses("DEX", 2);
        kenkuRace.Add_ability_Bonuses("WIS",1);

        // Lizardfolk 
        Races lizardFolkRace = new Races("Lizardfolk");
        lizardFolkRace.Add_ability_Bonuses("CON", 2);
        lizardFolkRace.Add_ability_Bonuses("WIS", 1);

        // Tabaxi 
        Races tabaxiRace = new Races("Tabaxi");
        tabaxiRace.Add_ability_Bonuses("DEX", 2);
        tabaxiRace.Add_ability_Bonuses("CHA", 1);

        // Triton 
        Races tritonRace = new Races("Triton");
        tritonRace.Add_ability_Bonuses("STR", 2);
        tritonRace.Add_ability_Bonuses("CON", 1);
        tritonRace.Add_ability_Bonuses("CHA", 1);

        // Bugbear
        Races bugBearRace = new Races("Bugbear");
        bugBearRace.Add_ability_Bonuses("STR", 2);
        bugBearRace.Add_ability_Bonuses("CON", 1);

        // Goblin Races 
        Races goblinRace = new Races("Goblin");
        goblinRace.Add_ability_Bonuses("DEX", 2);
        goblinRace.Add_ability_Bonuses("CON", 1);

        Races hobGoblinRace = new Races("Hobgoblin");
        hobGoblinRace.Add_ability_Bonuses("CON", 2);
        hobGoblinRace.Add_ability_Bonuses("INT", 1);

        // Kobold Race 
        Races koboldRace = new Races("Kobold");
        koboldRace.Add_ability_Bonuses("DEX", 2);

        // Yuan-ti Pureblood Race 
        Races yuantiPbRace = new Races("Yuan-ti Pureblood");
        yuantiPbRace.Add_ability_Bonuses("INT", 1);
        yuantiPbRace.Add_ability_Bonuses("CHA", 1);

        // Githyanki
        Races githyankiRace = new Races("Githyanki");
        yuantiPbRace.Add_ability_Bonuses("STR", 2);
        yuantiPbRace.Add_ability_Bonuses("INT", 1);

        // Githzerai	
        Races githzeraiRace = new Races("Githzerai");
        githzeraiRace.Add_ability_Bonuses("INT", 1);
        githzeraiRace.Add_ability_Bonuses("WIS", 2);

        // Tortle 
        Races tortleRace = new Races("Tortle");
        tortleRace.Add_ability_Bonuses("STR", 2);
        tortleRace.Add_ability_Bonuses("WIS", 1);

        // Verdan
        Races verdanRace = new Races("Verdan");
        verdanRace.Add_ability_Bonuses("CON", 2);
        verdanRace.Add_ability_Bonuses("CHA", 2);

        // Kalashtar
        Races kalashtarRace = new Races("Kalashtar");
        kalashtarRace.Add_ability_Bonuses("CON", 2);
        kalashtarRace.Add_ability_Bonuses("CHA", 1);

        // Shifter Races 
        Races shifterBeasthideRace = new Races("Shifter (Beasthide)");
        shifterBeasthideRace.Add_ability_Bonuses("STR", 1);
        shifterBeasthideRace.Add_ability_Bonuses("CON", 2);

        Races shifterLongToothRace = new Races("Shifter (Longtooth)");
        shifterLongToothRace.Add_ability_Bonuses("STR", 2);
        shifterLongToothRace.Add_ability_Bonuses("DEX", 1);

        Races shifterSwiftstrideRace = new Races("Shifter (Swiftstride)");
        shifterSwiftstrideRace.Add_ability_Bonuses("DEX", 2);
        shifterSwiftstrideRace.Add_ability_Bonuses("CHA", 1);

        Races shifterWildhuntRace = new Races("Shifter (Wildhunt)");
        shifterWildhuntRace.Add_ability_Bonuses("DEX", 1);
        shifterWildhuntRace.Add_ability_Bonuses("WIS", 2);

        // Centaur 
        Races centaurRace = new Races("Centaur");
        centaurRace.Add_ability_Bonuses("STR", 2);
        centaurRace.Add_ability_Bonuses("WIS", 1);

        // Loxodon 
        Races loxodonRace = new Races("Loxodon");
        loxodonRace.Add_ability_Bonuses("CON", 2);
        loxodonRace.Add_ability_Bonuses("WIS", 1);

        // Minotaur 
        Races minotaurRace = new Races("Minotaur");
        minotaurRace.Add_ability_Bonuses("STR", 2);
        minotaurRace.Add_ability_Bonuses("CON", 1);

        // Vedalken 
        Races vedalkenRace = new Races("Vedalken");
        vedalkenRace.Add_ability_Bonuses("INT", 2);
        vedalkenRace.Add_ability_Bonuses("WIS", 1);

        // Leonin
        Races leoninRace = new Races("Leonin");
        vedalkenRace.Add_ability_Bonuses("STR", 1);
        vedalkenRace.Add_ability_Bonuses("CON", 2);

        // Satyr 
        Races SatyrRace = new Races("Satyr");
        SatyrRace.Add_ability_Bonuses("DEX", 1);
        SatyrRace.Add_ability_Bonuses("CHA", 2);

        // Example usage
        Console.WriteLine("Welcome to the character creation screen!");

        // Let's say the player chooses to play an Elf character
        Races selectedRace = SatyrRace;

        Console.WriteLine("You've selected the " + selectedRace.Name + " race.");
        Console.WriteLine("Ability Score Bonuses:");

        foreach (var bonus in selectedRace.abilityBonuses)
        {
            Console.WriteLine(bonus.Key + ": " + bonus.Value);
        }

        // Now, you can apply these ability score bonuses to your character's stats.
        // You can implement the character's stats and additional logic based on your game's design.
    }
}
