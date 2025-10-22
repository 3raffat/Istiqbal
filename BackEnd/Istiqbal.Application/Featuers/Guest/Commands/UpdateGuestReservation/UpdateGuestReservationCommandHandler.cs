using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Application.Featuers.Reservations.Mapper;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes.Reservations;
using Istiqbal.Domain.RoomTypes.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuestReservation
{
    public sealed class UpdateGuestReservationCommandHandler
        (IAppDbContext _context , ILogger<UpdateGuestReservationCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<UpdateGuestReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(UpdateGuestReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                     .Include(r => r.Guest)
                     .Include(r => r.Room)
                     .ThenInclude(rm => rm.Type)
                     .FirstOrDefaultAsync(
                         r => r.Id == request.ReservationId
                             && r.GuestId == request.GuestId
                             && !r.IsDeleted
                             && !r.Guest.IsDeleted,
                         cancellationToken); 

            if (reservation is null)
            {
                _logger.LogWarning("Reservation {ReservationId} not found for guest {GuestId}", request.ReservationId, request.GuestId);

                return ReservationErrors.ReservationNotFoundForGuest;
            }

            if (reservation.Status == ReservationStatus.Completed ||reservation.Status == ReservationStatus.Cancelled)
            {
                _logger.LogWarning(
                    "Cannot update reservation {ReservationId} with status {Status}",
                    request.ReservationId,
                    reservation.Status);
                return ReservationErrors.CannotUpdateCompletedOrCancelledReservation;
            }

            if (request.RoomId != reservation.RoomId)
            {
                var roomExists = await _context.Rooms
                    .AnyAsync(r => r.Id == request.RoomId && !r.IsDeleted, cancellationToken);

                if (!roomExists)
                {
                    _logger.LogWarning("Room with ID {RoomId} was not found.", request.RoomId);
                    return RoomErrors.RoomNotFound;
                }
            }

            var hasConflict = await _context.Reservations
                .AnyAsync(x => x.Id != request.ReservationId 
                    && x.RoomId == request.RoomId
                    && x.CheckInDate < request.CheckOutDate
                    && x.CheckOutDate > request.CheckInDate
                    && x.Status != ReservationStatus.Cancelled,
                    cancellationToken);

            if (hasConflict)
            {
                _logger.LogWarning(
                    "Room {RoomId} is not available for the requested dates: {CheckInDate} to {CheckOutDate}.",
                    request.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate);
                return ReservationErrors.RoomNotAvailable;
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
