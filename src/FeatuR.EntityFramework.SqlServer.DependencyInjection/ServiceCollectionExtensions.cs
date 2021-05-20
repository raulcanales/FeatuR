using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FeatuR.EntityFramework.SqlServer.DependencyInjection
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
            settings.ValidateSettings();

            services.AddDbContext<FeatuRDbContext>(options =>
            {
                options.UseSqlServer(settings.GetConnectionString(), b => b.MigrationsAssembly("FeatuR.EntityFramework.SqlServer"));
            });

            services.AddScoped<IFeatureContext, FeatureContext>()
                    .AddScoped<IFeatureStore, EntityFrameworkFeatureStore>()
                    .AddScoped<IFeatureService, FeatureService>()
                    .AddSingleton<StrategyHandlerStore>();

            StrategyHandlerStore.InitializeHandlers(assembly);

            return services;
        }
    }
}
