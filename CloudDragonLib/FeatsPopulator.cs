﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDragon.Lib;

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

        public Task<List<Feats>> Populate(string fileName)
        {

        }
    }
}
