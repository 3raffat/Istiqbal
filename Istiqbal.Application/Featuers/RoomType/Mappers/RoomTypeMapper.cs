
using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Domain.Entities;

namespace Istiqbal.Application.Featuers.RoomType.Mappers
{
    public static class RoomTypeMapper
    {

        public static RoomTypeDto ToDto(this RoomType entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new RoomTypeDto(entity.Id, entity.Name, entity.Description, entity.PricePerNight, entity.MaxOccupancy);
        }

        public static List<RoomTypeDto> ToDtos(this IEnumerable<RoomType> entities)
        {
            return [.. entities.Select(x => x.ToDto())];
        }
    }
}