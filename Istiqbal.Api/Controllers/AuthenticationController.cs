using Istiqbal.Application.Featuers.Auth.Commands.LoginUsers;
using Istiqbal.Application.Featuers.Auth.Commands.RegisterUsers;
using Istiqbal.Contracts.Requests.Auth;
using Istiqbal.Domain.Common.Results;
using Istiqbal.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Istiqbal.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthenticationController(ISender _sender) :ApiController
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new LoginUserCommand(
                    request.email
                  ,request.password)
                , cancellationToken);

            return result.Match(
                 response => Ok(response),
                 Problem);
        }
        [HttpPost("register")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new RegisterUserCommand(
                    request.username,
                    request.email
                  , request.password)
                , cancellationToken);

            return result.Match(
                 response => Ok(response),
                 Problem);
        }

    }
}
