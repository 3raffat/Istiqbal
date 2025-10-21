
using Istiqbal.Application.Common.Interface;
using Istiqbal.Application.Featuers.Auth.Dtos;
using Istiqbal.Domain.Auth;
using Istiqbal.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Istiqbal.Infrastructure.Auth
{
    public sealed class TokenProvider(IConfiguration _configuration , IAppDbContext _context) : ITokenProvider
    {
        public async Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default)
        {
            var tokenResult = await GenerateJwt(user, ct);

           
            return tokenResult.Value;
        }
        private async Task<Result<TokenResponse>> GenerateJwt(AppUserDto user, CancellationToken ct = default)
        {
           var JwtSettings = _configuration.GetSection("JwtSettings");

            var Aduience = JwtSettings["Audience"]; 
            var Issuer = JwtSettings["Issuer"];
            var Secret = JwtSettings["Secret"];
            var Expire = DateTime.UtcNow.AddMinutes(int.Parse(JwtSettings["TokenExpirationInMinutes"]));
        
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = Expire,
                Issuer = Issuer,
                Audience = Aduience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)), SecurityAlgorithms.HmacSha256)
            };
             
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(descriptor);

            var oldRefreshTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == user.UserId)
            .ExecuteDeleteAsync(ct);

            var refreshTokenResult = RefreshToken.Create(
                Guid.NewGuid(),
                GenerateRefreshToken(),
                user.UserId,
                DateTime.UtcNow.AddDays(7));

            if (refreshTokenResult.IsError)
            {
                return refreshTokenResult.Errors;
            }

            var refreshToken = refreshTokenResult.Value;

            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync(ct);

            return new TokenResponse
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
                ExpiresOnUtc = Expire
            };
        }

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!)),
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"],
                ValidateLifetime = false, 
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token.");
            }

            return principal;
        }
    }
}
