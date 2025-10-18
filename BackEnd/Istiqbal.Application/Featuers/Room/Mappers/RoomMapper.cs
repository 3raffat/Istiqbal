using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Application.Featuers.Amenity.Mapper;
using Istiqbal.Application.Featuers.Room.Dtos;

namespace Istiqbal.Application.Featuers.Room.Mappers
{
    public static class RoomMapper
    {
        public static RoomDto ToDto(this Domain.RoomTypes.Rooms.Room room)
        {
            ArgumentNullException.ThrowIfNull(room);

            return new RoomDto(
                room.Id,
                room.Number,
                room.Type.Name,
                room.Floor,
                room.Status,
                room.Type.PricePerNight
            );
        }
       
        public static List<RoomDto> ToDtos(this IEnumerable<Domain.RoomTypes.Rooms.Room> rooms)
        {
            return [.. rooms.Select(x => x.ToDto())];
        } 
    }
}
