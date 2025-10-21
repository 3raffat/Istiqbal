 
namespace Istiqbal.Contracts.Requests.Rooms
{
    public sealed class CreateRoomRequest
    {
        public Guid RoomTypeId { get; set; }
        public RoomStatus RoomStatus { get; set; }

    }

}
