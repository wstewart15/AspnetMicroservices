var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app, app.Environment, app.Services);
ConfigureEndpoints(app, app.Services);


app.Run();


void ConfigureConfiguration(ConfigurationManager configuration)
{
}

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllersWithViews();
    services.AddHealthChecksUI()
     .AddInMemoryStorage();

}

void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env, IServiceProvider services)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services)
{
    app.MapHealthChecksUI();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}