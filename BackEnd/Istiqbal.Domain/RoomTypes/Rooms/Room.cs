using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes.Reservations;

namespace Istiqbal.Domain.RoomTypes.Rooms
{
    public sealed class Room : AuditableEntity
    {
        public int Number { get; private set; }
        public Guid RoomTypeId { get; private set; }
        public RoomType Type { get; private set; } = null!;
        public int Floor { get;  }
        public RoomStatus Status { get; private set; } 
        private readonly List<Reservation> _reservation = new();
        public IEnumerable<Reservation> Reservation => _reservation.AsReadOnly();
  
        private Room() { }
        private Room(Guid id, Guid roomTypeId,int floor,int number,RoomStatus status) : base(id)
        {
            RoomTypeId = roomTypeId;
            Floor = floor;
            Number = number;
            Status = status;
        }
        public static Result<Room> Create(Guid id, Guid roomTypeId,int lastRoomNumber, RoomStatus status)
        {
            if (id == Guid.Empty)
                return RoomErrors.RoomIdRequerd;

            if (roomTypeId == Guid.Empty)
                return RoomErrors.RoomTypeIdRequerd;

            var roomNumber = lastRoomNumber +1;

            return new Room(id, roomTypeId, GetFloorNumber(lastRoomNumber), roomNumber, status);
        }
        public Result<Updated> Update(RoomStatus roomStatus, Guid roomTypeId)
        {

            if (roomTypeId == Guid.Empty)
                return RoomErrors.RoomTypeIdRequerd;

            if (!Enum.IsDefined(roomStatus))
                return RoomErrors.RoomStatusInvalid;

            Status = roomStatus;
            RoomTypeId = roomTypeId;
            return Result.Updated;
        }
        //public Result<Updated> AddAmenities(List<Amenity> amenities)
        //{
        //    _amenities.RemoveAll(exist => amenities.All(x=>x.Id != exist.Id)); 

        //    foreach (var amenity in amenities)
        //    {
        //       var exist = _amenities.FirstOrDefault(x => x.Id == amenity.Id);
        //        if (exist is null)
        //        {
        //            _amenities.Add(amenity);
        //        }
        //        else
        //        {
        //            var result = exist.Update(amenity.Name);

        //            if (result.IsError)
        //                return result.Errors;
        //        }

        //    }
        //    return Result.Updated;
        //}
        private static int GetFloorNumber(int lastRoomNumber,int roomPerFloor = 10)
        {
         

            int floorNumber = ((lastRoomNumber-100)/roomPerFloor) +1;


            return floorNumber;
        }   
    }
}
