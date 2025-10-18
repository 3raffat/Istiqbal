using Istiqbal.Domain.Amenities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Infrastructure.Data.Configuration
{
    public sealed class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(r => r.Types)
                   .WithMany(r => r.Amenities);

            builder.Navigation(r => r.Types)
           .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
