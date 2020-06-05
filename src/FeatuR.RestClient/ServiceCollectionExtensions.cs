using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FeatuR.RestClient
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        private const string SectionName = "FeatuR";

        /// <summary>
        /// Registers all the necessary services to make <see cref="IFeatureService"/> work as a rest client, pointing to another service specified in the <see cref="RestFeatureServiceSettings"/>.
        /// </summary>
        public static IServiceCollection AddRestFeatureService(this IServiceCollection services)
            => AddFeatuR(services, SectionName);

        /// <summary>
        /// Registers all the necessary services to make <see cref="IFeatureService"/> work as a rest client, pointing to another service specified in the <see cref="RestFeatureServiceSettings"/>.
        /// </summary>
        public static IServiceCollection AddFeatuR(this IServiceCollection services, string configSection)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
                configuration = serviceProvider.GetService<IConfiguration>();

            var options = new RestFeatureServiceSettings();
            configuration.GetSection(configSection).Bind(options);
            options.ValidateSettings();
            services.AddSingleton(options);
            services.AddHttpClient<FeatureHttpClient>();
            services.AddTransient<IFeatureService, RestFeatureService>();
            services.AddScoped<IFeatureContext, FeatureContext>();
            return services;
        }
    }
}
