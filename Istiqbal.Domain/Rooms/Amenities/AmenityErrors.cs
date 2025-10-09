using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Domain.Rooms.Amenities
{
    public static class AmenityErrors
    {
        public static Error AmenityIdRequerd => Error.Validation(
            code: "Amenity.AmenityId.Required",
            description: "Amenity ID cannot be empty."
        );
        public static Error AmenityNameRequerd => Error.Validation(
            code: "Amenity.AmenityName.Required",
            description: "Amenity name cannot be empty."
        );
        public static Error AmenityIdNotFound => Error.NotFound(
            code: "Amenity.AmenityId.NotFound",
            description: "Amenity with the specified ID was not found."
        );
        public static Error AmenityInUse => Error.Conflict(
            code: "Amenity.Amenity.InUse",
            description: "Amenity is in use and cannot be deleted."
        );
        public static Error AmenityRoomsRequerd => Error.Validation(
            code: "Amenity.AmenityRooms.Required",
            description: "Amenity must be associated with at least one room."
        );
    }
}
