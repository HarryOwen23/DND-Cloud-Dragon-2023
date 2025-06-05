using System;

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

    public Feats(string featName, string description)
    {
        FeatName = featName;
        Description = description;
    }

    public Feats()
    {
    }
}

