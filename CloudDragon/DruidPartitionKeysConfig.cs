using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragon.PartitionKeys
{
    /// <summary>
    /// Defines Cosmos DB partition key names for Druid class data.
    /// </summary>
    public class DruidPartitionKeysConfig
    {
        /// <summary>Base Druid class partition key.</summary>
        public string Base { get; set; }
        /// <summary>Circle of the Land partition key.</summary>
        public string CircleOfTheLand { get; set; }
        /// <summary>Circle of the Moon partition key.</summary>
        public string CircleOfTheMoon { get; set; }
        /// <summary>Circle of Dreams partition key.</summary>
        public string CircleOfDreams { get; set; }
        /// <summary>Circle of the Shepherd partition key.</summary>
        public string CircleOfShepherd { get; set; }
        /// <summary>Circle of Spores partition key.</summary>
        public string CircleOfSpores { get; set; }
        /// <summary>Circle of Stars partition key.</summary>
        public string CircleOfStars { get; set; }
        /// <summary>Circle of Wildfire partition key.</summary>
        public string CircleOfWildfire { get; set; }
        /// <summary>Container for Druid cantrips.</summary>
        public string Cantrips { get; set; }
        /// <summary>Container for 1st level spells.</summary>
        public string Level1Spells { get; set; }
        /// <summary>Container for 2nd level spells.</summary>
        public string Level2Spells { get; set; }
        /// <summary>Container for 3rd level spells.</summary>
        public string Level3Spells { get; set; }
        /// <summary>Container for 4th level spells.</summary>
        public string Level4Spells { get; set; }
        /// <summary>Container for 5th level spells.</summary>
        public string Level5Spells { get; set; }
        /// <summary>Container for 6th level spells.</summary>
        public string Level6Spells { get; set; }
        /// <summary>Container for 7th level spells.</summary>
        public string Level7Spells { get; set; }
        /// <summary>Container for 8th level spells.</summary>
        public string Level8Spells { get; set; }
        /// <summary>Container for 9th level spells.</summary>
        public string Level9Spells { get; set; }
    }

}
