using System;

public class Feats
{
	public string featName { get; set; }
    public string FeatName { get; }
    public string Description { get; set; }

	public Feats(string featName, string description)
	{
		FeatName = featName;
		Description = description;
	}
}

