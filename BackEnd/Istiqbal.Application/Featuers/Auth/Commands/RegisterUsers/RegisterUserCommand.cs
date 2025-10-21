

using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.Auth.Commands.RegisterUsers
{
    public sealed record  RegisterUserCommand(string UserName , string Email, string Password):IRequest<Result<Success>>;
   
}
