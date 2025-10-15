using Istiqbal.Domain.Guestes.Reservations.Payments;
using Istiqbal.Domain.Guests.Reservations.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Istiqbal.Infrastructure.Data.Configuration
{
    public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id );

            builder.Property(p => p.Status).
                IsRequired()
                .HasConversion<string>();

            builder.Property(p=>p.PaymentMethod)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(p => p.Reservation)
                 .WithOne(p => p.Payment)
                 .HasForeignKey<Payment>(p=>p.ReservationId);

        }
    }
}
