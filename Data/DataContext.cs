using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Bus> Buses { get; set; }
        public DbSet<ChargingTransaction> ChargingTransactions { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<FavoritePoint> FavoritePoints { get; set; }
        public DbSet<InterestPoint> InterestPoints { get; set; }
        // public DbSet<OTP> OTPs { get; set; }
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

            // modelBuilder.Entity<Passenger>()
            //             .HasMany(o => o.OTPs)
            //             .WithOne(u => u.Passenger)
            //             .HasForeignKey(o => o.PassengerId)
            //             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Driver>()
                        .HasOne(d => d.Bus)
                        .WithOne(b => b.Driver)
                        .HasForeignKey<Bus>(b => b.DriverId)
                        .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Bus>()
            //             .HasMany(b => b.Trips)
            //             .WithOne(t => t.Bus)
            //             .HasForeignKey(t => t.BusId)
            //             .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Passenger>()
            //             .HasMany(p => p.ChargingTransactions)
            //             .WithOne(ct => ct.Passenger)
            //             .HasForeignKey(ct => ct.PassengerId)
            //             .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Passenger>()
            //             .HasMany(p => p.FavoritePoints)
            //             .WithOne(fp => fp.Passenger)
            //             .HasForeignKey(fp => fp.PassengerId)
            //             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Point>()
                        .HasOne(p => p.FavoritePoint)
                        .WithOne(fp => fp.Point)
                        .HasForeignKey<FavoritePoint>(fp => fp.PointId)
                        .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Passenger>()
            //             .HasMany(p => p.PaymentTransactions)
            //             .WithOne(pm => pm.Passenger)
            //             .HasForeignKey(pm => pm.PassengerId)
            //             .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Passenger>()
            //             .HasMany(p => p.Trips)
            //             .WithOne(t => t.Passenger)
            //             .HasForeignKey(t => t.PassengerId)
            //             .OnDelete(DeleteBehavior.NoAction);

            // one-to-many
            // modelBuilder.Entity<Entities.Route>()
            //             .HasMany(r => r.Buses)
            //             .WithOne(b => b.Route)
            //             .HasForeignKey(b => b.RouteId)
            //             .OnDelete(DeleteBehavior.NoAction);
            // one-to-many
            // modelBuilder.Entity<Entities.Route>()
            //             .HasMany(r => r.FavoritePoints)
            //             .WithOne(fp => fp.Route)
            //             .HasForeignKey(fp => fp.RouteId)
            //             .OnDelete(DeleteBehavior.NoAction);
            // one-to-one
            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.PaymentTransaction)
                        .WithOne(pt => pt.Trip)
                        .HasForeignKey<PaymentTransaction>(pt => pt.TripId)
                        .OnDelete(DeleteBehavior.NoAction);
            // one-to-many
            modelBuilder.Entity<Entities.Route>()
                        .HasMany(r => r.Trips)
                        .WithOne(t => t.Route)
                        .HasForeignKey(t => t.RouteId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InterestPoint>()
                        .HasOne(ip => ip.RouteStart)
                        .WithOne(r => r.StartingPoint)
                        .HasForeignKey<Entities.Route>(r => r.StartingPointId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InterestPoint>()
                        .HasOne(ip => ip.RouteEnd)
                        .WithOne(r => r.EndingPoint)
                        .HasForeignKey<Entities.Route>(r => r.EndingPointId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Point>()
                        .HasOne(p => p.InterestPoint)
                        .WithOne(ip => ip.Location)
                        .HasForeignKey<InterestPoint>(ip => ip.LocationId)
                        .OnDelete(DeleteBehavior.NoAction);

            // TODO
            modelBuilder.Entity<Trip>()
                        .HasOne(t => t.PickUpPoint)
                        .WithOne(p => p.TripPickup)
                        .HasForeignKey<Point>(t => t.TripPickupId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trip>() 
                        .HasOne(t => t.DropOffPoint)
                        .WithOne(p => p.TripDropoff)
                        .HasForeignKey<Point>(t => t.TripDropoffId)
                        .OnDelete(DeleteBehavior.NoAction);
            // base.OnModelCreating(modelBuilder);
        }
    }

}