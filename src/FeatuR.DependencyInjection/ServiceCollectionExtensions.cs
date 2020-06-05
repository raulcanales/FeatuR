using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FeatuR.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public const string SectionName = "FeatuR";

        public static IServiceCollection AddBlogR(this IServiceCollection services)
            => AddBlogR(services, SectionName);
        public static IServiceCollection AddBlogR(this IServiceCollection services, string sectionName)
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
                    .AddScoped<IFeatureService, FeatureService>();

            return services;
        }
    }
}
