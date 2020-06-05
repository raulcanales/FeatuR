using FeatuR.EntityFramework.MySQL.IntegrationTests.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FeatuR.EntityFramework.MySQL.IntegrationTests.Fixtures
{
    public class MySQLFixture : IDisposable
    {
        public FeatuRDbContext DbContext { get; }
        public IFeatureService Sut { get; }

        public MySQLFixture()
        {
            DbContext = CreateDbContext();
            PopulateDbContext();
            var store = new EntityFrameworkFeatureStore(DbContext);
            Sut = new FeatureService(store);
        }

        public void Dispose()
        {
            DbContext.Database.ExecuteSqlRaw("DELETE FROM Features;");
            DbContext.Database.ExecuteSqlRaw("DROP DATABASE featur;");
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
            const string EnvironmentVariable = "ConnectionStringMySQL";

            var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariable);
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException($"Couldn't find the environment variable '{EnvironmentVariable}'");

            var builder = new DbContextOptionsBuilder<FeatuRDbContext>();
            builder.UseMySQL(connectionString, x => x.MigrationsAssembly("FeatuR.EntityFramework.MySQL"));
            var context = new FeatuRDbContext(builder.Options);
            context.Database.Migrate(); // Generate the database
            return context;
        }
    }
}
