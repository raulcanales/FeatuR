using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatuR.RestClient
{
    public static class Extensions
    {
        public static IServiceCollection AddRestFeatureService(this IServiceCollection services, string configSection = "FeatureService")
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
