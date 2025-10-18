using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Application.Featuers.Amenity.Mapper;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenities.Queries
{
    public sealed class GetAmenityQueryHandler(IAppDbContext _context) : IRequestHandler<GetAmenityQuery, Result<List<AmenityDto>>>
    {
        public async Task<Result<List<AmenityDto>>> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
        {
            var amenities = await _context.Amenities.AsNoTracking().ToListAsync(cancellationToken);

            return amenities.toDtos();
        }
    }
}
