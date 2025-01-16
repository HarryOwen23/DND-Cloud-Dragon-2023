using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.PartitionKeys;

namespace CloudDragon
{
    public class PartitionKeysConfig
    {
        public BarbarianPartitionKeysConfig BarbarianClass { get; set; }
        public BardPartitionKeysConfig BardClass { get; set; }
        public ClericPartitionKeysConfig ClericClass { get; set; }
        public DruidPartitionKeysConfig DruidClass { get; set; }
        public FighterPartitionKeysConfig FighterClass { get; set; }
        public MonkPartitionKeysConfig MonkClass { get; set; }
        public PaladinPartitionKeysConfig PaladinClass { get; set; }
        public RangerPartitionKeysConfig RangerClass { get; set; }
        public RoguePartitionKeysConfig RogueClass { get; set; }
        public SorcererPartitionKeysConfig SorcererClass { get; set; }
        public WarlockPartitionKeysConfig WarlockClass { get; set; }
        public WizardPartitionKeysConfig WizardClass { get; set; }
        public WeaponsPartitionKeysConfig Weapons { get; set; }
    }
}
