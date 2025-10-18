using Istiqbal.Domain.Guestes.Reservations.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Infrastructure.Data.Configuration
{
    public sealed class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Rating)
                .IsRequired();

            builder.Property(f => f.Comments)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(f => f.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(f => f.Reservation)
                .WithMany(r => r.Feedbacks)
                .HasForeignKey(f => f.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
