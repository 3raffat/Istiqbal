using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomTypes.Commands.UpdateRoomType;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;


namespace Istiqbal.Application.Featuers.RoomTypes.Commands.DeleteRoomType
{
    public sealed class DeleteRoomTypeCommandHandler
           (IAppDbContext _context, ILogger<DeleteRoomTypeCommandHandler> _logger, HybridCache _cache,IUser _user)
        : IRequestHandler<DeleteRoomTypeCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
           var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(x => x.Id == request.Id ,cancellationToken);

            if (roomType is null)
            {
                _logger.LogWarning("Room type with ID {RoomTypeId} not found.", request.Id);

                return ApplicationErrors.RoomTypeNotFound;
            }

            roomType.SoftDelete(_user.Id);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.RoomType.All, cancellationToken);

            _logger.LogInformation("Room type with ID {RoomTypeId} deleted successfully by user {UserId}.", roomType.Id, _user.Id );

            return Result.Deleted;
        }
    }
}
