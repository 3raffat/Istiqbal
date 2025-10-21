using Istiqbal.Application.Common.Caching;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Reservations.Queries
{
    public sealed class GetReservationQuery : ICachedQuery<Result<List<ReservationDto>>>
    {
        public string CacheKey => CacheKeys.Reservation.All;

        public string[] Tags => [CacheKeys.Reservation.All];

        public TimeSpan? Expiration => CacheKeys.Expiration;
    }
}
