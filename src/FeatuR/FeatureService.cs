using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR
{
    /// <inheritdoc />
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureStore _featureStore;
        private static readonly ConcurrentDictionary<string, object> _strategyHandlers = new ConcurrentDictionary<string, object>();

        /// <inheritdoc />
        public FeatureService(IFeatureStore featureStore)
        {
            _featureStore = featureStore;
        }

        public bool IsFeatureEnabled(string featureId) => IsFeatureEnabledCore(featureId, null);
        public Task<bool> IsFeatureEnabledAsync(string featureId) => IsFeatureEnabledAsyncCore(featureId, null, CancellationToken.None);
        public Task<bool> IsFeatureEnabledAsync(string featureId, CancellationToken token) => IsFeatureEnabledAsyncCore(featureId, null, token);
        public bool IsFeatureEnabled(string featureId, IFeatureContext context) => IsFeatureEnabledCore(featureId, context);
        public Task<bool> IsFeatureEnabledAsync(string featureId, IFeatureContext context) => IsFeatureEnabledAsyncCore(featureId, context, CancellationToken.None);
        public Task<bool> IsFeatureEnabledAsync(string featureId, IFeatureContext context, CancellationToken token) => IsFeatureEnabledAsyncCore(featureId, context, token);

        public IEnumerable<string> GetEnabledFeatures(IFeatureContext context)
        {
            var featureIds = new List<string>();
            var features = _featureStore.GetEnabledFeatures();
            foreach (var feature in features)
            {
                if (IsFeatureEnabledCore(feature, context))
                    featureIds.Add(feature.Id);
            }
            return featureIds;
        }
        public Task<IEnumerable<string>> GetEnabledFeaturesAsync(IFeatureContext context) => GetEnabledFeaturesAsync(context, CancellationToken.None);

        public async Task<IEnumerable<string>> GetEnabledFeaturesAsync(IFeatureContext context, CancellationToken token)
        {
            var featureIds = new List<string>();
            var features = await _featureStore.GetEnabledFeaturesAsync(token);
            bool isEnanled;
            foreach (var feature in features)
            {
                isEnanled = await IsFeatureEnabledAsync(feature.Id, context, token).ConfigureAwait(false);
                if (isEnanled)
                    featureIds.Add(feature.Id);
            }
            return featureIds;
        }


        public IDictionary<string, bool> EvaluateFeatures(IEnumerable<string> featureIds, IFeatureContext context)
        {
            var result = new Dictionary<string, bool>();
            foreach (var featureId in featureIds)
            {
                if (string.IsNullOrWhiteSpace(featureId) || result.ContainsKey(featureId))
                    continue;

                result.Add(featureId, IsFeatureEnabled(featureId, context));
            }
            return result;
        }


        public Task<IDictionary<string, bool>> EvaluateFeaturesAsync(IEnumerable<string> featureIds, IFeatureContext context) => EvaluateFeaturesAsync(featureIds, context, CancellationToken.None);
        public async Task<IDictionary<string, bool>> EvaluateFeaturesAsync(IEnumerable<string> featureIds, IFeatureContext context, CancellationToken token)
        {
            var result = new Dictionary<string, bool>();
            bool isEnabled;
            foreach (var featureId in featureIds)
            {
                if (string.IsNullOrWhiteSpace(featureId) || result.ContainsKey(featureId))
                    continue;

                isEnabled = await IsFeatureEnabledAsync(featureId, context, token).ConfigureAwait(false);
                result.Add(featureId, isEnabled);
            }
            return result;
        }

        /// <summary>
        /// Can be overridden in a derived class to control how this is implemented. By default, will retrieve a feature and run a foreach with all the activation strategies
        /// </summary>
        /// <param name="featureId">Required id of the feature</param>
        /// <param name="context">Optional context</param>
        /// <returns>Boolean indicating if the feature is considered enabled</returns>
        protected virtual bool IsFeatureEnabledCore(string featureId, IFeatureContext context)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(featureId);

            var feature = _featureStore.GetFeatureById(featureId);
            return IsFeatureEnabledCore(feature, context);
        }

        /// <summary>
        /// Can be overridden in a derived class to control how this is implemented. By default, will retrieve a feature and run a foreach with all the activation strategies
        /// </summary>
        /// <param name="feature">The feature to evaluate</param>
        /// <param name="context">Optional context</param>
        /// <returns>Boolean indicating if the feature is considered enabled</returns>
        protected virtual bool IsFeatureEnabledCore(Feature feature, IFeatureContext context)
        {
            if (feature == null || !feature.Enabled)
                return false;

            if (feature.ActivationStrategies == null || !feature.ActivationStrategies.Any(kv => !string.IsNullOrWhiteSpace(kv.Key)))
                return false;

            IStrategyHandler handler = null;
            foreach (var activationStrategy in feature.ActivationStrategies.Keys)
            {
                handler = GetStrategyHandler(activationStrategy);
                if (handler == null || !handler.IsEnabled(feature.ActivationStrategies[activationStrategy], context))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Can be overridden in a derived class to control how this is implemented. By default, will retrieve a feature and run a foreach with all the activation strategies
        /// </summary>
        /// <param name="featureId">Required id of the feature</param>
        /// <param name="context">Optional context</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Boolean indicating if the feature is considered enabled</returns>  
        protected virtual async Task<bool> IsFeatureEnabledAsyncCore(string featureId, IFeatureContext context, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(featureId);

            var feature = await _featureStore.GetFeatureByIdAsync(featureId, token).ConfigureAwait(false);
            return await IsFeatureEnabledAsyncCore(feature, context, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Can be overridden in a derived class to control how this is implemented. By default, will retrieve a feature and run a foreach with all the activation strategies
        /// </summary>
        /// <param name="feature">The feature to evaluate</param>
        /// <param name="context">Optional context</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Boolean indicating if the feature is considered enabled</returns>        
        protected virtual async Task<bool> IsFeatureEnabledAsyncCore(Feature feature, IFeatureContext context, CancellationToken token)
        {
            if (feature == null || !feature.Enabled)
                return false;

            if (feature.ActivationStrategies == null || !feature.ActivationStrategies.Any(kv => !string.IsNullOrWhiteSpace(kv.Key)))
                return false;

            IStrategyHandler handler = null;
            foreach (var activationStrategy in feature.ActivationStrategies.Keys)
            {
                handler = GetStrategyHandler(activationStrategy);
                if (handler == null)
                    return false;

                var isEnabled = await handler.IsEnabledAsync(feature.ActivationStrategies[activationStrategy], context, token).ConfigureAwait(false);
                if (!isEnabled)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Can be overridden in a derived class to control how to resolve types of <see cref="IStrategyHandler"/>
        /// </summary>
        protected virtual IStrategyHandler GetStrategyHandler(string activationStrategy)
        {
            var strategyName = activationStrategy.ToLower(CultureInfo.InvariantCulture);
            if (_strategyHandlers.ContainsKey(strategyName))
                return _strategyHandlers[activationStrategy] as IStrategyHandler;
            else
            {
                var type = ResolveStrategyHandlerType(strategyName);
                if (type != null)
                    return _strategyHandlers.GetOrAdd(strategyName, t => Activator.CreateInstance(type)) as IStrategyHandler;
            }
            return null;
        }

        /// <summary>
        /// Can be overridden in a derived class to control how to resolve implementations of <see cref="IStrategyHandler"/>
        /// </summary>
        protected virtual Type ResolveStrategyHandlerType(string strategyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (TypeInfo typeInfo in assembly.DefinedTypes)
                {
                    if (typeInfo.ImplementedInterfaces.Contains(typeof(IStrategyHandler)) && typeInfo.Name.ToLower(CultureInfo.InvariantCulture).Equals($"{strategyName}strategyhandler"))
                        return typeInfo.AsType();
                }
            }
            return null;
        }
    }
}
