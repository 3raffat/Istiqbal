

using Istiqbal.Domain.Common.Results;
using MediatR;

namespace Istiqbal.Application.Featuers.Auth.Commands.RegisterUsers
{
    public sealed record  RegisterUserCommand(string username , string email, string password):IRequest<Result<Success>>;
   
}
