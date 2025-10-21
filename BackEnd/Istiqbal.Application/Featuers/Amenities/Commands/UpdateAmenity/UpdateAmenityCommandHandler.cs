using Istiqbal.Application.Common.Caching;
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

namespace Istiqbal.Application.Featuers.Amenity.Commands.UpdateAmenity
{
    public class UpdateAmenityCommandHandler
         (IAppDbContext _context, ILogger<UpdateAmenityCommandHandler> _logger ,HybridCache _cache)
        : IRequestHandler<UpdateAmenityCommand, Result<AmenityDto>>
    {
        public async Task<Result<AmenityDto>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(x=>x.Id==request.Id && !x.IsDeleted,cancellationToken);

            if (amenity is null)
            {
                _logger.LogWarning("Amenity with Id: {AmenityId} not exists.", request.Id);

                return AmenityErrors.AmenityIdNotFound;
            }

            var requestName = request.Name.Trim().ToLower();

            var existName = await _context.Amenities.AnyAsync(x=>x.Name.ToLower() == requestName && x.Id != request.Id, cancellationToken);

            if(existName)
            {
                _logger.LogWarning("Amenity with Name: {AmenityName} already exists", request.Name);

                return AmenityErrors.AmenityAlreadyExists;
            }

            var amenityResult = amenity.Update(request.Name);

            if (amenityResult.IsError)
                return amenityResult.Errors;
              
            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync(CacheKeys.Amenity.All, cancellationToken);

            _logger.LogInformation("Amenity with ID {AmenityId} is Update successfully",amenity.Id);

          return  amenity.toDto();
        }
    }
}
