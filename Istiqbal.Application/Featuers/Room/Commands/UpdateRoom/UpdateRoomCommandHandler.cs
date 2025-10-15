using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Commands.DeleteRoom;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;


namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed class UpdateRoomCommandHandler
          (IAppDbContext _context, ILogger<UpdateRoomCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<UpdateRoomCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .Include(x=>x.Amenities)
                .SingleOrDefaultAsync(x => x.Id == request.id,cancellationToken);

            if (room is null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found.", request.id);

                return ApplicationErrors.RoomNotFound;
            }

            var roomType = await _context.RoomTypes
                .SingleOrDefaultAsync(x => x.Id == request.roomTypeId, cancellationToken);

            if (roomType is null)
            {
                _logger.LogWarning("RoomType with ID {RoomTypeId} not found.", request.roomTypeId);
                return ApplicationErrors.RoomTypeNotFound;
            }

            List<Domain.Amenities.Amenity> amenities = new();

            foreach (var amenityId in request.amenitiesId)
            {
                var existAmenity = await _context.Amenities.FirstOrDefaultAsync(x => x.Id == amenityId, cancellationToken);

                if (existAmenity is null)
                {
                    _logger.LogWarning("Amenity with ID {Id} not found ", amenityId);

                }

                amenities.Add(existAmenity!);
            }

            var roomResult = room.Update(request.roomStatus, request.roomTypeId,amenities);

            if (roomResult.IsError)
                return roomResult.Errors;


            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Room with ID {RoomId} updated successfully.", request.id);

            await  _cache.RemoveByTagAsync("room",cancellationToken);

            return Result.Updated;
        }
    }
}
