using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Contracts.Requests.Rooms;


namespace Istiqbal.Application.Featuers.Room.Dtos
{
    public sealed record  RoomDto (Guid id , int number, string roomTypeId, int floor, RoomStatus status,decimal AmountPerNight ,List<AmenityDto> amenities);
   
}
