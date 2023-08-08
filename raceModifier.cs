using System;

class raceModifier
{
    static void Main()
    {
        // Creating races and adding ability score bonuses
        DndRace humanRace = new DndRace("Human");
        humanRace.AddAbilityScoreBonus("STR", 1);
        humanRace.AddAbilityScoreBonus("DEX", 1);
        humanRace.AddAbilityScoreBonus("CON", 1);
        humanRace.AddAbilityScoreBonus("INT", 1);
        humanRace.AddAbilityScoreBonus("WIS", 1);
        humanRace.AddAbilityScoreBonus("CHA", 1);

        // Dwarf Races
        DndRace dwarfHillRace = new DndRace("Dwarf (Hill)");
        dwarfHillRace.AddAbilityScoreBonus("CON", 2);

        DndRace dwarfMountainRace = new DndRace("Dwarf (Mountain)");
        dwarfMountainRace.AddAbilityScoreBonus("STR", 2);
        dwarfHillRace.AddAbilityScoreBonus("CON", 2);

        DndRace dwarfGrayRace = new DndRace("Dwarf (Gray/Duergar)");
        dwarfGrayRace.AddAbilityScoreBonus("STR", 1);
        dwarfGrayRace.AddAbilityScoreBonus("CON", 2);

        DndRace dwarfDuergarRace = new DndRace("Dwarf (Duergar)");
        dwarfDuergarRace.AddAbilityScoreBonus("STR", 1);
        dwarfDuergarRace.AddAbilityScoreBonus("CON", 2);

        // Elf Races
        DndRace elfHighRace = new DndRace("Elf (High)");
        elfHighRace.AddAbilityScoreBonus("DEX", 2);
        elfHighRace.AddAbilityScoreBonus("INT", 1);

        DndRace elfWoodRace = new DndRace("Elf (Wood)");
        elfWoodRace.AddAbilityScoreBonus("DEX", 2);
        elfWoodRace.AddAbilityScoreBonus("WIS", 1);

        DndRace elfDrowRace = new DndRace("Elf (Drow)");
        elfDrowRace.AddAbilityScoreBonus("DEX", 2);
        elfDrowRace.AddAbilityScoreBonus("CHA", 1);

        DndRace elfEladrinRace = new DndRace("Elf (Eladrin)");
        elfEladrinRace.AddAbilityScoreBonus("DEX", 2);
        elfEladrinRace.AddAbilityScoreBonus("INT", 1);

        DndRace elfSeaRace = new DndRace("Elf (Sea)");
        elfSeaRace.AddAbilityScoreBonus("DEX", 2);
        elfSeaRace.AddAbilityScoreBonus("CON", 1);

        DndRace elfShadarRace = new DndRace("Elf (Shadar-kai)");
        elfShadarRace.AddAbilityScoreBonus("DEX", 2);
        elfShadarRace.AddAbilityScoreBonus("CON", 1);

        // Halfling Races 
        DndRace halflingLfRace = new DndRace("Halfling (Lightfoot)");
        halflingLfRace.AddAbilityScoreBonus("DEX", 2);
        halflingLfRace.AddAbilityScoreBonus("CHA", 1);

        DndRace halflingStoutRace = new DndRace("Halfling (Stout)");
        halflingStoutRace.AddAbilityScoreBonus("DEX", 2);
        halflingStoutRace.AddAbilityScoreBonus("CON", 1);

        DndRace halflingGhostwiseRace = new DndRace("Halfling (Ghostwise)");
        halflingGhostwiseRace.AddAbilityScoreBonus("DEX", 2);
        halflingGhostwiseRace.AddAbilityScoreBonus("WIS", 1);

        // Gnome Races 
        DndRace GnomeForestRace = new DndRace("Gnome (Forest)");
        GnomeForestRace.AddAbilityScoreBonus("DEX", 1);
        GnomeForestRace.AddAbilityScoreBonus("INT", 2);

        DndRace GnomeRockRace = new DndRace("Gnome (Rock)");
        GnomeRockRace.AddAbilityScoreBonus("CON", 1);
        GnomeRockRace.AddAbilityScoreBonus("INT", 2);

        DndRace GnomeDeepRace = new DndRace("Gnome (Deep)");
        GnomeRockRace.AddAbilityScoreBonus("DEX", 1);
        GnomeRockRace.AddAbilityScoreBonus("INT", 2);

        // Orc Races 
        DndRace OrcRace = new DndRace("Orc");
        GnomeForestRace.AddAbilityScoreBonus("STR", 2);
        GnomeForestRace.AddAbilityScoreBonus("CON", 1);

        DndRace halfOrcRace = new DndRace("Half Orc");
        halfOrcRace.AddAbilityScoreBonus("STR", 2);
        halfOrcRace.AddAbilityScoreBonus("CON", 1);

        // Tiefling Race
        DndRace tieflingRace = new DndRace("Tiefling");
        tieflingRace.AddAbilityScoreBonus("INT", 1);
        tieflingRace.AddAbilityScoreBonus("CHA", 2);

        DndRace tieflingFeralRace = new DndRace("Tiefling (Feral)");
        tieflingFeralRace.AddAbilityScoreBonus("DEX", 2);
        tieflingFeralRace.AddAbilityScoreBonus("INT", 1);

        // Aasimar Races
        DndRace aasimarRace = new DndRace("Aasimar");
        aasimarRace.AddAbilityScoreBonus("WIS", 1);
        aasimarRace.AddAbilityScoreBonus("CHA", 2);

        DndRace aasimarProtectorRace = new DndRace("Aasimar (Protector)");
        aasimarProtectorRace.AddAbilityScoreBonus("WIS", 1);
        aasimarProtectorRace.AddAbilityScoreBonus("CHA", 2);

        DndRace aasimarScourgeRace = new DndRace("Aasimar (Scourge)");
        aasimarScourgeRace.AddAbilityScoreBonus("CON", 1);
        aasimarScourgeRace.AddAbilityScoreBonus("CHA", 2);

        DndRace aasimarFallenRace = new DndRace("Aasimar (Fallen)");
        aasimarFallenRace.AddAbilityScoreBonus("STR", 1);
        aasimarFallenRace.AddAbilityScoreBonus("CHA", 2);

        // Aarakocra Race
        DndRace aarakocraRace = new DndRace("Aarakocra");
        aarakocraRace.AddAbilityScoreBonus("DEX", 2);
        aarakocraRace.AddAbilityScoreBonus("WIS", 1);

        // Gensai Races
        DndRace gensaiAirRace = new DndRace("Genasi (Air)");
        gensaiAirRace.AddAbilityScoreBonus("DEX", 1);
        gensaiAirRace.AddAbilityScoreBonus("CON", 2);

        DndRace gensaiEarthRace = new DndRace("Genasi (Earth)");
        gensaiEarthRace.AddAbilityScoreBonus("STR", 1);
        gensaiEarthRace.AddAbilityScoreBonus("CON", 2);

        DndRace gensaiFireRace = new DndRace("Genasi (Fire)");
        gensaiFireRace.AddAbilityScoreBonus("CON", 2);
        gensaiFireRace.AddAbilityScoreBonus("INT", 1);

        DndRace gensaiWaterRace = new DndRace("Genasi (Water)");
        gensaiWaterRace.AddAbilityScoreBonus("CON", 2);
        gensaiWaterRace.AddAbilityScoreBonus("WIS", 1);

        // Goliath Race 
        DndRace goliathRace = new DndRace("Goliath");
        goliathRace.AddAbilityScoreBonus("STR", 2);
        goliathRace.AddAbilityScoreBonus("CON", 1);

        // Firbolg
        DndRace firbolgRace = new DndRace("Firbolg");
        firbolgRace.AddAbilityScoreBonus("STR", 1);
        firbolgRace.AddAbilityScoreBonus("WIS", 2);

        // Kenku
        DndRace kenkuRace = new DndRace("Kenku");
        kenkuRace.AddAbilityScoreBonus("DEX", 2);
        kenkuRace.AddAbilityScoreBonus("WIS",1);

        // Lizardfolk 
        DndRace lizardFolkRace = new DndRace("Lizardfolk");
        lizardFolkRace.AddAbilityScoreBonus("CON", 2);
        lizardFolkRace.AddAbilityScoreBonus("WIS", 1);

        // Tabaxi 
        DndRace tabaxiRace = new DndRace("Tabaxi");
        tabaxiRace.AddAbilityScoreBonus("DEX", 2);
        tabaxiRace.AddAbilityScoreBonus("CHA", 1);

        // Triton 
        DndRace tritonRace = new DndRace("Triton");
        tritonRace.AddAbilityScoreBonus("STR", 2);
        tritonRace.AddAbilityScoreBonus("CON", 1);
        tritonRace.AddAbilityScoreBonus("CHA", 1);

        // Bugbear
        DndRace bugBearRace = new DndRace("Tabaxi");
        bugBearRace.AddAbilityScoreBonus("STR", 2);
        bugBearRace.AddAbilityScoreBonus("CON", 1);

        // Goblin Races 
        DndRace goblinRace = new DndRace("Goblin");
        goblinRace.AddAbilityScoreBonus("DEX", 2);
        goblinRace.AddAbilityScoreBonus("CON", 1);

        DndRace hobGoblinRace = new DndRace("Hobgoblin");
        hobGoblinRace.AddAbilityScoreBonus("CON", 2);
        hobGoblinRace.AddAbilityScoreBonus("INT", 1);

        // Kobold Race 
        DndRace koboldRace = new DndRace("Kobold");
        koboldRace.AddAbilityScoreBonus("DEX", 2);

        // Yuan-ti Pureblood Race 
        DndRace yuantiPbRace = new DndRace("Yuan-ti Pureblood");
        yuantiPbRace.AddAbilityScoreBonus("INT", 1);
        yuantiPbRace.AddAbilityScoreBonus("CHA", 1);

        // Githyanki
        DndRace githyankiRace = new DndRace("Githyanki");
        yuantiPbRace.AddAbilityScoreBonus("STR", 2);
        yuantiPbRace.AddAbilityScoreBonus("INT", 1);

        // Githzerai	
        DndRace githyankiRace = new DndRace("Githzerai");
        yuantiPbRace.AddAbilityScoreBonus("INT", 1);
        yuantiPbRace.AddAbilityScoreBonus("WIS", 2);

        // Tortle 
        DndRace tortleRace = new DndRace("Tortle");
        tortleRace.AddAbilityScoreBonus("STR", 2);
        tortleRace.AddAbilityScoreBonus("WIS", 1);

        // Verdan
        DndRace verdanRace = new DndRace("Verdan");
        verdanRace.AddAbilityScoreBonus("CON", 2);
        verdanRace.AddAbilityScoreBonus("CHA", 2);

        // Kalashtar
        DndRace kalashtarRace = new DndRace("Kalashtar");
        kalashtarRace.AddAbilityScoreBonus("CON", 2);
        kalashtarRace.AddAbilityScoreBonus("CHA", 1);

        // Shifter Races 
        DndRace shifterBeasthideRace = new DndRace("Shifter (Beasthide)");
        shifterBeasthideRace.AddAbilityScoreBonus("STR", 1);
        shifterBeasthideRace.AddAbilityScoreBonus("CON", 2);

        DndRace shifterLongToothRace = new DndRace("Shifter (Longtooth)");
        shifterLongToothRace.AddAbilityScoreBonus("STR", 2);
        shifterLongToothRace.AddAbilityScoreBonus("DEX", 1);

        DndRace shifterSwiftstrideRace = new DndRace("Shifter (Swiftstride)");
        shifterSwiftstrideRace.AddAbilityScoreBonus("DEX", 2);
        shifterSwiftstrideRace.AddAbilityScoreBonus("CHA", 1);

        DndRace shifterWildhuntRace = new DndRace("Shifter (Wildhunt)");
        shifterWildhuntRace.AddAbilityScoreBonus("DEX", 1);
        shifterWildhuntRace.AddAbilityScoreBonus("WIS", 2);

        // Centaur 
        DndRace centaurRace = new DndRace("Centaur");
        centaurRace.AddAbilityScoreBonus("STR", 2);
        centaurRace.AddAbilityScoreBonus("WIS", 1);

        // Loxodon 
        DndRace loxodonRace = new DndRace("Loxodon");
        loxodonRace.AddAbilityScoreBonus("CON", 2);
        loxodonRace.AddAbilityScoreBonus("WIS", 1);

        // Minotaur 
        DndRace minotaurRace = new DndRace("Minotaur");
        minotaurRace.AddAbilityScoreBonus("STR", 2);
        minotaurRace.AddAbilityScoreBonus("CON", 1);

        // Vedalken 
        DndRace vedalkenRace = new DndRace("Vedalken");
        vedalkenRace.AddAbilityScoreBonus("INT", 2);
        vedalkenRace.AddAbilityScoreBonus("WIS", 1);

        // Leonin
        DndRace leoninRace = new DndRace("Leonin");
        vedalkenRace.AddAbilityScoreBonus("STR", 1);
        vedalkenRace.AddAbilityScoreBonus("CON", 2);

        // Satyr 
        DndRace SatyrRace = new DndRace("Satyr");
        SatyrRace.AddAbilityScoreBonus("DEX", 1);
        SatyrRace.AddAbilityScoreBonus("CHA", 2);

        // Example usage
        Console.WriteLine("Welcome to the character creation screen!");

        // Let's say the player chooses to play an Elf character
        DndRace selectedRace = ;

        Console.WriteLine("You've selected the " + selectedRace.Name + " race.");
        Console.WriteLine("Ability Score Bonuses:");

        foreach (var bonus in selectedRace.AbilityScoreBonuses)
        {
            Console.WriteLine(bonus.Key + ": " + bonus.Value);
        }

        // Now, you can apply these ability score bonuses to your character's stats.
        // You can implement the character's stats and additional logic based on your game's design.
    }
}
