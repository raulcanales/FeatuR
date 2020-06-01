using Microsoft.EntityFrameworkCore;
using System;

namespace FeatuR.EntityFramework.Tests
{
    public class EntityFrameworkFixture : IDisposable
    {
        public FeatuRDbContext DbContext { get; }
        public EntityFrameworkFeatureStore Sut { get; }

        public EntityFrameworkFixture()
        {
            DbContext = CreateDbContext();
            PopulateDbContext();
            Sut = new EntityFrameworkFeatureStore(DbContext);
        }

        private void PopulateDbContext()
        {
            DbContext.Features.AddRange(
                Features.DefaultFeature,
                Features.DisabledFeature,
                Features.Feature1);
            DbContext.SaveChanges();

        }

        private FeatuRDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeatuRDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            return new FeatuRDbContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
