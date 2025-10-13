using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Guest.Commands.CreateGuest
{
    public sealed class CreateGuestCommandValidator :AbstractValidator<CreateGuestCommand>  
    {
        public CreateGuestCommandValidator()
        {
           
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+[1-9]\d{4,14}$").WithMessage("Phone number must be in E.164 format (e.g., +1234567890).");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

        }
    }
}
