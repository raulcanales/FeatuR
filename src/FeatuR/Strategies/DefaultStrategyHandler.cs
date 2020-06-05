using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.Strategies
{
    /// <summary>
    /// Default strategy handler. Always returns true.
    /// </summary>
    public class DefaultStrategyHandler : IStrategyHandler
    {
        public bool IsEnabled(Dictionary<string, string> parameters)
            => true;
        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context)
            => true;
        public Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, CancellationToken token)
            => Task.FromResult(true);
        public Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, IFeatureContext context, CancellationToken token)
            => Task.FromResult(true);
    }
}