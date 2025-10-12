using Istiqbal.Application.Featuers.Room.Dtos;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Rooms.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.CreateRoom
{
    public sealed record CreateRoomCommand(Guid roomTypeId):IRequest<Result<RoomDto>>;

    
   
}
