using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Rooms.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.UpdateRoom
{
    public sealed record UpdateRoomCommand(Guid? id, Guid roomTypeId, List<UpdateAmenityCommand> amenities, RoomStatus roomStatus) : IRequest<Result<Updated>>;
    
    
}
