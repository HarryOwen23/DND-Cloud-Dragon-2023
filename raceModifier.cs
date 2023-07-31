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

        // Example usage
        Console.WriteLine("Welcome to the character creation screen!");

        // Let's say the player chooses to play an Elf character
        DndRace selectedRace = elfRace;

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
