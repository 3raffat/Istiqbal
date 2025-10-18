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
                .Matches(@"^\+[1-9]\d{1,3}\d{6,12}$")
                .WithMessage("Invalid international phone number format. Use + followed by country code and number (e.g., +962787654321).");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

        }
    }
}
