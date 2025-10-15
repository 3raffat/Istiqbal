

using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Common.Results;

namespace Istiqbal.Application.Common.Interface
{
    public interface ITokenProvider
    {
        Task<TokenResponse> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

       
    }
}
