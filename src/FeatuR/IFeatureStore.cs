namespace FeatuR
{
    /// <summary>
    /// Implements the source from which the service will resolve the features.
    /// </summary>
    public interface IFeatureStore
    {
        /// <summary>
        /// Gets a <see cref="Feature"/> by it's Id
        /// </summary>
        /// <param name="id">Id of the feature</param>
        Feature GetFeatureById(string id);
    }
}
