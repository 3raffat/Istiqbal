using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Application.Featuers.RoomType.Mappers;
using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using Istiqbal.Domain.RoomTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomTypes.Commands.UpdateRoomType
{
    public sealed class UpdateRoomTypeCommandHandler
        (IAppDbContext _context,ILogger<UpdateRoomTypeCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<UpdateRoomTypeCommand, Result<RoomTypeDto>>
    {
        public async Task<Result<RoomTypeDto>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _context.RoomTypes.Include(x=>x.Amenities.Where(x=>!x.IsDeleted)).
                FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

            if (roomType is null)
            {
                _logger.LogWarning("Room type with ID {RoomTypeId} not found.", request.Id);

                return RoomTypeErrors.RoomTypeNotFound;
            }

            var amenities = await _context.Amenities.Where(x=>request.amenitieIds.Contains(x.Id)).ToListAsync(cancellationToken);

            var updatedRoomType = 
                roomType.Update(request.Name.Trim(), 
                request.Description.Trim(), 
                request.PricePerNight,
                request.MaxOccupancy,
                amenities);

            if(updatedRoomType.IsError)
                return updatedRoomType.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.RoomType.All, cancellationToken);

            _logger.LogInformation("Room type with ID {RoomTypeId} updated successfully.", roomType.Id);

            return roomType.ToDto();
        }
    }
}
