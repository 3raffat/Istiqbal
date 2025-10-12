using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Istiqbal.Application.Featuers.Identity.Dtos
{
    public sealed record AppUserDto(string userId,string userEmail,List<string>Roles,List<Claim> Claims);
}
