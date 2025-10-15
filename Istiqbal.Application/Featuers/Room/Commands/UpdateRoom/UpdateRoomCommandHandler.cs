using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Commands.DeleteRoom;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Rooms.Amenities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var roomResult = room.Update(request.roomStatus,request.roomTypeId);

            if(roomResult.IsError)
                return roomResult.Errors;

            //List<Amenity> amenities = new();

            //foreach (var amenity in request.amenities)
            //{
            //    var amenityResult = Istiqbal.Domain.Rooms.Amenities.Amenity
            //        .Create(amenity.id,amenity.name.Trim());

            //    if (amenityResult.IsError)
            //        return amenityResult.Errors;

            //    amenities.Add(amenityResult.Value);
            //}

            //var amenitiesResult = room.AddAmenities(amenities);

            //if (amenitiesResult.IsError)
            //    return amenitiesResult.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Room with ID {RoomId} updated successfully.", request.id);

            await  _cache.RemoveByTagAsync("room",cancellationToken);

            return Result.Updated;
        }
    }
}
