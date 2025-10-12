using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomTypes.Dtos;
using Istiqbal.Application.Featuers.RoomTypes.Mappers;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Istiqbal.Application.Featuers.RoomTypes.Queries
{
    public sealed class GetRoomTypeQueryHandler(IAppDbContext _context) : IRequestHandler<GetRoomTypeQuery, Result<List<RoomTypeDto>>>
    {
        public async Task<Result<List<RoomTypeDto>>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var roomTypes = await _context.RoomTypes.AsNoTracking().ToListAsync(cancellationToken);

            return roomTypes.ToDtos();
        }
    }
}
