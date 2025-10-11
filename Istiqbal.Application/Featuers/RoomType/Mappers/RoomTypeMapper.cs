using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomType.Mappers
{
    public static class RoomTypeMapper
    {
        public sealed record RoomTypeDto(Guid Id, string Name, string Description, decimal PricePerNight, int MaxOccupancy);

        public static RoomTypeDto ToDto (this Domain.Rooms.RoomTypes.RoomType entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new RoomTypeDto(entity.Id,entity.Name,entity.Description,entity.PricePerNight,entity.MaxOccupancy);
        }

        public static List<RoomTypeDto> ToDtos(this IEnumerable<Domain.Rooms.RoomTypes.RoomType> entities)
        {
            return [.. entities.Select(x => x.ToDto())];
        }
    }
}
