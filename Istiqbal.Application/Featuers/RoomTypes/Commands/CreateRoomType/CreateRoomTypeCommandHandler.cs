
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomTypes.Dtos;
using Istiqbal.Application.Featuers.RoomTypes.Mappers;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Rooms.RoomTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace Istiqbal.Application.Featuers.RoomTypes.Commands.CreateRoomType
{
    public sealed class CreateRoomTypeCommandHandler
        (IAppDbContext _context, ILogger<CreateRoomTypeCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<CreateRoomTypeCommand, Result<RoomTypeDto>>
    {
        public async Task<Result<RoomTypeDto>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomName = request.Name.Trim().ToLower();

            var exists = await _context.RoomTypes
                .AnyAsync(x => x.Name == roomName, cancellationToken);

            if (exists)
            {
                _logger.LogWarning("Room type with name {RoomTypeName} already exists.", roomName);

                return RoomTypeErrors.RoomTypeNameAlreadyExists;
            }
            var roomTypeResult = RoomType
                .Create(Guid.NewGuid(),
                request.Name.Trim(),
                request.Description.Trim(),
                request.PricePerNight, 
                request.MaxOccupancy);

            if (roomTypeResult.IsError) 
                return roomTypeResult.Errors;

             await _context.RoomTypes.AddAsync(roomTypeResult.Value,cancellationToken);

             await _context.SaveChangesAsync(cancellationToken);

             await _cache.RemoveByTagAsync("roomType", cancellationToken);

            _logger.LogInformation("Room type with ID {RoomTypeId} created successfully.", roomTypeResult.Value.Id);

            var roomType = roomTypeResult.Value;

            return roomType.ToDto();
     
        }
    }
}
