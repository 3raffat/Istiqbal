using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Rooms.RoomTypes
{
    public static class RoomTypeErrors
    {
        public static Error RoomTypeIdRequerd => Error.Validation(
            code: "RoomType.RoomTypeId.Required",
            description: "Room type ID cannot be empty."
        );
        public static Error RoomTypeNameRequerd => Error.Validation(
            code: "RoomType.RoomTypeName.Required",
            description: "Room type name cannot be empty."
        );
        public static Error RoomTypeDescriptionRequerd => Error.Validation(
            code: "RoomType.RoomTypeDescription.Required",
            description: "Room type description cannot be empty."
        );
        public static Error RoomTypePricePerNightInvalid => Error.Validation(
            code: "RoomType.RoomTypePricePerNight.Invalid",
            description: "Room type price per night must be greater than zero."
        );
        public static Error RoomTypeMaxOccupancyInvalid => Error.Validation(
            code: "RoomType.RoomTypeMaxOccupancy.Invalid",
            description: "Room type max occupancy must be greater than zero."
        );
        public static Error RoomTypeIdNotFound => Error.NotFound(
            code: "RoomType.RoomTypeId.NotFound",
            description: "Room type with the specified ID was not found."
        );

        public static Error RoomTypeNameAlreadyExists => Error.Conflict(
            code: "RoomType.RoomTypeName.AlreadyExists",
            description: "A room type with the specified name already exists."
        );

        public static Error RoomTypeNotFound => Error.NotFound(
            code: "RoomType.NotFound",
            description: "Room type not found."
        );
    }
}
