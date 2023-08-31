using System;
using System.Collections.Generic;

// Class created to allow users to select a race for characters 
public class Races
{
	public string Name { get; set; }
	public Dictionary<string, int> abilityBonuses { get; set; }

	public Races(string nameOfRace)
	{
		Name = nameOfRace;
		abilityBonuses = new Dictionary<string, int>();
	}

	public void Add_ability_Bonuses(string abilityName, int abilityBonus)
	{
		if (abilityBonuses.ContainsKey(abilityName))
		{
			abilityBonuses[abilityName] += abilityBonus;
        }
		else
		{
            abilityBonuses.Add(abilityName, abilityBonus);

        }
    }
    public void RemoveAbilityScoreBonus(string abilityName)
    {
        if (abilityBonuses.ContainsKey(abilityName))
        {
            abilityBonuses.Remove(abilityName);
        }
    } 
}
