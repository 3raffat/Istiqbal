using Istiqbal.Application.Featuers.Identity.Queries;


namespace Istiqbal.Application.Featuers.Auth.Dtos
{
    public sealed record LoginUserDto(string email, TokenResponse token);
   
}
