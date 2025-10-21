using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Application.Featuers.Guest.Mapper;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace Istiqbal.Application.Featuers.Guest.Commands.CreateGuest
{
    public sealed class CreateGuestCommandHandler
          (IAppDbContext _context, ILogger<CreateGuestCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<CreateGuestCommand, Result<GuestDto>>
    {
        public async Task<Result<GuestDto>> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestName = request.FullName.Trim();
            var guestPhone = request.Phone.Trim();
            var guestEmail = request.Email.Trim();

            var existingGuest = await _context.Guests
                .Select(x => new { x.FullName, x.Phone, x.Email })
                .FirstOrDefaultAsync(x =>
                    x.FullName.ToLower() == guestName.ToLower() ||
                    x.Phone == guestPhone ||
                    x.Email.ToLower() == guestEmail.ToLower(),
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

            var guestResult = Domain.Guestes.Guest.Create(Guid.NewGuid(),
                                                          guestName,
                                                          guestPhone,
                                                          guestEmail);

            if (guestResult.IsError)
                return guestResult.TopError;

            var guest = guestResult.Value;

            await _context.Guests.AddAsync(guest, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Guest with ID {GuestId} created successfully.", guest.Id);

            await _cache.RemoveByTagAsync(CacheKeys.Guest.All, cancellationToken);

            return guest.toDto();
        }
    }
}
