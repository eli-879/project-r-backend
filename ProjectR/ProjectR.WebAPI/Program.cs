using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using ProjectR.Application;
using ProjectR.Infrastructure;
using ProjectR.Presentation;
using ProjectR.WebAPI.OptionsSetup;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Logging.AddDebug();

    builder.Host.UseNLog();

    builder.Services
        .AddApplication()
        .AddInfrastructure()
        .AddPresentation();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer();

    builder.Services.ConfigureOptions<JwtOptionsSetup>();
    builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"))
        );

    var app = builder.Build();



    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();



    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}


