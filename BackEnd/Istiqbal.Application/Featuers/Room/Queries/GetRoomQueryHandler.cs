using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Application.Featuers.Room.Mappers;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Queries
{
    public sealed class GetRoomQueryHandler(IAppDbContext _context) : IRequestHandler<GetRoomQuery, Result<List<RoomDto>>>
    {
        public async Task<Result<List<RoomDto>>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.Include(x=>x.Type).AsNoTracking().ToListAsync(cancellationToken);

            return room.ToDtos();
        }
    }
}
