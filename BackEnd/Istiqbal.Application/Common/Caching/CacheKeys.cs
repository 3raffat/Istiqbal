using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Common.Caching
{
    public static class CacheKeys
    {
        public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(20);

        public static readonly TimeSpan LongExpiration = TimeSpan.FromHours(1);

        public static readonly TimeSpan ShortExpiration = TimeSpan.FromMinutes(5);

        public sealed record Amenity
        {
            public const string All = "Amenity-All";
        }
        public sealed record Guest
        {
            public const string All = "Guest-All";
        }
        public sealed record Reservation
        {
            public const string All = "Reservation-All";
        }
        public sealed record Room
        {
            public const string All = "Room-All";
        }
        public sealed record RoomType
        {
            public const string All = "RoomType-All";
        }
    }

}
