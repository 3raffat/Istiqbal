using Istiqbal.Application.Common.Interface;
using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.Auth.Commands.RegisterUsers
{
    public sealed class RegisterUserCommandHandler(IUserService _userService) : IRequestHandler<RegisterUserCommand, Result<Success>>
    {
        public async Task<Result<Success>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.Register(request.username,request.email,request.password, cancellationToken);
        }
    }
}
