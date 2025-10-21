using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Guestes;
using Istiqbal.Domain.Guestes.Reservations;
using Istiqbal.Domain.Guestes.Reservations.Payments;
using Istiqbal.Domain.RoomTypes;
using Istiqbal.Domain.RoomTypes.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Istiqbal.Application.Common.Interface
{
    public interface IAppDbContext
    {
        public DbSet<Room> Rooms { get; }
        public DbSet<RoomType> RoomTypes { get; }
        public DbSet<Guest> Guests { get; }
        public DbSet<Reservation> Reservations { get; }
        public DbSet<Payment> Payments { get; }
        public DbSet<Amenity> Amenities { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    }
}
