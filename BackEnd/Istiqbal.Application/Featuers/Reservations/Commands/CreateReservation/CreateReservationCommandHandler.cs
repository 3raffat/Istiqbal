using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Application.Featuers.Reservations.Mapper;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes;
using Istiqbal.Domain.Guestes.Reservations;
using Istiqbal.Domain.RoomTypes.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation
{
    public sealed class CreateReservationCommandHandler
        (IAppDbContext _context ,ILogger<CreateReservationCommandHandler> _logger , HybridCache _cache)
        : IRequestHandler<CreateReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {

            if (request.CheckOutDate <= request.CheckInDate)
            {
                _logger.LogWarning("Invalid reservation dates: Check-out {CheckOutDate} must be after check-in {CheckInDate}.", request.CheckOutDate, request.CheckInDate);

                return ReservationErrors.ReservationCheckOutDateInvalid;
            }

            var guestExists = await _context.Guests.AnyAsync(x=>x.Id==request.GuestId && !x.IsDeleted,cancellationToken);

            if(!guestExists)
            {
                _logger.LogWarning("Guest with ID {GuestId} was not found.", request.GuestId);

                return GuestErrors.GuestNotFound;
            }

            var room = await _context.Rooms.Include(x=>x.Type).FirstOrDefaultAsync(x=>x.Id==request.RoomId,cancellationToken);

            if(room is null)
            {
                _logger.LogWarning("Room with ID {roomId} was not found.", request.RoomId);

                return RoomErrors.RoomNotFound;
            }

         
            var hasConflict = await _context.Reservations.AnyAsync(x => x.RoomId == request.RoomId
                    && x.CheckInDate < request.CheckOutDate
                    && x.CheckOutDate > request.CheckInDate
                    && x.Status != ReservationStatus.Cancelled,cancellationToken);

            if (hasConflict)
            {
                _logger.LogWarning(
                    "Room {RoomId} is not available for the requested dates: {CheckInDate} to {CheckOutDate}.",
                    request.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate);

                return ReservationErrors.RoomNotAvailable;
            }
            var numberOfDays = (request.CheckOutDate.ToDateTime(TimeOnly.MinValue) - request.CheckInDate.ToDateTime(TimeOnly.MinValue)).Days;

            var pricePerNight = room.Type.PricePerNight;

            var ReservationResult = Domain.Guestes.Reservations
                .Reservation.Create(
                Guid.NewGuid(),
                request.GuestId,
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate,
                numberOfDays,
                pricePerNight
                );

            if(ReservationResult.IsError)
                return ReservationResult.Errors;

            var reservation = ReservationResult.Value;

            await _context.Reservations.AddAsync(reservation,cancellationToken);    

            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(reservation)
              .Reference(r => r.Guest)
              .LoadAsync(cancellationToken);

            await _context.Entry(reservation)
              .Reference(r => r.Room)
              .LoadAsync(cancellationToken);

            _logger.LogInformation("Reservation created successfully. ReservationId: {ReservationId}, GuestId: {GuestId}",reservation.Id,reservation.GuestId);

            await _cache.RemoveByTagAsync(CacheKeys.Reservation.All,cancellationToken);

            return reservation.toDto();
        } 
    }
}
