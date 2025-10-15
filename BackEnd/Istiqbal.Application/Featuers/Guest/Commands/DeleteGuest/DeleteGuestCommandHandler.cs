using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Commands.CreateGuest;
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

namespace Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest
{
    public sealed class DeleteGuestCommandHandler
          (IAppDbContext _context, ILogger<DeleteGuestCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<DeleteGuestCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guest= await _context.Guests
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (guest is null)
            {
                _logger.LogWarning("Guest with ID {GuestId} not found.", request.Id);

                return ApplicationErrors.GuestNotFound;
            }

             _context.Guests.Remove(guest);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Guest with ID {GuestId} deleted successfully.", request.Id);

            await _cache.RemoveByTagAsync("guest", cancellationToken);

            return Result.Deleted;
        }
    }
}
