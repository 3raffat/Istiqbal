using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common;
using Istiqbal.Domain.Guestes;
using Istiqbal.Domain.Guestes.Reservations;
using Istiqbal.Domain.Guestes.Reservations.Feedbacks;
using Istiqbal.Domain.Guestes.Reservations.Payments;
using Istiqbal.Domain.RoomTypes;
using Istiqbal.Domain.RoomTypes.Rooms;
using Istiqbal.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Istiqbal.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }
        public DbSet<Room> Rooms => Set<Room>();

        public DbSet<RoomType> RoomTypes => Set<RoomType>();

        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        public DbSet<Guest> Guests => Set<Guest>();

        public DbSet<Reservation> Reservations => Set<Reservation>();

        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<Amenity> Amenities => Set<Amenity>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    }
}
