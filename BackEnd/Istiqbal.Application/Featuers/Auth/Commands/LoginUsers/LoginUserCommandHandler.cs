using Istiqbal.Application.Common.Interface;
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
    public sealed class LoginUserCommandHandler(IUserService _userService) : IRequestHandler<LoginUserCommand, Result<LoginUserDto>>
    {
       
        public async Task<Result<LoginUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.LoginUser(request.Email,request.Password);
        }
    }
}
