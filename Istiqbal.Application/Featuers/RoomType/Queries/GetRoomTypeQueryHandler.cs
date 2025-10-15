using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Application.Featuers.RoomType.Mappers;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Istiqbal.Application.Featuers.RoomTypes.Queries
{
    public sealed class GetRoomTypeQueryHandler
        (IAppDbContext _context) : IRequestHandler<GetRoomTypeQuery, Result<List<RoomTypeDto>>>
    {
        public async Task<Result<List<RoomTypeDto>>> Handle(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var roomTypes = await _context.RoomTypes.Where(x=>!x.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);

            return roomTypes.ToDtos();
        }
    }
}
