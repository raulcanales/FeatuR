using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FeatuR.EntityFramework.SqlServer
{
    public class FeatuRDbContextFactory : IDesignTimeDbContextFactory<FeatuRDbContext>
    {
        public FeatuRDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FeatuRDbContext>();
            var connectionString = "Server=localhost,5434;Uid=sa;Password=Pass@word;Database=featur;";
            builder.UseSqlServer(connectionString);
            return new FeatuRDbContext(builder.Options);
        }
    }
}
