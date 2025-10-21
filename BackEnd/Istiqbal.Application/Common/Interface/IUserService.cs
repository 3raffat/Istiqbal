using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Common.Interface
{
    public interface  IUserService
    {
        Task<Result<LoginUserDto>> LoginUser(string email, string password,CancellationToken cancellationToken= default);
        Task<Result<Success>> Register(string username,string email, string password,CancellationToken cancellationToken = default);
        Task<Result<AppUserDto>> GetUserByIdAsync(string userId);

    }
}
