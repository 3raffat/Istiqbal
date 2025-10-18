using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Auth.Commands.LoginUsers
{
    public sealed class LoginUserCommandValidator :AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x=>x.email)
                .NotEmpty();

            RuleFor(x=>x.password)
                .NotEmpty();
        }
    }
}
