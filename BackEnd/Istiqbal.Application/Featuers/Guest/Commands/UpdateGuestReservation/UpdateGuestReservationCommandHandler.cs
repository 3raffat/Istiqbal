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

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuestReservation
{
    public sealed class UpdateGuestReservationCommandHandler
        (IAppDbContext _context , ILogger<UpdateGuestReservationCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<UpdateGuestReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(UpdateGuestReservationCommand request, CancellationToken cancellationToken)
        { 
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == request.ReservationId && r.GuestId == request.GuestId && !r.IsDeleted && !r.Guest.IsDeleted,
                                                                            cancellationToken);
            if (reservation is null)
            {
                _logger.LogWarning("Reservation {ReservationId} not found for guest {GuestId}", request.ReservationId, request.GuestId);

                return ReservationErrors.ReservationNotFoundForGuest;
            }

            var reservationResult = reservation.Update(
                request.CheckInDate,
                request.CheckOutDate,
                request.RoomId,
                request.Status
                );

            if (reservationResult.IsError)
             return reservationResult.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Guest.All, cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Reservation.All, cancellationToken);

            _logger.LogInformation("Reservation {ReservationId} updated successfully for guest {GuestId}", request.ReservationId, request.GuestId);

            return reservation.toDto();
        }
    }
}
