

using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Application.Featuers.Room.Mappers;
using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.RoomTypes.Rooms;
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
            var roomType = await _context.RoomTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.roomTypeId, cancellationToken);


            if (roomType is null)
            {
                _logger.LogWarning("Room Type Not Found , RoomTypeId: {RoomTypeId}", request.roomTypeId);

                return ApplicationErrors.RoomTypeNotFound;
            }

            if (!Enum.IsDefined(request.roomStatus))
                return RoomErrors.RoomStatusInvalid;

            var lastRoom = await _context.Rooms
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Number)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            var lastRoomNumber = lastRoom?.Number is null or 0 ? 99 : lastRoom.Number;

            List<Domain.Amenities.Amenity> amenities = new();

            var roomResult = Domain.RoomTypes.Rooms.
                Room.Create(Guid.NewGuid(), request.roomTypeId, lastRoomNumber,request.roomStatus);

            if (roomResult.IsError)
                return roomResult.Errors;

            var room = roomResult.Value;

            await _context.Rooms.AddAsync(room, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(room)
               .Reference(r => r.Type)
               .LoadAsync(cancellationToken);

            _logger.LogInformation("Room Created Successfully , RoomId: {RoomId}", room.Id);

            await _cache.RemoveByTagAsync("room", cancellationToken);


            return room.ToDto();
        }
    }
}
