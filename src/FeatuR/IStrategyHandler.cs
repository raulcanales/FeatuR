using System.Collections.Generic;

namespace FeatuR
{
    /// <summary>
    /// Interface used to implement custom activation strategies.
    /// </summary>
    public interface IStrategyHandler
    {
        /// <summary>
        /// Evaluates if the feature should be enabled, based on the handler's logic rules.
        /// </summary>
        /// <param name="parameters">Parameters from the feature, for this specific strategy</param>
        bool IsEnabled(Dictionary<string, string> parameters);

        /// <summary>
        /// Evaluates if the feature should be enabled, based on the handler's logic rules.
        /// </summary>
        /// <param name="parameters">Parameters from the feature, for this specific strategy</param>
        /// <param name="context">Context containing different properties</param>        
        bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context);
    }
}