using catalog.api.Data;
using catalog.api.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app, app.Environment, app.Services);
ConfigureEndpoints(app, app.Services);

app.Run();


//Local Configuration methods
void ConfigureConfiguration(ConfigurationManager configuration)
{
}
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped<ICatalogContext, CatalogContext>();
    services.AddScoped<IProductRepository, ProductRepository>();

    services.AddControllers();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "catalog.api", Version = "v1" });
    });

    services.AddHealthChecks()
        .AddMongoDb(configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);

}
void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env, IServiceProvider services)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "catalog.api v1"));
    }

    app.UseAuthorization();
}
void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
{
    app.MapControllers();
    app.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
}