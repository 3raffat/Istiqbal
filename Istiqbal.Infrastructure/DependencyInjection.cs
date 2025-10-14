using Istiqbal.Application.Common.Interface;
using Istiqbal.Infrastructure.Auth;
using Istiqbal.Infrastructure.Data;
using Istiqbal.Infrastructure.Data.Interceptors;
using Istiqbal.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Istiqbal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(TimeProvider.System);

            services.AddDbContext<AppDbContext>((sp, _options) =>
            {
                _options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                _options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }).AddIdentityCore<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();;


         

            services.AddAuthentication(_options =>
            {
                _options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                _options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(_options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings");

                _options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,  
                    ValidateAudience = true,    
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero, // Eliminate delay of token when expire
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
                };
            });



            services.AddHybridCache(options => options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10), // L2, L3
                LocalCacheExpiration = TimeSpan.FromSeconds(30), // L1
            });

            services.AddScoped<ApplicationDbContextInitialiser>();


            services.AddScoped<ITokenProvider, TokenProvider>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            return services;
        }
    }
}
