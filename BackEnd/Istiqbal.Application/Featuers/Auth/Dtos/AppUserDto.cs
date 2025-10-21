using System.Security.Claims;

namespace Istiqbal.Application.Featuers.Auth.Dtos
{
    public sealed record AppUserDto(string UserId,string UserEmail,IList<string>Roles,IList<Claim> Claims);
}
