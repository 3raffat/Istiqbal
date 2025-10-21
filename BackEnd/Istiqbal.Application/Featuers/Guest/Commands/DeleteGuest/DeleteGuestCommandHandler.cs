using Istiqbal.Application.Common.Caching;
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
          (IAppDbContext _context, ILogger<DeleteGuestCommandHandler> _logger, HybridCache _cache,IUser _user)
        : IRequestHandler<DeleteGuestCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await _context.Guests.Include(x=>x.Reservation.Where(x=>!x.IsDeleted))
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

            if (guest is null)
            {
                _logger.LogWarning("Guest with ID {GuestId} not found.", request.Id);

                return ApplicationErrors.GuestNotFound;
            }

            guest.SoftDelete(_user.Id);

            foreach (var reservation in guest.Reservation)
            {
                reservation.Cancel();
                reservation.SoftDelete(_user.Id);
            }


            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Guest with ID {GuestId} deleted successfully by user {UserId}. ", request.Id,_user.Id);

            await _cache.RemoveByTagAsync(CacheKeys.Guest.All, cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Reservation.All, cancellationToken);

            return Result.Deleted;
        }
    }
}
