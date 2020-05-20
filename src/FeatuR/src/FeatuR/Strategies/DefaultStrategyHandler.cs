using System.Collections.Generic;

namespace FeatuR.Strategies
{
    public class DefaultStrategyHandler : IStrategyHandler
    {
        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context = null) => true;
    }
}