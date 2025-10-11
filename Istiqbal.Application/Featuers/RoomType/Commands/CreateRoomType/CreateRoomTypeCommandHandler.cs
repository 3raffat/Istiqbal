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
    public sealed class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, IResult<RoomTypeDto>>
    {
        public Task<IResult<RoomTypeDto>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
