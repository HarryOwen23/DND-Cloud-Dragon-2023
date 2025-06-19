using System;
using System.Collections.Generic;
using CloudDragonLib.Models;
using Newtonsoft.Json;

namespace CloudDragon.Models
{
    public class Combatant
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int InitiativeModifier { get; set; }
        public int Initiative { get; set; } // Calculated when session starts
        public int HP { get; set; }
        public int AC { get; set; }

        public List<string> Conditions { get; set; } = new();

        /// <summary>
        /// Equipment worn or wielded by the combatant, keyed by slot.
        /// </summary>
        public Dictionary<string, EquipmentItem> Equipped { get; set; } = new();

        /// <summary>
        /// Optional ability scores for this combatant keyed by ability name
        /// (e.g. "Dexterity"). Used for initiative and other rolls.
        /// </summary>
        public Dictionary<string, int> Stats { get; set; } = new();
        public bool IsDowned => HP <= 0;
    }
}
