using System.Collections.Generic;

namespace FeatuR
{
    /// <summary>
    /// Class that holds information for a feature.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Id of the feature (should be unique).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// (Optional) Name of the feature.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// (Optional) Description of the feature.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if the feature is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Activation strategies applied to the feature.
        /// </summary>  
        public Dictionary<string, Dictionary<string, string>> ActivationStrategies { get; set; }
    }
}
