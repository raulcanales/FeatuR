using System;
using System.Collections.Generic;

namespace FeatuR.Sql
{
    public class SqlFeatureStore : IFeatureStore
    {
        public IEnumerable<Feature> GetEnabledFeatures()
        {
            throw new NotImplementedException();
        }

        public Feature GetFeatureById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
