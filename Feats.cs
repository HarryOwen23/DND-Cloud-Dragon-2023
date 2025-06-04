using System;

/// <summary>
/// Represents a single feat with a name and description.
/// </summary>
public class Feat
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Feat(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
