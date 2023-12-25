using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration
//     .SetBasePath(Directory.GetCurrentDirectory())
//     .AddJsonFile("published_api/appsettings.json", optional: false, reloadOnChange: true)
//     .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedPassenger(context);
    await Seed.SeedDriver(context);
    await Seed.SeedPoint(context);
    await Seed.SeedInterestPoints(context);
    await Seed.SeedRoute(context);
    await Seed.SeedFavoritePoint(context);
    await Seed.SeedOTP(context);
}
catch (Exception ex)
{
    var logger = services?.GetService<ILogger<Program>>();
    logger?.LogError(ex, "An Error Occurred During Migration");
}

app.Run("http://localhost:5002");