using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.EntityFramework.MySQL
{
    /// <inheritdoc />
    public partial interface IFeatureStore
    {
        /// <summary>
        /// Gets a <see cref="Feature"/> by it's Id
        /// </summary>
        /// <param name="id">Id of the feature</param>
        Task<Feature> GetFeatureByIdAsync(string id, CancellationToken token = default);
    }
}
