using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.PartitionKeys
{
    /// <summary>
    /// Partition keys used for Fighter archetype data.
    /// </summary>
    public class FighterPartitionKeysConfig
    {
        /// <summary>Base Fighter partition.</summary>
        public string Base { get; set; }
        /// <summary>Champion archetype partition.</summary>
        public string Champion { get; set; }
        /// <summary>Battle Master archetype partition.</summary>
        public string BattleMaster { get; set; }
        public string ArcaneArcher { get; set; }
        public string Banneret { get; set; }
        public string Cavalier { get; set; }
        public string EchoKnight { get; set; }
        public string PsiWarrior { get; set; }
        public string RuneKnight { get; set; }
        public string Samurai { get; set; }
        public string EldritchKnight { get; set; }
    }
}
