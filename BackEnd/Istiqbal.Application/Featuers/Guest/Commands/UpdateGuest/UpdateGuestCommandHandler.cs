using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest;
using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Application.Featuers.Guest.Mapper;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes;
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
        : IRequestHandler<UpdateGuestCommand, Result<GuestDto>>

    {
        public async Task<Result<GuestDto>> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guest = await _context.Guests
                .FirstOrDefaultAsync(x=>x.Id==request.Id && !x.IsDeleted);

            if(guest is null)
            {
                _logger.LogWarning("Guest with Id {GuestId} not found.", request.Id);

                return ApplicationErrors.GuestNotFound;
            }

            var guestName = request.FullName.Trim();
            var guestPhone = request.Phone.Trim();
            var guestEmail = request.Email.Trim();

            var existingGuest = await _context.Guests
                .Select(x => new {x.Id ,x.FullName, x.Phone, x.Email })
                .FirstOrDefaultAsync(x => x.Id != request.Id &&
                   (x.FullName.ToLower() == guestName.ToLower() ||
                    x.Phone == guestPhone ||
                    x.Email.ToLower() == guestEmail.ToLower()),
                    cancellationToken);

            if (existingGuest is not null)
            {
                if (string.Equals(existingGuest.FullName, guestName, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("Guest with name {GuestName} already exists.", guestName);
                    return GuestErrors.GuestNameAlreadyExists;
                }
                if (existingGuest.Phone == guestPhone)
                {
                    _logger.LogWarning("Guest with phone {GuestPhone} already exists.", guestPhone);
                    return GuestErrors.GuestPhoneAlreadyExists;
                }
                if (string.Equals(existingGuest.Email, guestEmail, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning("Guest with email {GuestEmail} already exists.", guestEmail);
                    return GuestErrors.GuestEmailAlreadyExists;
                }
            }
            var updateResult = guest.Update(guestName,
                                            guestPhone,
                                            guestEmail);

            if (updateResult.IsError)
                return updateResult.Errors;

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Guest with Id {GuestId} updated successfully.", guest.Id);

            await _cache.RemoveByTagAsync(CacheKeys.Guest.All, cancellationToken);

            return guest.toDto();
        }
    }
}
