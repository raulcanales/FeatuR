using System.Collections.Generic;

namespace FeatuR.Strategies
{
    /// <summary>
    /// Default strategy handler. Always returns true.
    /// </summary>
    public class DefaultStrategyHandler : IStrategyHandler
    {
        /// <inheritdoc />
        public bool IsEnabled(Dictionary<string, string> parameters)
            => IsEnabled(parameters, null);
        /// <inheritdoc />
        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context)
            => true;
    }
}