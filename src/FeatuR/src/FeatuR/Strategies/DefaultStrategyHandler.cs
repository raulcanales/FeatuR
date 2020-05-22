using System.Collections.Generic;

namespace FeatuR.Strategies
{
    /// <summary>
    /// Default strategy handler. Always returns true.
    /// </summary>
    public class DefaultStrategyHandler : IStrategyHandler
    {
        /// <summary>
        /// Default strategy. Always returns true.
        /// </summary>
        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context = null) => true;
    }
}