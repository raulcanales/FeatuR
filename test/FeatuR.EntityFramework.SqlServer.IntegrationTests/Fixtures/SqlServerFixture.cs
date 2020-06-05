using FeatuR.EntityFramework.SqlServer.IntegrationTests.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FeatuR.EntityFramework.SqlServer.IntegrationTests.Fixtures
{
    public class SqlServerFixture : IDisposable
    {
        public FeatuRDbContext DbContext { get; }
        public IFeatureService Sut { get; }

        public SqlServerFixture()
        {
            DbContext = CreateDbContext();
            PopulateDbContext();
            var store = new EntityFrameworkFeatureStore(DbContext);
            Sut = new FeatureService(store);
        }

        public void Dispose()
        {
            DbContext.Database.ExecuteSqlRaw("DELETE FROM Features;");
            DbContext.Dispose();
        }

        private void PopulateDbContext()
        {
            DbContext.Features.AddRange(new[]
            {
                Features.DefaultFeature,
                Features.DisabledFeature,
                Features.ExcludeUserPepe,
                Features.OnlyForUser444
            });

            DbContext.SaveChanges();
        }

        private FeatuRDbContext CreateDbContext()
        {
            const string EnvironmentVariable = "ConnectionStringSqlServer";

            var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariable);
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException($"Couldn't find the environment variable '{EnvironmentVariable}'");

            var builder = new DbContextOptionsBuilder<FeatuRDbContext>();
            builder.UseSqlServer(connectionString, x => x.MigrationsAssembly("FeatuR.EntityFramework.SqlServer"));
            var context = new FeatuRDbContext(builder.Options);
            context.Database.Migrate(); // Generate the database
            return context;
        }
    }
}
