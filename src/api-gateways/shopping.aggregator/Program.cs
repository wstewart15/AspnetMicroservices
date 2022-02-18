using Common.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using shopping.aggregator.Policies;
using shopping.aggregator.Services;
using shopping.aggregator.Services.impl;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

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
    services.AddTransient<LoggingDelegatingHandler>();

    services.AddHttpClient<ICatalogService, CatalogService>(c =>
       c.BaseAddress = new Uri(configuration["ApiSettings:CatalogUrl"]))
        .AddHttpMessageHandler<LoggingDelegatingHandler>()
        .AddPolicyHandler(Policies.RetryPolicy())
        .AddPolicyHandler(Policies.CircuitBreakerPolicy());

    services.AddHttpClient<IBasketService, BasketService>(c =>
       c.BaseAddress = new Uri(configuration["ApiSettings:BasketUrl"]))
        .AddHttpMessageHandler<LoggingDelegatingHandler>()
        .AddPolicyHandler(Policies.RetryPolicy())
        .AddPolicyHandler(Policies.CircuitBreakerPolicy());

    services.AddHttpClient<IOrderService, OrderService>(c =>
        c.BaseAddress = new Uri(configuration["ApiSettings:OrderingUrl"]))
        .AddHttpMessageHandler<LoggingDelegatingHandler>()
        .AddPolicyHandler(Policies.RetryPolicy())
        .AddPolicyHandler(Policies.CircuitBreakerPolicy());

    services.AddControllers();
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddHealthChecks()
        .AddUrlGroup(new Uri($"{configuration["ApiSettings:CatalogUrl"]}/swagger/index.html"), "Catalog.API", HealthStatus.Degraded)
        .AddUrlGroup(new Uri($"{configuration["ApiSettings:BasketUrl"]}/swagger/index.html"), "Basket.API", HealthStatus.Degraded)
        .AddUrlGroup(new Uri($"{configuration["ApiSettings:OrderingUrl"]}/swagger/index.html"), "Ordering.API", HealthStatus.Degraded);
}

void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env, IServiceProvider services)
{
    // Configure the HTTP request pipeline.
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
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