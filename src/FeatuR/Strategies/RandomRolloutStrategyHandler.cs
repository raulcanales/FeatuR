using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.Strategies
{
    /// <summary>
    /// Enables a given feature for random users based on specified percentage.
    /// </summary>
    public class RandomRolloutStrategyHandler : IStrategyHandler
    {
        public bool IsEnabled(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, IFeatureContext context, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}