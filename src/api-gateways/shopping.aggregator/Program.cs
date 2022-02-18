using Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using shopping.aggregator.Policies;
using shopping.aggregator.Services;
using shopping.aggregator.Services.impl;



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
}