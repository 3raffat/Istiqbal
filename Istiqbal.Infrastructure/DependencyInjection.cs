using Istiqbal.Infrastructure.Data;
using Istiqbal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Istiqbal.Infrastructure
{
    public static class DependencyInjection
    {
public static IServiceCollection AddInfrastructure(this IServiceCollection services, string Connection)
        {
             services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(Connection));
            return services;
        }
    }
}
