using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest;
using Istiqbal.Application.Featuers.Guest.Dtos;
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

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest
{
    public sealed class UpdateGuestCommandHandler
        (IAppDbContext _context, ILogger<UpdateGuestCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<UpdateGuestCommand, Result<Updated>>

    {
        public async Task<Result<Updated>> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await _context.Guests
                .FirstOrDefaultAsync(x=>x.Id==request.id);

            if(guest is null)
            {
                _logger.LogWarning("Guest with Id {GuestId} not found.", request.id);

                return ApplicationErrors.GuestNotFound;
            }

            var updateResult = guest.Update(
                request.fullName.Trim()
                , request.phone.Trim()
                ,request.email.Trim());

            if (updateResult.IsError)
                return updateResult.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Guest with Id {GuestId} updated successfully.", guest.Id);

             await _cache.RemoveByTagAsync("guest",cancellationToken);

            return Result.Updated;
        }
    }
}
