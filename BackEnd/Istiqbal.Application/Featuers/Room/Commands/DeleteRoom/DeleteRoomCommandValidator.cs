using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Room.Commands.DeleteRoom
{
    public sealed class DeleteRoomCommandValidator :AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Room ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Room ID must be a valid GUID.");
        }
    }
}
