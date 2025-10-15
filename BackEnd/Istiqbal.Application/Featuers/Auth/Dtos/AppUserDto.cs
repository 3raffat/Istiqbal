using System.Security.Claims;

namespace Istiqbal.Application.Featuers.Auth.Dtos
{
    public sealed record AppUserDto(string userId,string userEmail,IList<string>Roles,IList<Claim> Claims);
}
