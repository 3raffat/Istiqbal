using Istiqbal.Application.Featuers.RoomType.Dtos;
using Istiqbal.Domain.Common.Results.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomType.Commands.CreateRoomType
{
    public sealed record CreateRoomTypeCommand(string Name, string Description, decimal PricePerNight, int MaxOccupancy):
        IRequest<IResult<RoomTypeDto>>;

}
