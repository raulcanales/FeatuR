using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FeatuR.EntityFramework.Types
{
    internal class FeatureTypeConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name).IsRequired().HasMaxLength(140);
            builder.Property(f => f.Description).HasMaxLength(512);
            builder.Property(f => f.ActivationStrategies).IsRequired();
        }
    }
}
