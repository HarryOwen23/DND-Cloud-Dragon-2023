using System.Collections.Generic;

namespace CloudDragonApi.Models
{
    public class Combatant
    {
        public string Name { get; set; }
        public int InitiativeModifier { get; set; }
        public int Initiative { get; set; } // Calculated when session starts
        public int HP { get; set; }
        public int AC { get; set; }

        public List<string> Conditions { get; set; } = new();
        public bool IsDowned => HP <= 0;
    }
}
