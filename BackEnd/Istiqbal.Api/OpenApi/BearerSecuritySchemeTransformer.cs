using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Istiqbal.Api.OpenApi
{
    public sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer,IOpenApiOperationTransformer
    {
        private readonly string _schemeId = JwtBearerDefaults.AuthenticationScheme;
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            document.Components ??= new();
            document.Components.SecuritySchemes ??= new Dictionary<string,OpenApiSecurityScheme>();
            document.Components.SecuritySchemes[_schemeId] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat="JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer token",
                Name = "Authorization",
                Reference= new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= _schemeId
                }
            };
                 return Task.CompletedTask;
        }

        public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
        {
            if(context.Description.ActionDescriptor.EndpointMetadata.OfType<IAuthorizeData>().Any())
            {
                operation.Security ??= [];
                var key = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference()
                };
                key.Reference.Type = ReferenceType.SecurityScheme;
                key.Reference.Id = _schemeId;

                var requirement = new OpenApiSecurityRequirement
                {
                       { key, [] },
                };

                operation.Security.Add(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
