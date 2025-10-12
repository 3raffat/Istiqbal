using Istiqbal.Application.Featuers.Identity.Dtos;
using Istiqbal.Application.Featuers.Identity.Queries;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Common.Interface
{
    public interface ITokenProvider
    {
        Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

       
    }
}
