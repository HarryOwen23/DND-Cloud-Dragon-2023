using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.PartitionKeys
{
    /// <summary>
    /// Partition key names for weapon documents.
    /// </summary>
    public class WeaponsPartitionKeysConfig
    {
        /// <summary>Martial melee weapons partition.</summary>
        public string MartialMelee { get; set; }
        /// <summary>Martial ranged weapons partition.</summary>
        public string RangedMartial { get; set; }
        /// <summary>Simple melee weapons partition.</summary>
        public string SimpleMelee { get; set; }
        /// <summary>Simple ranged weapons partition.</summary>
        public string SimpleRanged { get; set; }
        /// <summary>Renaissance firearms partition.</summary>
        public string RenaissanceFirearms { get; set; }
        /// <summary>Modern firearms partition.</summary>
        public string ModernFirearms { get; set; }
        /// <summary>Futuristic firearms partition.</summary>
        public string FuturisticFirearms { get; set; }
        /// <summary>Explosives partition.</summary>
        public string Explosives { get; set; }
    }
}
