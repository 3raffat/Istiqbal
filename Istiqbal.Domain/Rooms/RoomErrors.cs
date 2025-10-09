using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Rooms
{
    public static class RoomErrors
    {

        public static Error RoomTypeIdRequerd => Error.Validation(
            code: "Room.RoomTypeId.Required",
            description: "Room type ID cannot be empty."
        );
        public static Error RoomNumberRequerd => Error.Validation(
            code: "Room.RoomNumber.Required",
            description: "Room number cannot be empty."
        );
        public static Error RoomStatusInvalid => Error.Validation(
            code: "Room.RoomStatus.Invalid",
            description: "Room status is invalid."
        );
        public static Error RoomAmenitiesRequerd => Error.Validation(
            code: "Room.RoomAmenities.Required",
            description: "Room must have at least one amenity."
        );
        public static Error RoomIdRequerd => Error.Validation(
            code: "Room.RoomId.Required",
            description: "Room ID cannot be empty."
        );
        public static Error RoomAmenityIsExist => Error.Conflict(
            code: "Room.RoomAmenity.IsExist",
            description: "The room already has this amenity."
        );
        public static Error RoomAmenityNotFound => Error.NotFound(
            code: "Room.RoomAmenity.NotFound",
            description: "The room does not have this amenity."
        );
    }
}
