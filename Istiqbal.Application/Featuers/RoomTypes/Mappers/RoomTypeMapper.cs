using Istiqbal.Application.Featuers.RoomTypes.Dtos;
using Istiqbal.Domain.Rooms.RoomTypes;

namespace Istiqbal.Application.Featuers.RoomTypes.Mappers
{
    public static class RoomTypeMapper
    {

        public static RoomTypeDto ToDto (this RoomType entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new RoomTypeDto(entity.Id,entity.Name,entity.Description,entity.PricePerNight,entity.MaxOccupancy);
        }

        public static List<RoomTypeDto> ToDtos(this IEnumerable<RoomType> entities)
        {
            return [.. entities.Select(x => x.ToDto())];
        }
    }
}
