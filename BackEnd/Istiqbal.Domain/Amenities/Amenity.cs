
using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.RoomTypes;
using Istiqbal.Domain.RoomTypes.Rooms;

namespace Istiqbal.Domain.Amenities
{
    public sealed class Amenity : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        private List<RoomType> _types = new();
        public IEnumerable<RoomType> Types => _types.AsReadOnly();
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
    }
}
