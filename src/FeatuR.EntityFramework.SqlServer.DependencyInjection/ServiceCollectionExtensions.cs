using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeatuR.EntityFramework.SqlServer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogR(this IServiceCollection services, string sectionName = "FeatuR")
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
                configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var settings = new FeatuRSettings();
            configuration.GetSection(sectionName).Bind(settings);
            settings.ValidateSettings();

            services.AddDbContext<FeatuRDbContext>(options =>
            {
                options.UseSqlServer(settings.GetConnectionString());
            });

            services.AddScoped<IFeatureContext, FeatureContext>()
                    .AddScoped<IFeatureStore, SqlServerFeatureStore>()
                    .AddScoped<IFeatureService, FeatureService>();

            return services;
        }
    }
}
