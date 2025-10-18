using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.DeleteGuest
{
    public sealed class DeleteGuestCommandValidator :AbstractValidator<DeleteGuestCommand>
    {
        public DeleteGuestCommandValidator()
        {
           RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Guest Id is required.");

        }
    }
}
