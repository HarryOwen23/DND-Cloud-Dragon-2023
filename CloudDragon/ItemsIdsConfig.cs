using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.ItemIds;

namespace CloudDragon.ItemIds
{
    /// <summary>
    /// Root configuration object containing item id lookups for many game areas.
    /// Each property maps to a section in <c>appsettings.json</c> describing
    /// identifiers for that category.
    /// </summary>
    public class ItemIdsConfig
    {
        /// <summary>Armor item identifiers.</summary>
        public ArmorItemIdsConfig Armor { get; set; }
        /// <summary>Artificer class item identifiers.</summary>
        public ArtificerItemIdsConfig ArtificerClass { get; set; }
        /// <summary>Background related item identifiers.</summary>
        public BackgroundsItemIdsConfig Backgrounds { get; set; }
        /// <summary>Barbarian class item identifiers.</summary>
        public BarbarianItemIdsConfig BarbarianClass { get; set; }
        /// <summary>Bard class item identifiers.</summary>
        public BardItemIdsConfig BardClass { get; set; }
        /// <summary>Cleric class item identifiers.</summary>
        public ClericItemIdsConfig ClericClass { get; set; }
        /// <summary>Druid class item identifiers.</summary>
        public DruidItemIdsConfig DruidClass { get; set; }
        /// <summary>Fighter class item identifiers.</summary>
        public FighterItemIdsConfig FighterClass { get; set; }
        /// <summary>Monk class item identifiers.</summary>
        public MonkItemIdsConfig MonkClass { get; set; }
        /// <summary>Paladin class item identifiers.</summary>
        public PaladinItemIdsConfig PaladinClass { get; set; }
        /// <summary>Ranger class item identifiers.</summary>
        public RangerItemIdsConfig RangerClass { get; set; }
        /// <summary>Rogue class item identifiers.</summary>
        public RogueItemIdsConfig RogueClass { get; set; }
        /// <summary>Sorcerer class item identifiers.</summary>
        public SorcererItemIdsConfig SorcererClass { get; set; }
        /// <summary>Warlock class item identifiers.</summary>
        public WarlockItemIdsConfig WarlockClass { get; set; }
        /// <summary>Weapon item identifiers.</summary>
        public WeaponsItemIdsConfig WeaponsClass { get; set; }
        /// <summary>Wizard class item identifiers.</summary>
        public WizardItemIdsConfig WizardClass { get; set; }

    }

}
