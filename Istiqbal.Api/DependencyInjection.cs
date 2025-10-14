
using Istiqbal.Api.OpenApi;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Istiqbal.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddCustomApiVersioning()
                .AddApiDocumentation();
            return services;
        }

        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            
            return services;
        }

        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            string[] versions = ["v1"];
            foreach (var version in versions)
            {
                services.AddOpenApi(version, _options =>
                {
                    //Versioning config
                    _options.AddDocumentTransformer<VersionInfoTransformer>();

                    //Security Scheme config

                    _options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                    _options.AddOperationTransformer<BearerSecuritySchemeTransformer>();

                });
            }
            return services;
        }
    }
}
