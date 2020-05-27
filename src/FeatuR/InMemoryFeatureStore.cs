using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace FeatuR
{
    /// <summary>
    /// In memory store for <see cref="Feature"/>
    /// </summary>
    public class InMemoryFeatureStore : IFeatureStore
    {
        /// <summary>
        /// In memory dictionary of features.
        /// </summary>
        public static ConcurrentDictionary<string, Feature> Features { get; set; } = new ConcurrentDictionary<string, Feature>();

        /// <inheritdoc />
        public InMemoryFeatureStore() { }
        /// <inheritdoc />
        public InMemoryFeatureStore(IEnumerable<Feature> features)
        {
            foreach (var feature in features)
                Features.TryAdd(feature.Id, feature);
        }

        /// <inheritdoc />
        public Feature GetFeatureById(string featureId)
        {
            Features.TryGetValue(featureId, out var feature);
            return feature;
        }

        /// <inheritdoc />
        public IEnumerable<Feature> GetEnabledFeatures() => Features.Values.Where(f => f.Enabled);
    }
}
