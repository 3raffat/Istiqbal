using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Application.Featuers.Amenity.Mapper;
using Istiqbal.Domain.Amenities;
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

namespace Istiqbal.Application.Featuers.Amenity.Commands.CreateAmenity
{
    public sealed class CreateAmenityCommandHandler
        (IAppDbContext _context,ILogger<CreateAmenityCommandHandler> _logger, HybridCache _cache)
        : IRequestHandler<CreateAmenityCommand, Result<AmenityDto>>
    {
        public async Task<Result<AmenityDto>> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var name = request.name.ToLower();
            var exist = await _context.Amenities.AnyAsync(x => x.Name.ToLower() == name, cancellationToken);

            if (exist)
            {
                _logger.LogWarning("Attempt to create an amenity that already exists: {name}", name);

                return AmenityErrors.AmenityAlreadyExists;
            }

            var amenityResult = Domain.Amenities.Amenity.Create(
                Guid.NewGuid()
                ,request.name);

            if (amenityResult.IsError)
                return amenityResult.Errors;

            var amenity = amenityResult.Value;

            await _context.Amenities.AddAsync(amenity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync("amenity");

            _logger.LogInformation("Amenity created successfully: {AmenityName}", amenity.Name);

            return amenity.toDto();
        }
    }
}
