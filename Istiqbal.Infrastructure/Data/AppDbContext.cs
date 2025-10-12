using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Common;
using Istiqbal.Domain.Guests;
using Istiqbal.Domain.Guests.Reservations;
using Istiqbal.Domain.Guests.Reservations.Feedbacks;
using Istiqbal.Domain.Guests.Reservations.Payments;
using Istiqbal.Domain.Identity;
using Istiqbal.Domain.Rooms;
using Istiqbal.Domain.Rooms.Amenities;
using Istiqbal.Domain.Rooms.RoomTypes;
using Istiqbal.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
