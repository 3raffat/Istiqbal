using Istiqbal.Domain.Guests;
using Istiqbal.Domain.Guests.Reservations;
using Istiqbal.Domain.Guests.Reservations.Feedbacks;
using Istiqbal.Domain.Guests.Reservations.Payments;
using Istiqbal.Domain.Identity;
using Istiqbal.Domain.Rooms;
using Istiqbal.Domain.Rooms.Amenities;
using Istiqbal.Domain.Rooms.RoomTypes;
using Microsoft.EntityFrameworkCore;


namespace Istiqbal.Application.Common.Interface
{
    public interface IAppDbContext
    {
        public DbSet<Room> Rooms { get; }
        public DbSet<RoomType> RoomTypes { get; }
        public DbSet<Feedback> Feedbacks { get; }
        public DbSet<Guest> Guests { get; }
        public DbSet<Reservation> Reservations { get; }
        public DbSet<Payment> Payments { get; }
        public DbSet<Amenity> Amenities { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
