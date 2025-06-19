using System;

namespace DNDCloudDragon
{
/// <summary>
/// Represents a single feat with a name and description.
/// </summary>
public class Feat
{
    /// <summary>
    /// Name of the feat.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Text describing the feat's effects.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Feat"/> class.
    /// </summary>
    /// <param name="name">Name of the feat.</param>
    /// <param name="description">Feat description.</param>
    public Feat(string name, string description)
    {
        Name = name;
        Description = description;
        Console.WriteLine($"Created feat {name}");
    }
}
}
