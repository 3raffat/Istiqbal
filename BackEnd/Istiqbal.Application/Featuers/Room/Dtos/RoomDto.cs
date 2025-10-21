using Istiqbal.Application.Featuers.Amenity.Dtos;
using Istiqbal.Contracts.Requests.Rooms;


namespace Istiqbal.Application.Featuers.Room.Dtos
{
    public sealed record  RoomDto (Guid id , int Number, string RoomTypeName, int Floor, RoomStatus Status,decimal AmountPerNight );
   
}
