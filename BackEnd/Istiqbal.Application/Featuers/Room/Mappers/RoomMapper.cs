using Istiqbal.Application.Featuers.Amenity.Dtos;
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
                room.Type.PricePerNight,
                room.Amenities.Select(a=>a.toDto()).ToList()
            );
        }
       
        public static List<RoomDto> ToDtos(this IEnumerable<Domain.RoomTypes.Rooms.Room> rooms)
        {
            return [.. rooms.Select(x => x.ToDto())];
        }

        public static AmenityDto toDto(this Domain.Amenities.Amenity amenity)
        {
            return new AmenityDto(
                id: amenity.Id,
                name: amenity.Name
                );
        }
       
        public static List<AmenityDto> ToDtos(this IEnumerable<Domain.Amenities.Amenity> amenity)
        {
            return [..amenity.Select(x=>x.toDto())];
        }
    }
}
