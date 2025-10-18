using Istiqbal.Domain.Auth;
using Istiqbal.Infrastructure.Auth;
using Istiqbal.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Istiqbal.Infrastructure.Data
{
    public sealed class ApplicationDbContextInitialiser(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<ApplicationDbContextInitialiser> logger)
    {
        private readonly AppDbContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ILogger<ApplicationDbContextInitialiser> _logger = logger;

        public async Task InitialiseAsync()
        {
            try
            {
                _context.Database.EnsureCreated();
            }
            catch (Exception ex)

            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
        public async Task TrySeedAsync()
        {
            var adminRole = new IdentityRole(nameof(Role.Admin));

            if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await _roleManager.CreateAsync(adminRole);
            }

            var admin = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            if (_userManager.Users.All(u => u.Email != admin.Email))
            {
                await _userManager.CreateAsync(admin, "Admin123$");
                await _userManager.AddToRoleAsync(admin, adminRole.Name!);
            }

            var guestRole = new IdentityRole(nameof(Role.Guest));

            if (_roleManager.Roles.All(r => r.Name != guestRole.Name))
            {
                await _roleManager.CreateAsync(guestRole);
            }

            var ReceptionistRole = new IdentityRole(nameof(Role.Receptionist));

            if (_roleManager.Roles.All(r => r.Name != ReceptionistRole.Name))
            {
                await _roleManager.CreateAsync(ReceptionistRole);
            }
        }
    }
}
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}