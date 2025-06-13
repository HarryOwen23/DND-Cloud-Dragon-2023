using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.PartitionKeys;

namespace CloudDragon
{
    /// <summary>
    /// Defines Cosmos DB partition keys for each character class and equipment category.
    /// </summary>
    public class PartitionKeysConfig
    {
        /// <summary>Partition keys for the Barbarian class.</summary>
        public BarbarianPartitionKeysConfig BarbarianClass { get; set; }
        /// <summary>Partition keys for the Bard class.</summary>
        public BardPartitionKeysConfig BardClass { get; set; }
        /// <summary>Partition keys for the Cleric class.</summary>
        public ClericPartitionKeysConfig ClericClass { get; set; }
        /// <summary>Partition keys for the Druid class.</summary>
        public DruidPartitionKeysConfig DruidClass { get; set; }
        /// <summary>Partition keys for the Fighter class.</summary>
        public FighterPartitionKeysConfig FighterClass { get; set; }
        /// <summary>Partition keys for the Monk class.</summary>
        public MonkPartitionKeysConfig MonkClass { get; set; }
        /// <summary>Partition keys for the Paladin class.</summary>
        public PaladinPartitionKeysConfig PaladinClass { get; set; }
        /// <summary>Partition keys for the Ranger class.</summary>
        public RangerPartitionKeysConfig RangerClass { get; set; }
        /// <summary>Partition keys for the Rogue class.</summary>
        public RoguePartitionKeysConfig RogueClass { get; set; }
        /// <summary>Partition keys for the Sorcerer class.</summary>
        public SorcererPartitionKeysConfig SorcererClass { get; set; }
        /// <summary>Partition keys for the Warlock class.</summary>
        public WarlockPartitionKeysConfig WarlockClass { get; set; }
        /// <summary>Partition keys for the Wizard class.</summary>
        public WizardPartitionKeysConfig WizardClass { get; set; }
        /// <summary>Partition keys for weapons data.</summary>
        public WeaponsPartitionKeysConfig Weapons { get; set; }
    }
}
