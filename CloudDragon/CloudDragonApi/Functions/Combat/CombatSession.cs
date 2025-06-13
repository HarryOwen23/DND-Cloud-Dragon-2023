using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CloudDragonApi.Models;

namespace CloudDragonApi.Models
{
    /// <summary>
    /// Represents the state of an ongoing combat encounter.
    /// </summary>
    public class CombatSession
    {
        /// <summary>
        /// Unique identifier for the session used as the Cosmos DB id/partition key.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Friendly name for the encounter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ordered list of combatants participating in the encounter.
        /// </summary>
        public List<Combatant> Combatants { get; set; } = new();

        /// <summary>
        /// Current round number of the encounter.
        /// </summary>
        public int Round { get; set; } = 1;

        /// <summary>
        /// Index of the combatant whose turn is currently active.
        /// </summary>
        public int TurnIndex { get; set; } = 0;

        /// <summary>
        /// Time the session was created in UTC.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Arbitrary log messages for auditing the encounter.
        /// </summary>
        public List<string> Log { get; set; } = new();
    }
}
