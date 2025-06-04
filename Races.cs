using System.Collections.Generic;

/// <summary>
/// Represents a playable race and the ability score bonuses it grants.
/// </summary>
public class Race
{
    public string Name { get; set; }
    public Dictionary<string, int> AbilityScoreBonuses { get; } = new();

    public Race(string name)
    {
        Name = name;
    }

    public void AddAbilityScoreBonus(string ability, int bonus)
    {
        if (AbilityScoreBonuses.ContainsKey(ability))
            AbilityScoreBonuses[ability] += bonus;
        else
            AbilityScoreBonuses[ability] = bonus;
    }

    public void RemoveAbilityScoreBonus(string ability)
    {
        AbilityScoreBonuses.Remove(ability);
    }
}
