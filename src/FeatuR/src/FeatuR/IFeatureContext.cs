using System.Collections.Generic;

namespace FeatuR
{
    /// <summary>
    /// Context where we can set extra information (for example user id, ip, client name, etc).
    /// </summary>
    public interface IFeatureContext
    {
        /// <summary>
        /// List of parameters that will be check when evaluating if a feature is enabled or not.
        /// </summary>
        Dictionary<string, string> Parameters { get; set; }
    }
}
