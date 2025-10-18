using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.RoomTypes.Commands.CreateRoomType
{
    public sealed record CreateRoomTypeCommand(string Name, string Description, decimal PricePerNight, int MaxOccupancy, IEnumerable<Guid> AmenitieIds) :
        IRequest<Result<RoomTypeDto>>;

}
