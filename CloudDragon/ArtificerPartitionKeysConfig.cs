using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.PartitionKeys
{
    /// <summary>
    /// Partition key names for Artificer data in Cosmos DB.
    /// </summary>
    public class ArtificerPartitionKeysConfig
    {
        /// <summary>Base Artificer partition.</summary>
        public string Base { get; set; }
        /// <summary>Alchemist subclass partition.</summary>
        public string Alchemist { get; set; }
        /// <summary>Armorer subclass partition.</summary>
        public string Armorer { get; set; }
        /// <summary>Artillerist subclass partition.</summary>
        public string Artillerist { get; set; }
        /// <summary>Battle Smith subclass partition.</summary>
        public string BattleSmith { get; set; }
    }
}

