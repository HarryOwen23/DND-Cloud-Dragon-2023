using System;


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

		public Task<List<Races>> Populate(string fileName)
		{

		}
	}
}