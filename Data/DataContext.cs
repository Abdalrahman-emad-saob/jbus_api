using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Bus> Buses { get; set; }
        public DbSet<ChargingTransaction> ChargingTransactions { get; set; }
        public DbSet<FavoritePoint> FavoritePoints { get; set; }
        public DbSet<InterestPoint> InterestPoints { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Entities.Route> Routes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOne(p => p.Passenger)
                        .WithOne(u => u.User)
                        .HasForeignKey<Passenger>(u => u.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Passenger>()
                        .HasMany(o => o.OTPs)
                        .WithOne(u => u.Passenger)
                        .HasForeignKey(o => o.PassengerId)
                        .OnDelete(DeleteBehavior.NoAction);
            // TODO
            // modelBuilder.Entity<User>()
            //             .HasOne(p => p.Passenger)
            //             .WithOne(u => u.User)
            //             .HasForeignKey<Passenger>(u => u.UserId)
            //             .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<User>()
            //             .HasOne(p => p.Passenger)

            //             .WithOne(u => u.User)
            //             .HasForeignKey<Passenger>(u => u.UserId)
            //             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}