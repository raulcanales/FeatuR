using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace FeatuR.EntityFramework.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        public FeatuRDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeatuRDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            return new FeatuRDbContext(optionsBuilder.Options);
        }
    }
}
