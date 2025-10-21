using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.DeleteRoom
{
    public sealed record DeleteRoomCommand(Guid Id):IRequest<Result<Deleted>>;

}
