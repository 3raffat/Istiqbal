

using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed record UpdateRoomCommand(Guid? Id, Guid RoomTypeId, RoomStatus RoomStatus) : IRequest<Result<RoomDto>>;
    
    
}
