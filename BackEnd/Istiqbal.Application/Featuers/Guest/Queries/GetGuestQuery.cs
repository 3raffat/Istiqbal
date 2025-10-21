using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Queries
{
    public sealed class GetGuestQuery : ICachedQuery<Result<List<GuestDto>>>
    {
        public string CacheKey => CacheKeys.Guest.All;

        public string[] Tags => [CacheKeys.Guest.All];

        public TimeSpan? Expiration => CacheKeys.Expiration;
    }
}
