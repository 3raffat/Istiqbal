

using Istiqbal.Domain.RoomTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istiqbal.Infrastructure.Data.Configuration
{
    public sealed class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(rt => rt.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(rt => rt.PricePerNight)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(rt => rt.MaxOccupancy)
                .IsRequired();

            builder.Navigation(rt => rt.Rooms)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
