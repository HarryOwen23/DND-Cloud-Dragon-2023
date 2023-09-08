using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.Lib;


namespace CloudDragon.Lib
{

	public interface IRacesPopulator
	{
		Task<List<Races>> Populate(string fileName);
	}

	public class RacesPopulator : IRacesPopulator
	{
        public RacesPopulator()
		{
		}

		public async Task<List<Races>> Populate(string fileName)
		{
            var result = new List<Races>();

            return result;
        }
	}
}