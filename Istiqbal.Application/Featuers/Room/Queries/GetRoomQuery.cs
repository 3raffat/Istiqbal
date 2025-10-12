using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Queries
{
    public sealed class GetRoomQuery : ICachedQuery<RoomDto>
    {
        public string CacheKey => "rooms";

        public string[] Tags => ["room"];
        
        public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
    }
}
