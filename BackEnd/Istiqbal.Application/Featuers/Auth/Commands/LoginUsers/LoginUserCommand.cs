using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Auth.Commands.LoginUsers
{
    public sealed record  LoginUserCommand(string email, string password):IRequest<Result<LoginUserDto>>;
    
}

