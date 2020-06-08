using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Reflection;

namespace FeatuR.EntityFramework.MySQL
{
    public class FeatuRDbContextFactory : IDesignTimeDbContextFactory<FeatuRDbContext>
    {
        public FeatuRDbContext CreateDbContext(string[] args)
        {
            const string EnvironmentVariable = "ConnectionStringMySQL";

            var connectionString = Environment.GetEnvironmentVariable(EnvironmentVariable);
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException($"Missing environment variable '{EnvironmentVariable}'");

            var builder = new DbContextOptionsBuilder<FeatuRDbContext>();
            builder.UseMySql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().ToString()));
            return new FeatuRDbContext(builder.Options);
        }
    }
}
