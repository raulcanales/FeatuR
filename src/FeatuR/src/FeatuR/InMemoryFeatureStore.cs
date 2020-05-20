using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FeatuR
{
    /// <summary>
    /// In memory store for <see cref="Feature"/>
    /// </summary>
    public class InMemoryFeatureStore : IFeatureStore
    {
        /// <summary>
        /// In memory list of features.
        /// </summary>
        public static ConcurrentDictionary<string, Feature> Features { get; set; } = new ConcurrentDictionary<string, Feature>();

        public InMemoryFeatureStore() { }
        public InMemoryFeatureStore(IEnumerable<Feature> features)
        {
            foreach (var feature in features)
                Features.TryAdd(feature.Id, feature);
        }

        /// <summary>
        /// Returns a <see cref="Feature"/> by its Id
        /// </summary>
        public Feature GetFeatureById(string featureId)
        {
            Features.TryGetValue(featureId, out var feature);
            return feature;
        }
    }
}
