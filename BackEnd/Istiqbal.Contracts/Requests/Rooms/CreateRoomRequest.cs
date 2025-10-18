 
namespace Istiqbal.Contracts.Requests.Rooms
{
    public sealed class CreateRoomRequest
    {
        public Guid roomTypeId { get; set; }
        public RoomStatus roomStatus { get; set; }

    }

}
