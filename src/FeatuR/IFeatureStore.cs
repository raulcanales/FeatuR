using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR
{
    /// <summary>
    /// Implements the source from which the service will resolve the features.
    /// </summary>
    public partial interface IFeatureStore
    {
        /// <summary>
        /// Gets a <see cref="Feature"/> by it's Id
        /// </summary>
        /// <param name="featureId">Id of the feature</param>
        Feature GetFeatureById(string featureId);

        /// <summary>
        /// Returns all the enabled features.
        /// </summary>
        IEnumerable<Feature> GetEnabledFeatures();

        /// <summary>
        /// Gets a <see cref="Feature"/> by it's Id
        /// </summary>
        /// <param name="featureId">Id of the feature</param>
        Task<Feature> GetFeatureByIdAsync(string featureId);

        /// <summary>
        /// Gets a <see cref="Feature"/> by it's Id
        /// </summary>
        /// <param name="featureId">Id of the feature</param>
        /// <param name="token">Cancellation token</param>
        Task<Feature> GetFeatureByIdAsync(string featureId, CancellationToken token);

        /// <summary>
        /// Returns all the enabled features.
        /// </summary>
        Task<IEnumerable<Feature>> GetEnabledFeaturesAsync();

        /// <summary>
        /// Returns all the enabled features.
        /// </summary>
        Task<IEnumerable<Feature>> GetEnabledFeaturesAsync(CancellationToken token);
    }
}
