using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.Strategies
{
    /// <summary>
    /// Enables a given feature for random users based on a simple/naive randomizer.
    /// </summary>
    public class RandomStrategyHandler : IStrategyHandler
    {
        public bool IsEnabled(Dictionary<string, string> parameters)
            => IsEnabled(parameters, null);

        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context)
            => new Random().Next(1, 100) % 2 == 0;

        public Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, CancellationToken token)
            => Task.FromResult(IsEnabled(parameters, null));

        public Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, IFeatureContext context, CancellationToken token)
            => Task.FromResult(IsEnabled(parameters, context));
    }
}