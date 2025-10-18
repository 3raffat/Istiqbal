using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Istiqbal.Infrastructure.Auth
{
    public sealed class UserService(UserManager<AppUser> _manager,ITokenProvider _token) : IUserService
    {
        public async Task<Result<LoginUserDto>> LoginUser(string email, string password ,CancellationToken cancellationToken)
        {
            var user = await _manager.FindByEmailAsync(email);

            if (user is null || !await _manager.CheckPasswordAsync(user, password))
                return Error.Unauthorized("Invalid_Login_Attempt", "Email or password is incorrect");

           var userInfo = await GetUserInfoAsync(user);

           var token = await _token.GenerateJwtTokenAsync(userInfo,cancellationToken);

            return new LoginUserDto(user.Email!, token);
        }
        private async Task<AppUserDto> GetUserInfoAsync(AppUser user)
        {

            var roles = await _manager.GetRolesAsync(user);

            var claims = await _manager.GetClaimsAsync(user);

            return new AppUserDto(user.Id, user.Email!, roles, claims);
        }

        public async Task<Result<Success>> Register(string username, string email, string password,CancellationToken cancellationToken)
        {
            var existUser = await _manager.Users.
                AnyAsync(x=>x.UserName == username || x.Email==email,cancellationToken);

            if(existUser)
            {
                return Error.Conflict("User_Already_Exists", "A user with the same username or email already exists");
            }
            AppUser user = new AppUser {UserName = username,Email = email};
            
           var createResult =  await _manager.CreateAsync(user,password);

            if (!createResult.Succeeded)
                return Error.Failure("User_Creation_Failed",  $"Failed to create user");

           var roleResult =  await _manager.AddToRoleAsync(user,nameof(Role.Receptionist));

            if (!roleResult.Succeeded)
                return Error.Failure("Role_Assignment_Failed", $"Failed to assign role");


            return Result.Success;
        }
    }
}
