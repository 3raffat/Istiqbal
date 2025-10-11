using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.RoomType.Commands.DeleteRoomType
{
    public sealed class DeleteRoomTypeCommandValidator :AbstractValidator<DeleteRoomTypeCommand>
    {
        public DeleteRoomTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Room type ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Room type ID must be a valid GUID.");
        }
    }
}
