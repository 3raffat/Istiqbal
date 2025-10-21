using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomType.Dtos;
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
        public string CacheKey => CacheKeys.RoomType.All;

        public string[] Tags => [CacheKeys.RoomType.All];

        public TimeSpan? Expiration => CacheKeys.Expiration;
    }
}
