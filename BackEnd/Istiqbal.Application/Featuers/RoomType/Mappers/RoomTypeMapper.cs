using Istiqbal.Application.Featuers.Amenity.Mapper;
using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Domain.RoomTypes;

namespace Istiqbal.Application.Featuers.RoomType.Mappers
{
    public static class RoomTypeMapper
    {

        public static RoomTypeDto ToDto(this Istiqbal.Domain.RoomTypes.RoomType entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new RoomTypeDto(entity.Id,
                entity.Name,
                entity.Description,
                entity.PricePerNight,
                entity.MaxOccupancy,
                entity.Amenities.Select(x =>x.toDto()).ToList()
                );
        }

        public static List<RoomTypeDto> ToDtos(this IEnumerable<Istiqbal.Domain.RoomTypes.RoomType> entities)
        {
            return [.. entities.Select(x => x.ToDto())];
        }
    }
}