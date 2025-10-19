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
    public class GetReservationQuery : ICachedQuery<Result<List<ReservationDto>>>
    {
        public string CacheKey => "reservation";

        public string[] Tags => ["reservation"];

        public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
    }
}
