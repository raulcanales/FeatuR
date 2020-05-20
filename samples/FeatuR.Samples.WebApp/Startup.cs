using FeatuR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Samples.featuR.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var store = new InMemoryFeatureStore(new[]
            {
                new Feature
                {
                    Id = "feature1",
                    ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
                    {
                        ["userid"]=new Dictionary<string, string>
                        {
                            ["allowed"]=",123456,9999,abc123,"
                        }
                    }
                },
                new Feature
                {
                    Id = "feature2",
                    ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
                    {
                        ["default"]=new Dictionary<string, string>()
                    }
                }});
            services.AddSingleton<IFeatureStore>(store);
            services.AddTransient<IFeatureService, FeatureService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
