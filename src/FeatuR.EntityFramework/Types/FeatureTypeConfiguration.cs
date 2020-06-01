using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FeatuR.EntityFramework.Types
{
    internal class FeatureTypeConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name).IsRequired().HasMaxLength(140);
            builder.Property(f => f.Description).HasMaxLength(512);
            builder.Property(f => f.ActivationStrategies)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .IsRequired();
        }
    }
}
