using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomTypes.Dtos;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomTypes.Queries
{
    public sealed record GetRoomTypeQuery : ICachedQuery<Result<List<RoomTypeDto>>>
    {
        public string CacheKey => "roomTypes";

        public string[] Tags => ["roomType"];

        public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
    }
}
