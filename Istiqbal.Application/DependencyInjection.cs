using FluentValidation;
using Istiqbal.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Istiqbal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(CachingBehavior<,>));
            });

            return services;
        }
    }
}
