using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Reservations.Dtos;
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
        : IRequestHandler<UpdateGuestReservationCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateGuestReservationCommand request, CancellationToken cancellationToken)
        {
            var guestReservation = await _context.Reservations
             .FirstOrDefaultAsync(r => r.Id == request.ReservationId && r.GuestId == request.GuestId, cancellationToken);

            if (guestReservation is null)
            {
                _logger.LogWarning("Reservation {ReservationId} not found for guest {GuestId}", request.ReservationId, request.GuestId);

                return ReservationErrors.ReservationNotFoundForGuest;
            }

            var reservationResult = guestReservation.Update(
                request.CheckInDate,
                request.CheckOutDate,
                request.roomId,
                request.Status
                );

            if (reservationResult.IsError)
             return reservationResult.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Reservation {ReservationId} updated successfully for guest {GuestId}", request.ReservationId, request.GuestId);

            return Result.Updated;
        }
    }
}
