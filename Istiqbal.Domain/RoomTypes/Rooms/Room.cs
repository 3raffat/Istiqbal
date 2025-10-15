using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Guestes.Reservations;
using Istiqbal.Domain.RoomTypes.Enums;

namespace Istiqbal.Domain.RoomTypes.Rooms
{
    public sealed class Room : AuditableEntity
    {
        public int Number { get; private set; }
        public Guid RoomTypeId { get; private set; }
        public RoomType Type { get; private set; } = null!;
        public int Floor { get;  }
        public RoomStatus Status { get; private set; } = RoomStatus.Available;
        private readonly List<Reservation> _reservation = new();
        public IReadOnlyCollection<Reservation> Reservation => _reservation.AsReadOnly();
        private List<Amenity> _amenities = new();
        public IReadOnlyCollection<Amenity> Amenities => _amenities.AsReadOnly();
        private Room() { }
        private Room(Guid id, Guid roomTypeId,int floor, List<Amenity> amenities) : base(id)
        {
            RoomTypeId = roomTypeId;
            Floor = floor;
            _amenities = amenities;
        }
        public static Result<Room> Create(Guid id, Guid roomTypeId,int floor, List<Amenity> amenities)
        {
            if (id == Guid.Empty)
                return RoomErrors.RoomIdRequerd;

            if (roomTypeId == Guid.Empty)
                return RoomErrors.RoomTypeIdRequerd;


            return new Room(id, roomTypeId, GetFloorNumber(floor), amenities);
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
        public Result<Updated> AddAmenities(List<Amenity> amenities)
        {
            _amenities.RemoveAll(exist => amenities.All(x=>x.Id != exist.Id)); 

            foreach (var amenity in amenities)
            {
               var exist = _amenities.FirstOrDefault(x => x.Id == amenity.Id);
                if (exist is null)
                {
                    _amenities.Add(amenity);
                }
                else
                {
                    var result = exist.Update(amenity.Name);

                    if (result.IsError)
                        return result.Errors;
                }

            }
            return Result.Updated;
        }
        private static int GetFloorNumber(int? lastRoomNumber, int baseFloor = 1, int roomsPerFloor = 10)
        {
            if (lastRoomNumber is null)
                return baseFloor * 100; 

            int nextNumber = lastRoomNumber.Value + 1;

            if (nextNumber % 100 > roomsPerFloor)
                nextNumber = ((nextNumber / 100) + 1) * 100;

            return nextNumber;
        }   
    }
}
