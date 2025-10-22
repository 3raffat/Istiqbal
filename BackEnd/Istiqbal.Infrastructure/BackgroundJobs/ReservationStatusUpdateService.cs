using Istiqbal.Application.Common.Interface;
using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Infrastructure.BackgroundJobs
{
   
   public sealed class ReservationStatusUpdateService(IServiceScopeFactory scopeFactory,
    ILogger<ReservationStatusUpdateService> logger,
    IOptions<AppSettings> options,
    TimeProvider dateTime) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly ILogger<ReservationStatusUpdateService> _logger = logger;
    private readonly AppSettings _appSettings = options.Value;
    private readonly TimeProvider _time = dateTime;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(_appSettings.RoomStatusCheckFrequencyMinutes));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Running room status update at {Now}", _time.GetLocalNow());
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
                var now = _time.GetUtcNow();

                // 1️ Start Reservations → Occupied
                var startingReservation = await db.Reservations
                    .Include(r => r.Room)
                    .Where(r =>
                        r.Room.Status == RoomStatus.Available &&
                        r.CheckInDate <= DateOnly.FromDateTime(now.DateTime) &&
                        r.CheckOutDate > DateOnly.FromDateTime(now.DateTime))
                    .ToListAsync(stoppingToken);

                foreach (var res in startingReservation)
                    res.Room.SetStatus(RoomStatus.Occupied);

                // 2️ Finish Reservation → Cleaning
                var finishedReservation = await db.Reservations
                    .Include(r => r.Room)
                    .Where(r =>
                        r.Room.Status == RoomStatus.Occupied &&
                        r.CheckOutDate <= DateOnly.FromDateTime(now.DateTime))
                    .ToListAsync(stoppingToken);

                foreach (var res in finishedReservation)
                {
                    res.Room.SetStatus(RoomStatus.Cleaning);
                    res.Room.CleaningStartTime = now.DateTime; 
                }

                // 3️ Cleaning → Available 
                var cleaningRooms = await db.Rooms
                    .Where(r => r.Status == RoomStatus.Cleaning)
                    .ToListAsync(stoppingToken);

                foreach (var room in cleaningRooms)
                {
                    if (room.CleaningStartTime.HasValue &&
                        now - room.CleaningStartTime.Value >= TimeSpan.FromMinutes(_appSettings.RoomCleaningDurationMinutes))
                    {
                        room.SetStatus(RoomStatus.Available);
                        room.CleaningStartTime = null; 
                    }
                }

                // 4️ Available → UnderMaintenance
                var availableRooms = await db.Rooms
                    .Where(r => r.Status == RoomStatus.Available)
                    .ToListAsync(stoppingToken);

                foreach (var room in availableRooms)
                {
                    if (room.LastMaintenanceDate == null ||
                        now.DateTime.Date - room.LastMaintenanceDate.Value.Date >= TimeSpan.FromDays(_appSettings.RoomMaintenanceFrequencyDays))
                    {
                        room.SetStatus(RoomStatus.UnderMaintenance);
                        room.MaintenanceStartTime = now.DateTime; 
                    }
                }

                // 5️ UnderMaintenance → Available 
                var maintenanceDoneRooms = await db.Rooms
                    .Where(r => r.Status == RoomStatus.UnderMaintenance)
                    .ToListAsync(stoppingToken);

                foreach (var room in maintenanceDoneRooms)
                {
                    if (room.MaintenanceStartTime.HasValue &&
                        now - room.MaintenanceStartTime.Value >= TimeSpan.FromHours(_appSettings.RoomMaintenanceDurationHours))
                    {
                        room.SetStatus(RoomStatus.Available);
                        room.LastMaintenanceDate = now.DateTime; 
                        room.MaintenanceStartTime = null; 
                    }
                }

                await db.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating room statuses.");
            }
        }
    }
}
}

