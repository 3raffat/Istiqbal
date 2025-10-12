using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.CreateRoom
{
    public sealed class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
           RuleFor(x=>x.roomTypeId)
                .NotEmpty()
                .WithMessage("Room Type Id is required");

        }
    }
}
