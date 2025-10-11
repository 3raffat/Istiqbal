using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Guests;
using Istiqbal.Domain.Guests.Reservations;
using Istiqbal.Domain.Guests.Reservations.Feedbacks;
using Istiqbal.Domain.Guests.Reservations.Payments;
using Istiqbal.Domain.Rooms;
using Istiqbal.Domain.Rooms.Amenities;
using Istiqbal.Domain.Rooms.RoomTypes;
using Istiqbal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Istiqbal.Infrastructure.Persistence
{
    public sealed class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
      
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Amenity> Amenities => Set<Amenity>();
    }
}
