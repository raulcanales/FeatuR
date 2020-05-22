using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace FeatuR
{
    /// <inheritdoc />
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureStore _featureStore;
        private static readonly ConcurrentDictionary<string, object> _strategyHandlers = new ConcurrentDictionary<string, object>();

        public FeatureService(IFeatureStore featureStore)
        {
            _featureStore = featureStore;
        }

        /// <inheritdoc />
        public bool IsFeatureEnabled(string featureId) => IsFeatureEnabledCore(featureId, null);
        /// <inheritdoc />
        public bool IsFeatureEnabled(string featureId, FeatureContext context) => IsFeatureEnabledCore(featureId, context);
        
        /// <summary>
        /// Can be overrided in a derived class to control how this is implemented. By default, will retrieve a feature and run a foreach with all the activation strategies
        /// </summary>
        /// <param name="featureId">Required id of the feature</param>
        /// <param name="context">Optional context</param>
        /// <returns>Boolean indicating if the feature is considered enabled</returns>
        protected virtual bool IsFeatureEnabledCore(string featureId, FeatureContext context)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(featureId);

            var feature = _featureStore.GetFeatureById(featureId);
            if (feature == null)
                return false;                

            if (feature.ActivationStrategies == null || !feature.ActivationStrategies.Any(kv => !string.IsNullOrWhiteSpace(kv.Key)))
                return false;

            IStrategyHandler handler = null;
            foreach (var activationStrategy in feature.ActivationStrategies.Keys)
            {
                var strategyName = activationStrategy.ToLower();

                if (_strategyHandlers.ContainsKey(strategyName))
                {
                    handler = _strategyHandlers[activationStrategy] as IStrategyHandler;
                }
                else
                {
                    var type = ResolveType(strategyName);
                    if (type != null)
                        handler = _strategyHandlers.GetOrAdd(strategyName, t => Activator.CreateInstance(type)) as IStrategyHandler;
                }

                if (handler == null || !handler.IsEnabled(feature.ActivationStrategies[activationStrategy], context))
                    return false;
            }

            return true;
        }

        protected virtual Type ResolveType(string strategyName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (TypeInfo typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.ImplementedInterfaces.Contains(typeof(IStrategyHandler)) && typeInfo.Name.ToLower().Equals($"{strategyName}strategyhandler"))
                    return typeInfo.AsType();
            }

            return null;
        }
    }
}
