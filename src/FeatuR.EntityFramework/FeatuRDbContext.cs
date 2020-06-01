using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FeatuR.EntityFramework
{
    public class FeatuRDbContext : DbContext
    {
        public DbSet<Feature> Features { get; set; }

        public FeatuRDbContext(DbContextOptions<FeatuRDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
