using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FeatuR.EntityFramework.MySQL
{
    public class FeatuRDbContextFactory : IDesignTimeDbContextFactory<FeatuRDbContext>
    {
        public FeatuRDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FeatuRDbContext>();
            var connectionString = "Server=localhost,5434;Uid=sa;Password=Pass@word;Database=featur;";
            builder.UseMySQL(connectionString);
            return new FeatuRDbContext(builder.Options);
        }
    }
}
