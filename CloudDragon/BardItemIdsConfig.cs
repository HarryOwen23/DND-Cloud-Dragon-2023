using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.ItemIds
{
    /// <summary>
    /// Maps Bard subclass names to item identifiers in Cosmos DB.
    /// </summary>
    public class BardItemIdsConfig
    {
        /// <summary>Base Bard class items.</summary>
        public string Base { get; set; }
        /// <summary>College of Lore items.</summary>
        public string CollegeOfLore { get; set; }
        /// <summary>College of Creation items.</summary>
        public string CollegeOfCreation { get; set; }
        /// <summary>College of Eloquence items.</summary>
        public string CollegeOfEloquence { get; set; }
        /// <summary>College of Glamour items.</summary>
        public string CollegeOfGlamour { get; set; }
        /// <summary>College of Spirits items.</summary>
        public string CollegeOfSpirits { get; set; }
        /// <summary>College of Swords items.</summary>
        public string CollegeOfSwords { get; set; }
        /// <summary>College of Valor items.</summary>
        public string CollegeOfValor { get; set; }
        /// <summary>College of Whispers items.</summary>
        public string CollegeOfWhispers { get; set; }
        /// <summary>Cantrip item identifiers.</summary>
        public string Cantrips { get; set; }
        /// <summary>1st-level spell item identifiers.</summary>
        public string Level1Spells { get; set; }
        /// <summary>2nd-level spell item identifiers.</summary>
        public string Level2Spells { get; set; }
        /// <summary>3rd-level spell item identifiers.</summary>
        public string Level3Spells { get; set; }
        /// <summary>4th-level spell item identifiers.</summary>
        public string Level4Spells { get; set; }
        /// <summary>5th-level spell item identifiers.</summary>
        public string Level5Spells { get; set; }
        /// <summary>6th-level spell item identifiers.</summary>
        public string Level6Spells { get; set; }
        /// <summary>7th-level spell item identifiers.</summary>
        public string Level7Spells { get; set; }
        /// <summary>8th-level spell item identifiers.</summary>
        public string Level8Spells { get; set; }
        /// <summary>9th-level spell item identifiers.</summary>
        public string Level9Spells { get; set; }
    }

}
