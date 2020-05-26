using System.Collections;
using System.Collections.Generic;

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
        bool IsFeatureEnabled(string featureId, IFeatureContext context);

        /// <summary>
        /// Returns a list of all the enabled features for a given context
        /// </summary>
        /// <param name="context">Context containing extra properties for a given scope</param>
        /// <returns>List with al the id's of the features that are enabled for the given <see cref="IFeatureContext"/></returns>
        IEnumerable<string> GetEnabledFeatures(IFeatureContext context);
    }
}
