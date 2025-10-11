using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Common.Results.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomType.Commands.UpdateRoomType
{
    public sealed class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, IResult<Updated>>
    {
        public Task<IResult<Updated>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
