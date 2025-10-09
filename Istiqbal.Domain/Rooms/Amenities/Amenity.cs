using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Rooms.Amenities
{
    public sealed class Amenity : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;

        public readonly List<Room> _rooms = new();
        public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
        private Amenity() { }
        private Amenity(Guid id, string name) : base(id)
        {
            Name = name;
        }
        public static Result<Amenity> Create(Guid id, string name)
        {
            if (id == Guid.Empty)
                return AmenityErrors.AmenityIdRequerd;

            if (string.IsNullOrWhiteSpace(name))
                return AmenityErrors.AmenityNameRequerd;

            return new Amenity(id, name);
        }

        public Result<Updated> Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return AmenityErrors.AmenityNameRequerd;

            Name = name;
            return Result.Updated;
        }
        public Result<Updated> AddRoom(Room room)
        {
            if (room == null)
                return AmenityErrors.AmenityRoomsRequerd;

            if (Rooms.Contains(room))
                return RoomErrors.RoomAmenityIsExist;

            _rooms.Add(room);
            return Result.Updated;
        }
        public Result<Deleted> RemoveRoom(Room room)
        {
            if (room == null)
                return AmenityErrors.AmenityRoomsRequerd;

            if (!Rooms.Contains(room))
                return RoomErrors.RoomAmenityNotFound;

            _rooms.Remove(room);
            return Result.Deleted;
        }
    }
}
