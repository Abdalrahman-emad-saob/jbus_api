using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .LogTo(Console.WriteLine, LogLevel.Information);
            // opt.UseSqlServer(configuration.GetConnectionString("MSSqlServer"))
        });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors();
            services.AddScoped<DataContext>();
            services.AddScoped<ITokenService, TokenService>();
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
            services.AddScoped<ITokenHandlerService, TokenHandlerService>();
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
            services.AddSingleton<NotificationService>();

            return services;
        }
    }
}