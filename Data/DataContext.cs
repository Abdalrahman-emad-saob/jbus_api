using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<ChargingTransaction> ChargingTransactions { get; set; }
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


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                        .OnDelete(DeleteBehavior.Restrict);

            // one-to-one
            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.PaymentTransaction)
                        .WithOne(pt => pt.Trip)
                        .HasForeignKey<PaymentTransaction>(pt => pt.TripId)
                        .OnDelete(DeleteBehavior.Restrict);
            // one-to-many
            // modelBuilder.Entity<Entities.Route>()
            //             .HasMany(r => r.Trips)
            //             .WithOne(t => t.Route)
            //             .HasForeignKey(t => t.RouteId)
            //             .OnDelete(DeleteBehavior.NoAction);

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

            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.PickUpPoint)
                        .WithOne(p => p.TripPickup)
                        .HasForeignKey<Trip>(t => t.PickUpPointId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.DropOffPoint)
                        .WithOne(p => p.TripDropoff)
                        .HasForeignKey<Trip>(t => t.DropOffPointId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Passenger>()
                        .HasOne(p => p.Creditor)
                        .WithOne(f => f.Creditor)
                        .HasForeignKey<Fazaa>(f => f.CreditorId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Passenger>()
                        .HasOne(p => p.InDebt)
                        .WithOne(f => f.InDebt)
                        .HasForeignKey<Fazaa>(f => f.InDebtId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Entities.Route>()
                        .HasOne(r => r.PredefinedStops)
                        .WithOne(ps => ps.Route)
                        .HasForeignKey<PredefinedStops>(ps => ps.RouteId)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    }

}