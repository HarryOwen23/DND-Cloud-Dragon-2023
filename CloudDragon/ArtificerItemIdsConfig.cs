using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.ItemIds
{
    /// <summary>
    /// Item identifiers for various Artificer subclasses stored in Cosmos DB.
    /// </summary>
    public class ArtificerItemIdsConfig
    {
        /// <summary>Base Artificer class items.</summary>
        public string Base { get; set; }
        /// <summary>Alchemist specialization items.</summary>
        public string Alchemist { get; set; }
        /// <summary>Armorer specialization items.</summary>
        public string Armorer { get; set; }
        /// <summary>Artillerist specialization items.</summary>
        public string Artillerist { get; set; }
        /// <summary>Battle Smith specialization items.</summary>
        public string BattleSmith { get; set; }
    }
}
