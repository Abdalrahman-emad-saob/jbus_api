using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            // .LogTo(Console.WriteLine, LogLevel.Information);
            // opt.UseSqlServer(configuration.GetConnectionString("MSSqlServer"))
        });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "jbus-api",
                        Version = "v1",
                        Description = "jbus API",
                        Extensions = new Dictionary<string, IOpenApiExtension>()
                        {
                            {
                                "x-logo", new OpenApiObject
                                {
                                    { "url", new OpenApiString("/images/logo.png") },
                                    { "altText", new OpenApiString("JBus Logo") }
                                }
                            }
                        }
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });
                });

            services.AddCors();
            services.AddScoped<DataContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IChargingTransactionRepository, ChargingTransactionRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IFavoritePointRepository, FavoritePointRepository>();
            services.AddScoped<IInterestPointRepository, InterestPointRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IPointRepository, PointRepository>();
            services.AddScoped<IDriverTripRepository, DriverTripRepository>();
            services.AddScoped<IFazaaRepository, FazaaRepository>();
            services.AddScoped<IPredefinedStopsRepository, PredefinedStopsRepository>();
            services.AddScoped<IFriendsRepository, FriendsRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<INotisTokenRepository, NotisTokenRepository>();
            services.AddScoped<IBlacklistedTokenRepository, BlacklistedTokenRepository>();
            services.AddScoped<ICreditCardsRepository, CreditCardsRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenHandlerService, TokenHandlerService>();
            string key = configuration["Crypto:Key"] ?? string.Empty;
            string iv = configuration["Crypto:Iv"] ?? string.Empty;
            services.AddScoped<ICryptoService>(provider => new CryptoService(key, iv));
            services.AddScoped<NotificationService>();
            // services.AddMemoryCache();
            // services.AddSingleton<IProcessingStrategy, FixedWindowProcessingStrategy>();
            // services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            // services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            // services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }
    }
}