using Istiqbal.Api;
using Istiqbal.Api.Extension;
using Istiqbal.Application;
using Istiqbal.Infrastructure;
using Istiqbal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();


builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Istiqbal API V1");

        options.EnableDeepLinking();
        options.DisplayRequestDuration();
        options.EnableFilter();
    });
    await app.InitialiseDatabaseAsync();
}
using (var scope = app.Services.CreateScope())
{
    var httpContextAccessor = scope.ServiceProvider
        .GetRequiredService<IHttpContextAccessor>();
    ErrorExtensions.Initialize(httpContextAccessor);
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
