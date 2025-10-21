using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Commands.DeleteRoom;
using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Application.Featuers.Room.Mappers;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;


namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed class UpdateRoomCommandHandler
          (IAppDbContext _context, ILogger<UpdateRoomCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<UpdateRoomCommand, Result<RoomDto>>
    {
        public async Task<Result<RoomDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .SingleOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

            if (room is null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found.", request.Id);

                return ApplicationErrors.RoomNotFound;
            }

            var roomType = await _context.RoomTypes
                .SingleOrDefaultAsync(x => x.Id == request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                _logger.LogWarning("RoomType with ID {RoomTypeId} not found.", request.RoomTypeId);
                return ApplicationErrors.RoomTypeNotFound;
            }


            var roomResult = room.Update(request.RoomStatus, request.RoomTypeId);

            if (roomResult.IsError)
                return roomResult.Errors;


            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Room with ID {RoomId} updated successfully.", request.Id);

            await _cache.RemoveByTagAsync(CacheKeys.Room.All, cancellationToken);

            return room.ToDto();
        }
    }
}
