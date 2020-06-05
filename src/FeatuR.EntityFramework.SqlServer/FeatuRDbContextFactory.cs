using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Reflection;

namespace FeatuR.EntityFramework.SqlServer
{
    public class FeatuRDbContextFactory : IDesignTimeDbContextFactory<FeatuRDbContext>
    {
        public FeatuRDbContext CreateDbContext(string[] args)
        {
            const string EnvironmentVariable = "ConnectionStringSqlServer";

            var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariable);
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException($"Missing environment variable '{EnvironmentVariable}'");

            var builder = new DbContextOptionsBuilder<FeatuRDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().ToString()));
            return new FeatuRDbContext(builder.Options);
        }
    }
}
