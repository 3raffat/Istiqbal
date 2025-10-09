using Istiqbal.Domain.Common;
using Istiqbal.Domain.Common.Results;


namespace Istiqbal.Domain.Rooms.RoomTypes
{
    public sealed class RoomType :AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal PricePerNight { get; private set; }
        public short MaxOccupancy { get; private set; }
        private RoomType() { }
        private RoomType(Guid id, string name, string description, decimal pricePerNight, short maxOccupancy) : base(id)
        {
            Name = name;
            Description = description;
            PricePerNight = pricePerNight;
            MaxOccupancy = maxOccupancy;
        }
        public static Result<RoomType> Create(Guid id, string name, string description, decimal pricePerNight, short maxOccupancy)
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

            return new RoomType(id, name, description, pricePerNight, maxOccupancy);
        }
        public Result<Updated> Update( string name, string description, decimal pricePerNight, short maxOccupancy)
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
            return Result.Updated;
        }
    }
}
