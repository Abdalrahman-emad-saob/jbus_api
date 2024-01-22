using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// builder.Configuration
//     .SetBasePath(Directory.GetCurrentDirectory())
//     .AddJsonFile("published_api/appsettings.json", optional: false, reloadOnChange: true)
//     .AddEnvironmentVariables();

builder.Services.AddControllers();
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("jbus-8f9bf-firebase-adminsdk-ai17o-fc475217c3.json"),
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenBlacklistMiddleware>();
app.UseIpRateLimiting();
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "jbus_endpoints";
});
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
    await Seed.SeedAdmin(context);
    await Seed.SeedCreditCards(context);
}
catch (Exception ex)
{
    var logger = services?.GetService<ILogger<Program>>();
    logger?.LogError(ex, "An Error Occurred During Migration");
}

app.Run();
// app.Run("http://localhost:5000");