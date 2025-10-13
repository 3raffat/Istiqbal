using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.UpdateGuest
{
    public sealed class UpdateGuestCommandValidator :AbstractValidator<UpdateGuestCommand>
    {
        public UpdateGuestCommandValidator()
        {
            RuleFor(x => x.id)
             .NotEmpty().WithMessage("Guest Id is required.");

            RuleFor(x => x.fullName)
                .NotEmpty().WithMessage("Guest Name is required.");

            RuleFor(x => x.phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+[1-9]\d{4,14}$").WithMessage("Phone number must be in E.164 format.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
