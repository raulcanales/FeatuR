namespace FeatuR
{
    /// <summary>
    /// Service used to check if a feature is enabled or not
    /// </summary>
    public interface IFeatureService
    {
        /// <summary>
        /// Checks if a feature is enabled.
        /// </summary>
        /// <param name="featureId">The id of the feature to check</param>
        bool IsFeatureEnabled(string featureId);

        /// <summary>
        /// Checks if a feature is enabled.
        /// </summary>
        /// <param name="featureId">The id of the feature to check</param>
        /// <param name="context">Context containing extra properties for a given scope</param>
        bool IsFeatureEnabled(string featureId, FeatureContext context);
    }
}
