using Istiqbal.Domain.Guests;
using Istiqbal.Domain.Guests.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Infrastructure.Data.Configuration
{
    public sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public string FullName { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        private readonly List<Reservation> _reservation = new();
        public IReadOnlyCollection<Reservation> Reservation => _reservation.AsReadOnly();
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.FullName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(g => g.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(g => g.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(g => g.Email)
                .IsUnique();

            builder.HasMany(g => g.Reservation)
                .WithOne(r => r.Guest)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
