using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using Istiqbal.Domain.Rooms.RoomTypes;
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
        : IRequestHandler<UpdateRoomTypeCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _context.RoomTypes.
                FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (roomType is null)
            {
                _logger.LogWarning("Room type with ID {RoomTypeId} not found.", request.Id);

                return RoomTypeErrors.RoomTypeNotFound;
            }

            var updatedRoomType = 
                roomType.Update(request.Name.Trim(), 
                request.Description.Trim(), 
                request.PricePerNight,
                request.MaxOccupancy);

            if(updatedRoomType.IsError)
                return updatedRoomType.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync("roomType", cancellationToken);

            _logger.LogInformation("Room type with ID {RoomTypeId} updated successfully.", roomType.Id);

            return Result.Updated;
        }
    }
}
