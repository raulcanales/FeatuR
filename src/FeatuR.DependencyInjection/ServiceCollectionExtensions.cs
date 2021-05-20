using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FeatuR.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public const string SectionName = "FeatuR";

        public static IServiceCollection AddFeatuR(this IServiceCollection services)
            => AddFeatuR(services, SectionName, Assembly.GetExecutingAssembly());

        public static IServiceCollection AddFeatuR(this IServiceCollection services, Assembly assembly)
            => AddFeatuR(services, SectionName, assembly);

        public static IServiceCollection AddFeatuR(this IServiceCollection services, string sectionName)
            => AddFeatuR(services, sectionName, Assembly.GetExecutingAssembly());

        public static IServiceCollection AddFeatuR(this IServiceCollection services, string sectionName, Assembly assembly)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
                configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var settings = new FeatuRSettings();
            configuration.GetSection(sectionName).Bind(settings);

            foreach (var feature in settings.Features)
            {
                if (string.IsNullOrWhiteSpace(feature.Id))
                    continue;

                InMemoryFeatureStore.Features.TryAdd(feature.Id, feature);
            }

            services.AddScoped<IFeatureContext, FeatureContext>()
                    .AddScoped<IFeatureStore, InMemoryFeatureStore>()
                    .AddScoped<IFeatureService, FeatureService>()
                    .AddSingleton<StrategyHandlerStore>();

            StrategyHandlerStore.InitializeHandlers(assembly);

            return services;
        }
    }
}
