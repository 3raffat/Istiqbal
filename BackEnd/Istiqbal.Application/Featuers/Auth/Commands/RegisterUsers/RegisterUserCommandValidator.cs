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
            RuleFor(x=>x.email).NotEmpty();
            RuleFor(x => x.username).NotEmpty();
            RuleFor(x => x.password).NotEmpty();

        }
    }
}
