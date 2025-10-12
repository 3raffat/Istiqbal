using Istiqbal.Domain.Guests.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Istiqbal.Infrastructure.Data.Configuration
{
    public sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
           builder.HasKey(x => x.Id);   

              builder.Property(x => x.Amount)
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();

            builder.Property(x => x.NumberOfGuests)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.CheckInDate)
                .IsRequired();

            builder.Property(x => x.CheckOutDate)
                .IsRequired();

            builder.Property(x => x.MaxOccupancy)
                .IsRequired();


        }
    }
}
