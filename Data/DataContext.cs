using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<ChargingTransaction> ChargingTransactions { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<FavoritePoint> FavoritePoints { get; set; }
        public DbSet<InterestPoint> InterestPoints { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Entities.Route> Routes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DriverTrip> DriverTrips { get; set; }
        public DbSet<Fazaa> Fazaas { get; set; }
        public DbSet<PredefinedStops> PredefinedStops { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<BlacklistedToken> BlacklistedTokens { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>()
                        .HasIndex(c => c.CardNumber)
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .HasOne(p => p.Passenger)
                        .WithOne(u => u.User)
                        .HasForeignKey<Passenger>(u => u.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                        .HasOne(d => d.Driver)
                        .WithOne(u => u.User)
                        .HasForeignKey<Driver>(u => u.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Driver>()
                        .HasOne(d => d.Bus)
                        .WithOne(b => b.Driver)
                        .HasForeignKey<Bus>(b => b.DriverId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.PaymentTransaction)
                        .WithOne(pt => pt.Trip)
                        .HasForeignKey<PaymentTransaction>(pt => pt.TripId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

            modelBuilder.Entity<InterestPoint>()
                        .HasOne(ip => ip.RouteStart)
                        .WithOne(r => r.StartingPoint)
                        .HasForeignKey<Entities.Route>(r => r.StartingPointId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InterestPoint>()
                        .HasOne(ip => ip.RouteEnd)
                        .WithOne(r => r.EndingPoint)
                        .HasForeignKey<Entities.Route>(r => r.EndingPointId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InterestPoint>()
                        .HasOne(ip => ip.Location)
                        .WithOne(p => p.InterestPoint)
                        .HasForeignKey<InterestPoint>(ip => ip.LocationId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Point>()
                        .HasMany(p => p.TripPickup)
                        .WithOne(p => p.PickUpPoint)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired(false);

            modelBuilder.Entity<Point>()
                        .HasMany(p => p.TripDropoff)
                        .WithOne(p => p.DropOffPoint)
                        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Fazaa>()
                        .HasOne(f => f.InDebt)
                        .WithMany(p => p.InDebts)
                        .HasForeignKey(f => f.InDebtId);

            modelBuilder.Entity<Fazaa>()
                        .HasOne(f => f.Creditor)
                        .WithMany(p => p.Creditors)
                        .HasForeignKey(f => f.CreditorId);

            modelBuilder.Entity<Entities.Route>()
                        .HasOne(r => r.PredefinedStops)
                        .WithOne(ps => ps.Route)
                        .HasForeignKey<PredefinedStops>(ps => ps.RouteId)
                        .OnDelete(DeleteBehavior.NoAction);

        }
    }

}