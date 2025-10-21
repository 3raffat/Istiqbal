using Istiqbal.Api.Extension;
using Istiqbal.Application.Featuers.Auth.Commands.LoginUsers;
using Istiqbal.Application.Featuers.Auth.Commands.RegisterUsers;
using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Application.Featuers.Auth.Queries.RefreshTokens;
using Istiqbal.Application.Featuers.Reservations.Dtos;
using Istiqbal.Contracts.Requests.Auth;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Istiqbal.Application.Common.Responses.StandardResponse;

namespace Istiqbal.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthenticationController(ISender _sender) : ControllerBase
    {


        [HttpPost("login")]
        [AllowAnonymous]
        [EndpointName("LoginUser")]
        [EndpointDescription("Authenticates an employee using email and password and returns a login token.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<LoginUserDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new LoginUserCommand(request.Email, request.Password),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<LoginUserDto>(result.Value,
                                                                StatusCodes.Status200OK,
                                                                "Employee logged in successfully"));
        }

        [HttpPost("register")]
        [Authorize(Roles = nameof(Role.Admin))]
        [EndpointName("RegisterUser")]
        [EndpointDescription("Registers a new employee with username, email, and password. Admin access required.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<object>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new RegisterUserCommand(request.UserName, request.Email, request.Password),
                cancellationToken);

            if (result.IsError)
                return result.TopError.ToActionResult();

            return Ok(new StandardSuccessResponse<object>(null,
                                                          StatusCodes.Status200OK,
                                                          "Employee registered successfully"));
        }
        [HttpPost("refresh-token")]
        [EndpointDescription("Use this endpoint to refresh your expired access token using a valid refresh token.")]
        [ProducesResponseType(typeof(StandardSuccessResponse<TokenResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenQuery query ,CancellationToken cancellationToken)
        {
            var result = await _sender.Send(query,cancellationToken);

            if(result.IsError)
                return result.TopError.ToActionResult();

            return StatusCode(StatusCodes.Status201Created,new StandardSuccessResponse<TokenResponse>(result.Value,
                                                                                                      StatusCodes.Status201Created,
                                                                                                      "Refresh token generated successfully"));
        }
    }
}