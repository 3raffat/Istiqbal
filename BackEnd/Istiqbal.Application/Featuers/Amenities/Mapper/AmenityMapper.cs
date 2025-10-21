using Istiqbal.Application.Featuers.Amenity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Amenity.Mapper
{
    public static class AmenityMapper
    {

        public static AmenityDto toDto (this Domain.Amenities.Amenity amenity)
        {
            return new AmenityDto(
                Id: amenity.Id,
                Name:amenity.Name
                );
        }
        public static List<AmenityDto> toDtos(this IEnumerable< Domain.Amenities.Amenity> amenity)
        {
            return [..amenity.Select(a=> toDto(a))];
        }


    }
}
