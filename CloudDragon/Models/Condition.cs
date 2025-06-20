namespace CloudDragon.Models
{
    /// <summary>
    /// Represents a status condition that can affect a combatant.
    /// </summary>
    public class Condition
    {
        /// <summary>Name of the condition.</summary>
        public string Name { get; set; }

        /// <summary>Description of the effect.</summary>
        public string Effect { get; set; }

        /// <summary>Whether the condition ends automatically at turn end.</summary>
        public bool EndsOnTurnEnd { get; set; }
    }
}
