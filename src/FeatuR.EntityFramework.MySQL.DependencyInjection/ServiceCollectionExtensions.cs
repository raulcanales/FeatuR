using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FeatuR.EntityFramework.MySQL.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public const string SectionName = "FeatuR";

        public static IServiceCollection AddFeatuR(this IServiceCollection services)
            => AddFeatuR(services, SectionName);
        public static IServiceCollection AddFeatuR(this IServiceCollection services, string sectionName)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
                configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var settings = new FeatuRSettings();
            configuration.GetSection(sectionName).Bind(settings);
            settings.ValidateSettings();

            services.AddDbContext<FeatuRDbContext>(options =>
            {
                options.UseMySQL(settings.GetConnectionString());
            });

            services.AddScoped<IFeatureContext, FeatureContext>()
                    .AddScoped<IFeatureStore, EntityFrameworkFeatureStore>()
                    .AddScoped<IFeatureService, FeatureService>();

            return services;
        }
    }
}
