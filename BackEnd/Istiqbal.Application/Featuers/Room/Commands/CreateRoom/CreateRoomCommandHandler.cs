

using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Application.Featuers.Room.Mappers;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace Istiqbal.Application.Featuers.Room.Commands.CreateRoom
{
    public sealed class CreateRoomCommandHandler
        (IAppDbContext _context, ILogger<CreateRoomCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<CreateRoomCommand, Result<RoomDto>>
    {
        public async Task<Result<RoomDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var exist = await _context.RoomTypes
                .AnyAsync(x => x.Id == request.roomTypeId, cancellationToken);

            if (!exist)
            {
                _logger.LogWarning("Room Type Not Found , RoomTypeId: {RoomTypeId}", request.roomTypeId);

                return ApplicationErrors.RoomTypeNotFound;
            }

            var lastRoom = await _context.Rooms
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Number)
                .FirstOrDefaultAsync(cancellationToken);

            var lastRoomNumber  = lastRoom?.Number  ?? 99;

            List<Domain.Amenities.Amenity> amenities = new();



            foreach (var amenityId in request.AmenitiesIds)
            {
                var existAmenity = await _context.Amenities.FirstOrDefaultAsync(x=>x.Id == amenityId, cancellationToken);

                if (existAmenity is null)
                {
                    _logger.LogWarning("Amenity with ID {Id} not found ", amenityId);

                }

                amenities.Add(existAmenity!);
            }

            var roomResult = Domain.RoomTypes.Rooms.
                Room.Create(Guid.NewGuid(), request.roomTypeId, lastRoomNumber, amenities);

            if (roomResult.IsError)
                return roomResult.Errors;

            var room = roomResult.Value;

            await _context.Rooms.AddAsync(room, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Room Created Successfully , RoomId: {RoomId}", room.Id);

            await _cache.RemoveByTagAsync("room", cancellationToken);


            return room.ToDto();
        }
    }
}
