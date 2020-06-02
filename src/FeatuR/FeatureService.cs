using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

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

        /// <inheritdoc />
        public bool IsFeatureEnabled(string featureId) => IsFeatureEnabledCore(featureId, null);
        /// <inheritdoc />
        public bool IsFeatureEnabled(string featureId, IFeatureContext context) => IsFeatureEnabledCore(featureId, context);

        /// <inheritdoc />
        public IEnumerable<string> GetEnabledFeatures(IFeatureContext context)
        {
            var featureIds = new List<string>();
            foreach (var feature in _featureStore.GetEnabledFeatures())
            {
                if (IsFeatureEnabled(feature.Id, context))
                    featureIds.Add(feature.Id);
            }
            return featureIds;
        }

        /// <inheritdoc />
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

        /// <summary>
        /// Can be overrided in a derived class to control how this is implemented. By default, will retrieve a feature and run a foreach with all the activation strategies
        /// </summary>
        /// <param name="featureId">Required id of the feature</param>
        /// <param name="context">Optional context</param>
        /// <returns>Boolean indicating if the feature is considered enabled</returns>
        protected virtual bool IsFeatureEnabledCore(string featureId, IFeatureContext context)
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
                handler = GetStrategyHandler(activationStrategy);
                if (handler == null || !handler.IsEnabled(feature.ActivationStrategies[activationStrategy], context))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Can be overrided in a derived class to control how to resolve types of <see cref="IStrategyHandler"/>
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
        /// Can be overrided in a derived class to control how to resolve implementations of <see cref="IStrategyHandler"/>
        /// </summary>
        protected virtual Type ResolveStrategyHandlerType(string strategyName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (TypeInfo typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.ImplementedInterfaces.Contains(typeof(IStrategyHandler)) && typeInfo.Name.ToLower(CultureInfo.InvariantCulture).Equals($"{strategyName}strategyhandler"))
                    return typeInfo.AsType();
            }
            return null;
        }
    }
}
