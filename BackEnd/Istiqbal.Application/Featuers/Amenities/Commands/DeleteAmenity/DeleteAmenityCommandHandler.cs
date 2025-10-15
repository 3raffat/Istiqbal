

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
        (IAppDbContext _context,ILogger<DeleteAmenityCommandHandler> _logger, HybridCache _cache) : IRequestHandler<DeleteAmenityCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);

            if (amenity is null )
            {
                _logger.LogWarning("Amenity with Id: {AmenityId} not exists.", request.id);

                return AmenityErrors.AmenityIdNotFound;
            }

            _context.Amenities.Remove(amenity);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync("amenity", cancellationToken);

            _logger.LogInformation("Amenity with ID {AmenityId} deleted successfully.", amenity.Id);

            return Result.Deleted;
        }
    }
}
