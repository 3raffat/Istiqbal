

using Istiqbal.Contracts.Requests.Rooms;
using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed record UpdateRoomCommand(Guid? id, Guid roomTypeId, RoomStatus roomStatus) : IRequest<Result<Updated>>;
    
    
}
