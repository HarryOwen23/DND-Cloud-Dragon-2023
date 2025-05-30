using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDragonLib
{
    public interface IFeatsPopulator
    {
        Task<List<Feats>> Populate(string fileName);
    }
    public class FeatsPopulator : IFeatsPopulator
    {
        public FeatsPopulator()
        {
        }

        public async Task<List<Feats>> Populate(string fileName)
        {
            var result = new List<Feats>();
            return result;
        }
    }
}
