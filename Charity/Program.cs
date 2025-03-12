using Charity.API.Hubs;
using Charity.Application.Extensions;
using Charity.Application.MiddleWares;
using Charity.Contracts.Repositories;
using Charity.Infrastructure.Extensions;
using Charity.Services.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// add Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddInfrastructureDependencies(builder.Configuration)
    .AddApplicationDependencies()
    .AddServicesDependencies(builder.Configuration);

// Enable multi-version support
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// add localization settings
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG")
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "ar-EG", uiCulture: "ar-EG");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.ApplyCurrentCultureToResponseHeaders = true;
});

//Add Permissions Policy

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(Permissions.Categories.Create, policy => policy.RequireClaim("Permission", Permissions.Categories.Create));
//    options.AddPolicy(Permissions.Categories.View, policy => policy.RequireClaim("Permission", Permissions.Categories.View));
//    options.AddPolicy(Permissions.Categories.Edit, policy => policy.RequireClaim("Permission", Permissions.Categories.Edit));
//    options.AddPolicy(Permissions.Categories.Delete, policy => policy.RequireClaim("Permission", Permissions.Categories.Delete));
//});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigins");

//Data Seeder 
await using (var scop = app.Services.CreateAsyncScope())
{
    var services = scop.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("app");
    try
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();

    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "An error occurred seeding the DB");
    }
}

app.UseMiddleware<ErrorHandlingMiddleWare>();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
