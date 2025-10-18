using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Guest.Dtos;
using Istiqbal.Application.Featuers.Guest.Mapper;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Queries
{
    internal class GetGuestQueryHandler(IAppDbContext _context) : IRequestHandler<GetGuestQuery,Result<List<GuestDto>>>
    {
        public async Task <Result<List<GuestDto>>> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
           var guests = await _context.Guests.AsNoTracking().ToListAsync(cancellationToken);

            return guests.ToDtos();
        }
    }
}
