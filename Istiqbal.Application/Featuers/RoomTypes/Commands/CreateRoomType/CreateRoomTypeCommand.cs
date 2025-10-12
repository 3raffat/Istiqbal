using Istiqbal.Application.Featuers.RoomTypes.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.RoomTypes.Commands.CreateRoomType
{
    public sealed record CreateRoomTypeCommand(string Name, string Description, decimal PricePerNight, int MaxOccupancy):
        IRequest<Result<RoomTypeDto>>;

}
