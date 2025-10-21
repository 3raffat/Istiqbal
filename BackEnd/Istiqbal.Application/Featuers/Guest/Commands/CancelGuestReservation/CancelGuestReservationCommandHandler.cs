using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Application.Featuers.Reservations.Mapper;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes.Reservations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.DeleteGuestReservation
{
    public sealed class CancelGuestReservationCommandHandler
        (IAppDbContext _context, ILogger<CancelGuestReservationCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<CancelGuestReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(CancelGuestReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations .FirstOrDefaultAsync(r => r.Id == request.ReservationId && r.GuestId == request.GuestId && !r.IsDeleted && !r.Guest.IsDeleted,
                                                                               cancellationToken);
            if (reservation is null)
            {
                _logger.LogWarning("Reservation {ReservationId} not found for guest {GuestId}", request.ReservationId, request.GuestId);

                return ReservationErrors.ReservationNotFoundForGuest;
            }

            var now = DateTimeOffset.UtcNow;
            var checkIn = reservation.CheckInDate;

            var hoursUntilCheckIn = (checkIn - now).TotalHours;

            if (hoursUntilCheckIn < 24)
            {
                _logger.LogWarning(
                    "Cannot cancel reservation {ReservationId} - only {Hours:F1} hours until check-in",
                    request.ReservationId,
                    hoursUntilCheckIn);
                return ReservationErrors.CancellationTooLate;
            }


            var reservationResult = reservation.Cancel();

            if (reservationResult.IsError)
                return reservationResult.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Guest.All,cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Reservation.All, cancellationToken);

            await _context.Entry(reservation)
             .Reference(r => r.Guest)
             .LoadAsync(cancellationToken);

            await _context.Entry(reservation)
              .Reference(r => r.Room)
              .LoadAsync(cancellationToken);

            _logger.LogInformation("Reservation {ReservationId} cancelled successfully for guest {GuestId}",
                reservation.Id, request.GuestId);

            return reservation.toDto();
        }
    }
}