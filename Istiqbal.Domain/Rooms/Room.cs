using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Rooms.Amenities;
using Istiqbal.Domain.Rooms.Enums;
using Istiqbal.Domain.Rooms.RoomTypes;

namespace Istiqbal.Domain.Rooms
{
    public sealed class Room : AuditableEntity
    {
        public int Number { get; private set; }
        public Guid RoomTypeId { get; private set; }
        public RoomType Type { get; private set; } = null!;
        public int Floor => Number / 100;
        public RoomStatus Status { get; private set; } = RoomStatus.Available;
        private readonly List<Amenity> _amenities = new();
        public IReadOnlyCollection<Amenity> Amenities => _amenities.AsReadOnly();
        private Room() { }
        private Room(Guid id, Guid roomTypeId, RoomStatus roomStatus) : base(id)
        {
            RoomTypeId = roomTypeId;
            Status = roomStatus;
        }
        public static Result<Room> Create(Guid id, Guid roomTypeId, RoomStatus roomStatus)
        {
            if (id == Guid.Empty)
                return RoomErrors.RoomIdRequerd;

            if (roomTypeId == Guid.Empty)
                return RoomErrors.RoomTypeIdRequerd;

            if (!Enum.IsDefined(roomStatus))
                return RoomErrors.RoomStatusInvalid;

            return new Room(id, roomTypeId, roomStatus);
        }
        public Result<Updated> Update(RoomStatus roomStatus)
        {
            if (!Enum.IsDefined(roomStatus))
                return RoomErrors.RoomStatusInvalid;

            Status = roomStatus;
            return Result.Updated;
        }
        public Result<Updated> AddAmenities(Amenity amenity)
        {
            if (amenity == null)
                return RoomErrors.RoomAmenitiesRequerd;

            if (_amenities.Contains(amenity))
                return RoomErrors.RoomAmenityIsExist;

            _amenities.Add(amenity);
            return Result.Updated;
        }
        public Result<Deleted> RemoveAmenities(Amenity amenity)
        {
            if (amenity == null)
                return RoomErrors.RoomAmenitiesRequerd;

            if (!_amenities.Contains(amenity))
                return RoomErrors.RoomAmenityNotFound;

            _amenities.Remove(amenity);
            return Result.Deleted;
        }
    }
}
