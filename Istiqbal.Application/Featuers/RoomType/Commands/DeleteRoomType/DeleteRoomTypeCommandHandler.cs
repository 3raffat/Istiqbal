using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomType.Commands.DeleteRoomType
{
    public sealed class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, IResult<Deleted>>
    {
        public Task<IResult<Deleted>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
