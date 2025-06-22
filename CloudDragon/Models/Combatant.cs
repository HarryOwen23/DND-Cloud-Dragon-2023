using System;
using System.Collections.Generic;
using CloudDragonLib.Models;
using Newtonsoft.Json;

namespace CloudDragon.Models
{
    /// <summary>
    /// Participant in a combat encounter such as a player or monster.
    /// </summary>
    public class Combatant
    {
        /// <summary>
        /// Unique identifier used as the Cosmos DB id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>Name displayed for the combatant.</summary>
        public string Name { get; set; }

        /// <summary>Modifier added to initiative rolls.</summary>
        public int InitiativeModifier { get; set; }

        /// <summary>Result of the initiative roll for the session.</summary>
        public int Initiative { get; set; }

        /// <summary>Current hit points.</summary>
        public int HP { get; set; }

        /// <summary>Armor class value.</summary>
        public int AC { get; set; }

        /// <summary>Active condition names affecting this combatant.</summary>
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

        /// <summary>True if <see cref="HP"/> is zero or below.</summary>
        public bool IsDowned => HP <= 0;
    }
}
