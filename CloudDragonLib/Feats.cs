using System;

namespace CloudDragonLib.Models
{
/// <summary>
/// Represents a single feat and its descriptive text.
/// </summary>
public class Feats
{
    /// <summary>
    /// Name of the feat.
    /// </summary>
    public string FeatName { get; set; }

    /// <summary>
    /// Description of the feat's effects.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Creates a new <see cref="Feats"/> with the provided name and description.
    /// </summary>
    /// <param name="featName">Feat name.</param>
    /// <param name="description">Feat description.</param>
    public Feats(string featName, string description)
    {
        FeatName = featName;
        Description = description;
        Console.WriteLine($"Created feat {featName}");
    }

    /// <summary>
    /// Initializes an empty feat. Primarily used when deserializing from JSON.
    /// </summary>
    public Feats()
    {
        Console.WriteLine("Created empty feat");
    }
}
}

