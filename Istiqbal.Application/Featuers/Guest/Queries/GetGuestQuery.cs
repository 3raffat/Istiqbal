using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Queries
{
    public sealed class GetGuestQuery : ICachedQuery<List<GuestDto>>
    {
        public string CacheKey => "guests";

        public string[] Tags => ["guest"];

        public TimeSpan? Expiration => TimeSpan.FromMinutes(20);
    }
}
