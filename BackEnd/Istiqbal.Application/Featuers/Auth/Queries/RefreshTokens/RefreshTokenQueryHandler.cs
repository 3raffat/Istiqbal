using Istiqbal.Application.Common.Errors;
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Auth.Queries.RefreshTokens
{
    public sealed class RefreshTokenQueryHandler
        (IAppDbContext _context,ITokenProvider _provider , IUserService _userService, ILogger<RefreshTokenQueryHandler> _logger)
        : IRequestHandler<RefreshTokenQuery, Result<TokenResponse>>
    {
        public async Task<Result<TokenResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var principal = _provider.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);

            if (principal is null)
            {
                _logger.LogError("Expired access token is not valid");

                return ApplicationErrors.ExpiredAccessTokenInvalid;
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
            {
                _logger.LogError("Invalid userId claim");

                return ApplicationErrors.UserIdClaimInvalid;
            }

            var getUserResult = await _userService.GetUserByIdAsync(userId);

            if (getUserResult.IsError)
            {
                _logger.LogError("Get user by id error occurred: {ErrorDescription}", getUserResult.TopError.Description);
                return getUserResult.Errors;
            }

            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken && r.UserId == userId, cancellationToken);

            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                _logger.LogError("Refresh token has expired");

                return ApplicationErrors.RefreshTokenExpired;
            }

            var generateTokenResult = await _provider.GenerateJwtTokenAsync(getUserResult.Value, cancellationToken);

            if (generateTokenResult.IsError)
            {
                _logger.LogError("Generate token error occurred: {ErrorDescription}", generateTokenResult.TopError.Description);

                return generateTokenResult.Errors;
            }
            return generateTokenResult.Value;
        }
    }
}
