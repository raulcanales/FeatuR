using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FeatuR
{
    public class StrategyHandlerStore
    {
        private static readonly ConcurrentDictionary<string, object> _strategyHandlers = new ConcurrentDictionary<string, object>();

        public static void InitializeHandlers(Assembly executingAssembly)
        {
            var assemblies = new List<Assembly> { executingAssembly };
            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var assembly in assemblies)
            {
                LoadStrategyHandlersFromAssembly(assembly);
            }
        }

        public IStrategyHandler GetStrategyHandler(string activationStrategy)
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

        private static void LoadStrategyHandlersFromAssembly(Assembly assembly)
        {
            foreach (TypeInfo typeInfo in assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IStrategyHandler))))
            {
                var strategyName = typeInfo.Name.ToLower(CultureInfo.InvariantCulture).Replace("strategyhandler", "");
                _strategyHandlers.TryAdd(strategyName, Activator.CreateInstance(typeInfo.AsType()));
            }
        }

        private Type ResolveStrategyHandlerType(string strategyName)
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
