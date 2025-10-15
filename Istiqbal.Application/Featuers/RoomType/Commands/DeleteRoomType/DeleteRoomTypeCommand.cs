using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomTypes.Commands.DeleteRoomType
{
    public sealed record DeleteRoomTypeCommand(Guid Id):IRequest<Result<Deleted>>;
    
    
}
