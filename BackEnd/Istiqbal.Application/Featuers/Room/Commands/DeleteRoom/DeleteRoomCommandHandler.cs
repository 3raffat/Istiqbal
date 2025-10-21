using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.DeleteRoom
{
    public sealed class DeleteRoomCommandHandler
        (IAppDbContext _context ,ILogger<DeleteRoomCommandHandler> _logger, HybridCache _cache,IUser _user)
        : IRequestHandler<DeleteRoomCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
           var room = await _context.Rooms.FirstOrDefaultAsync(x=>x.Id ==request.Id,cancellationToken);

            if (room is null)
            {
                _logger.LogWarning("Room with Id {RoomId} not found.", request.Id);

                return ApplicationErrors.RoomNotFound;
            }

            room.SoftDelete(_user.Id);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Room.All, cancellationToken);

            _logger.LogInformation("Room with Id {RoomId} deleted successfully by user {UserId}.", request.Id,_user.Id);

            return Result.Deleted;
        }
    }
}
