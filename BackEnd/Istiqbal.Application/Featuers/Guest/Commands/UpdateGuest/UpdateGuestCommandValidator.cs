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
                .Matches(@"^\+[1-9]\d{1,3}\d{6,12}$")
                .WithMessage("Invalid international phone number format. Use + followed by country code and number (e.g., +962787654321).");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
