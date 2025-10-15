
namespace Istiqbal.Contracts.Requests.Rooms
{
    public sealed class CreateRoomRequest
    {
      
        public Guid roomTypeId { get; set; }
        public List<CreateAmenityRequest> Amenities { get; set; }
    }

}
