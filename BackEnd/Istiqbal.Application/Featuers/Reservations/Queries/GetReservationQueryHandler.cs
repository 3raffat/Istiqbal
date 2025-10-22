using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Application.Featuers.Reservations.Mapper;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Istiqbal.Contracts.Requests.Reservation.CreateReservationRequest;

namespace Istiqbal.Application.Featuers.Reservations.Queries
{
    public sealed class GetReservationQueryHandler(IAppDbContext _context) : IRequestHandler<GetReservationQuery, Result<List<ReservationDto>>>
    {
        public async Task<Result<List<ReservationDto>>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .Where(x=>x.Status!= ReservationStatus.Cancelled && !x.IsDeleted && !x.Guest.IsDeleted && !x.Room.IsDeleted)
                .Include(x=>x.Room).ThenInclude(x=>x.Type)
                .Include(x=>x.Guest).ToListAsync(cancellationToken);

            return reservation.toDtos();
        }
    }
}
