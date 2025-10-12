using Istiqbal.Domain.Common;
using Istiqbal.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Infrastructure.Data.Configuration
{

    public sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r =>r.Id);

           builder.Property(r => r.Number)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(seed: 100, increment: 1);

            builder.HasOne(r => r.Type)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Amenities)
                .WithMany(r => r.Rooms);

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(r => r.Floor)
                .IsRequired();

            builder.HasMany(r => r.Reservation)
                .WithOne(res => res.Room)
                .HasForeignKey(res => res.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
