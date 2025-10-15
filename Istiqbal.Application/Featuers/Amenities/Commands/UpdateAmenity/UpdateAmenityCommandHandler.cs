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
            var amenity = await _context.Amenities.FirstOrDefaultAsync(x=>x.Id==request.id,cancellationToken);

            if (amenity is null)
            {
                _logger.LogWarning("Amenity with Id: {AmenityId} not exists.", request.id);

                return AmenityErrors.AmenityIdNotFound;
            }

            var amenityResult = amenity.Update(request.name);

            if (amenityResult.IsError)
                return amenityResult.Errors;
              
            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveByTagAsync("amenity", cancellationToken);

            _logger.LogWarning("Amenity with ID {amenityId} is Update successfully",amenity.Id);

          return  amenity.toDto();
        }
    }
}
