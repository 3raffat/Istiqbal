

using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace Istiqbal.Application.Featuers.Amenities.Commands.DeleteAmenity
{
    public sealed class DeleteAmenityCommandHandler
        (IAppDbContext _context,ILogger<DeleteAmenityCommandHandler> _logger, HybridCache _cache,IUser _user) : IRequestHandler<DeleteAmenityCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

            if (amenity is null )
            {
                _logger.LogWarning("Amenity with Id: {AmenityId} not exists.", request.Id);

                return AmenityErrors.AmenityIdNotFound;
            }

            amenity.SoftDelete(_user.Id);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Amenity.All, cancellationToken);

            _logger.LogInformation("Amenity with ID {AmenityId} deleted successfully.", amenity.Id);

            return Result.Deleted;
        }
    }
}
