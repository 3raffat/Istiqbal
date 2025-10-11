using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Istiqbal.Application.Featuers.RoomType.Commands.UpdateRoomType
{
    public sealed record  UpdateRoomTypeCommand(Guid Id,string Name, string Description, decimal PricePerNight, int MaxOccupancy) 
        :IRequest<IResult<Updated>>;
}
