using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.ItemIds
{
    /// <summary>
    /// Identifier references for different armor categories stored in Cosmos DB.
    /// </summary>
    public class ArmorItemIdsConfig
    {
        /// <summary>Id used for heavy armor items.</summary>
        public string HeavyArmor { get; set; }
        /// <summary>Id used for medium armor items.</summary>
        public string MediumArmor { get; set; }
        /// <summary>Id used for light armor items.</summary>
        public string LightArmor { get; set; }
    }
}
