using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Auth.Commands.RegisterUsers
{
    public sealed class RegisterUserCommandValidator :AbstractValidator<RegisterUserCommand>    
    {
        public RegisterUserCommandValidator() 
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format."); 

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters."); 

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.");
        }
    }
}
