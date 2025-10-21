

using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Common.Results;
using System.Security.Claims;

namespace Istiqbal.Application.Common.Interface
{
    public interface ITokenProvider
    {
        Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
