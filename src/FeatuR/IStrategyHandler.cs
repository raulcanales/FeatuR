using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="token">Cancellation token</param>
        Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, CancellationToken token);

        /// <summary>
        /// Evaluates if the feature should be enabled, based on the handler's logic rules.
        /// </summary>
        /// <param name="parameters">Parameters from the feature, for this specific strategy</param>
        /// <param name="context">Context containing different properties</param>        
        bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context);

        /// <summary>
        /// Evaluates if the feature should be enabled, based on the handler's logic rules.
        /// </summary>
        /// <param name="parameters">Parameters from the feature, for this specific strategy</param>
        /// <param name="context">Context containing different properties</param>
        /// <param name="token">Cancellation token</param>
        Task<bool> IsEnabledAsync(Dictionary<string, string> parameters, IFeatureContext context, CancellationToken token);
    }
}