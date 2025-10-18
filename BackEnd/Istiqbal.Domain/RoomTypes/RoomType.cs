using Istiqbal.Domain.Amenities;
using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.RoomTypes.Rooms;
using System.Collections.Generic;

namespace Istiqbal.Domain.RoomTypes
{
    public  class RoomType :AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal PricePerNight { get; private set; }
        public int MaxOccupancy { get; private set; }
        private List<Room> _rooms = new();
        public IEnumerable<Room> Rooms => _rooms.AsReadOnly();
        private  List<Amenity> _amenities = new();
        public IEnumerable<Amenity> Amenities => _amenities.AsReadOnly();
        private RoomType() { }
        private RoomType(Guid id, string name, string description, decimal pricePerNight, int maxOccupancy,List<Amenity> amenities) : base(id)
        {
            Name = name;
            Description = description;
            PricePerNight = pricePerNight;
            MaxOccupancy = maxOccupancy;
            _amenities = amenities;
        }
        public static Result<RoomType> Create(Guid id, string name, string description, decimal pricePerNight, int maxOccupancy, List<Amenity> amenities)
        {
            if (id == Guid.Empty)
                return RoomTypeErrors.RoomTypeIdRequerd;

            if (string.IsNullOrWhiteSpace(name))
                return RoomTypeErrors.RoomTypeNameRequerd;

            if (string.IsNullOrWhiteSpace(description))
                return RoomTypeErrors.RoomTypeDescriptionRequerd;

            if (pricePerNight <= 0)
                return RoomTypeErrors.RoomTypePricePerNightInvalid;

            if (maxOccupancy <= 0)
                return RoomTypeErrors.RoomTypeMaxOccupancyInvalid;

            return new RoomType(id, name, description, pricePerNight, maxOccupancy,amenities);
        }
        public Result<Updated> Update( string name, string description, decimal pricePerNight, int maxOccupancy, List<Amenity> amenities)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RoomTypeErrors.RoomTypeNameRequerd;

            if (string.IsNullOrWhiteSpace(description))
                return RoomTypeErrors.RoomTypeDescriptionRequerd;

            if (pricePerNight <= 0)
                return RoomTypeErrors.RoomTypePricePerNightInvalid;

            if (maxOccupancy <= 0)
                return RoomTypeErrors.RoomTypeMaxOccupancyInvalid;

            Name = name;
            Description = description;
            PricePerNight = pricePerNight;
            MaxOccupancy = maxOccupancy;
            _amenities = amenities;
            return Result.Updated;
        }
    }
}
