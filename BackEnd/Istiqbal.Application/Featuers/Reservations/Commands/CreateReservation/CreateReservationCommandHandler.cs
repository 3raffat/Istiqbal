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

namespace Istiqbal.Application.Featuers.Reservations.Commands.CreateReservation
{
    public sealed class CreateReservationCommandHandler
        (IAppDbContext _context ,ILogger<CreateReservationCommandHandler> _logger , HybridCache _cache)
        : IRequestHandler<CreateReservationCommand, Result<ReservationDto>>
    {
        public async Task<Result<ReservationDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
           var guestExists= await _context.Guests.AnyAsync(x=>x.Id==request.GuestId,cancellationToken);

            if(!guestExists)
            {
                _logger.LogWarning("Guest with ID {GuestId} was not found.", request.GuestId);

                return GuestErrors.GuestNotFound;
            }

            var room = await _context.Rooms.Include(x=>x.Type).FirstOrDefaultAsync(x=>x.Id==request.roomId,cancellationToken);

            if(room is null)
            {
                _logger.LogWarning("Room with ID {roomId} was not found.", request.roomId);

                return RoomErrors.RoomNotFound;
            }

            if (request.CheckOutDate.Date <= request.CheckInDate.Date)
            {
                _logger.LogWarning("Invalid reservation dates: Check-out {CheckOutDate} must be after check-in {CheckInDate}.", request.CheckOutDate, request.CheckInDate);

                return ReservationErrors.ReservationCheckOutDateInvalid;
            }

            var numberOfDays = (request.CheckOutDate - request.CheckInDate).Days;

            var pricePerNight = room.Type.PricePerNight;

            var ReservationResult = Domain.Guestes.Reservations
                .Reservation.Create(
                Guid.NewGuid(),
                request.GuestId,
                request.roomId,
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

            await _cache.RemoveByTagAsync("reservation",cancellationToken);

            return reservation.toDto();
        } 
    }
}
