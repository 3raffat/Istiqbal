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
        (IAppDbContext _context, ILogger<CreateRoomCommandHandler> _logger,HybridCache _cache)
        : IRequestHandler<CreateRoomCommand, Result<RoomDto>>
    {
        public async Task<Result<RoomDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var exist = await _context.RoomTypes
                .AnyAsync(x=>x.Id == request.roomTypeId,cancellationToken);

            if (!exist)
            {
                _logger.LogWarning("Room Type Not Found , RoomTypeId: {RoomTypeId}", request.roomTypeId);

                return ApplicationErrors.RoomTypeNotFound;
            }

            var lastRoom = await _context.Rooms
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Number)
                .FirstOrDefaultAsync(cancellationToken);

            int floorNumber = (lastRoom != null) ? lastRoom.Number + 1 : 100;

            var roomResult = Istiqbal.Domain.Rooms.
                Room.Create(Guid.NewGuid(), request.roomTypeId, floorNumber);

            if (roomResult.IsError)
                return roomResult.Errors;

            await _context.Rooms.AddAsync(roomResult.Value, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var room = roomResult.Value;

            _logger.LogInformation("Room Created Successfully , RoomId: {RoomId}", room.Id);

             await _cache.RemoveByTagAsync("room",cancellationToken);


            return room.ToDto();
        }
    }
}
