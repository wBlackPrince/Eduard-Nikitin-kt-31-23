using EduardNikitinKT_31_23.db;
using EduardNikitinKT_31_23.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Logger logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

builder.Logging.ClearProviders();
builder.Host.UseNLog();

try
{
    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<StudentDbContext>(
        options => options
            .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "API v1");
        });
    }

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
