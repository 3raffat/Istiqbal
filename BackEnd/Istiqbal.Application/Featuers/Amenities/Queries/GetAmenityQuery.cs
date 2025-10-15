using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenities.Queries
{
    public sealed class GetAmenityQuery : ICachedQuery<Result<List<AmenityDto>>>
    {
        public string CacheKey => "amenity";

        public string[] Tags => ["amenity"];

        public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
    }
}
