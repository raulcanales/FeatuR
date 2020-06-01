using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.EntityFramework
{
    /// <inheritdoc />
    public partial interface IFeatureStore
    {
        /// <summary>
        /// Gets a <see cref="Feature"/> by it's Id
        /// </summary>
        /// <param name="id">Id of the feature</param>
        Task<Feature> GetFeatureByIdAsync(string id, CancellationToken token = default);

        /// <summary>
        /// Returns all the enabled features.
        /// </summary>
        Task<IEnumerable<Feature>> GetEnabledFeaturesAsync(CancellationToken token = default);
    }
}
