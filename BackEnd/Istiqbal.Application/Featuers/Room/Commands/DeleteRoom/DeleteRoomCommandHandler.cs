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
        (IAppDbContext _context ,ILogger<DeleteRoomCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<DeleteRoomCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
           var room = await _context.Rooms.SingleOrDefaultAsync(x=>x.Id ==request.id,cancellationToken);

            if (room is null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found.", request.id);

                return ApplicationErrors.RoomNotFound;
            }

            _context.Rooms.Remove(room);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync("room", cancellationToken);

            _logger.LogInformation("Room with ID {RoomId} deleted successfully.", request.id);

            return Result.Deleted;
        }
    }
}
