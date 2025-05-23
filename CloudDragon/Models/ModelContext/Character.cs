[ModelContext]
public class Character
{
    public string Id { get; set; }

    [ModelField("The full name of the character.")]
    public string Name { get; set; }

    [ModelField("The race of the character, such as Elf, Dwarf, or Human.")]
    public string Race { get; set; }

    [ModelField("The class of the character, such as Ranger, Wizard, or Paladin.")]
    public string Class { get; set; }

    [ModelField("The level of the character.")]
    public int Level { get; set; }

    [ModelField("A physical description of the character's appearance.")]
    public string? Appearance { get; set; }

    [ModelField("The character's personality traits, goals, or quirks.")]
    public string? Personality { get; set; }

    [ModelField("The backstory of the character.")]
    public string? Backstory { get; set; }

    [ModelField("The character's core attributes such as strength, dexterity, etc.")]
    public Dictionary<string, int> Stats { get; set; } = new();


    // Any fields not relevant for prompt generation (like CosmosDB metadata, timestamps, etc.) can be left unannotated
}
