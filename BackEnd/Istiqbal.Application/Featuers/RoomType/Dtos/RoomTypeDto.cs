
using Istiqbal.Application.Featuers.Amenity.Dtos;

namespace Istiqbal.Application.Featuers.RoomType.Dtos
{
    public sealed record RoomTypeDto(Guid Id, string Name, string Description, decimal PricePerNight, int MaxOccupancy,IEnumerable<AmenityDto> Amenities);
         

}
