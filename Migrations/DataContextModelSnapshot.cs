﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("API.Entities.BlacklistedToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BlacklistedTokens");
                });

            modelBuilder.Entity("API.Entities.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BusNumber")
                        .HasColumnType("text");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.Property<int>("Going")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("RouteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DriverId")
                        .IsUnique();

                    b.HasIndex("RouteId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("API.Entities.ChargingTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("ChargingMethod")
                        .HasColumnType("integer");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PassengerId");

                    b.ToTable("ChargingTransactions");
                });

            modelBuilder.Entity("API.Entities.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.Property<short>("CVC")
                        .HasColumnType("smallint");

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("CardType")
                        .HasColumnType("text");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CardNumber")
                        .IsUnique();

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("API.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BusId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("API.Entities.DriverTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int?>("RouteId")
                        .HasColumnType("integer");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BusId");

                    b.HasIndex("DriverId");

                    b.HasIndex("RouteId");

                    b.ToTable("DriverTrips");
                });

            modelBuilder.Entity("API.Entities.FavoritePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("integer");

                    b.Property<int?>("PointId")
                        .HasColumnType("integer");

                    b.Property<int?>("RouteId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PassengerId");

                    b.HasIndex("PointId");

                    b.HasIndex("RouteId");

                    b.ToTable("FavoritePoints");
                });

            modelBuilder.Entity("API.Entities.Fazaa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CreditorId")
                        .HasColumnType("integer");

                    b.Property<int?>("InDebtId")
                        .HasColumnType("integer");

                    b.Property<bool>("Paid")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReturnedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreditorId");

                    b.HasIndex("InDebtId");

                    b.ToTable("Fazaas");
                });

            modelBuilder.Entity("API.Entities.Friends", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Confirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ConfirmedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("FriendId")
                        .HasColumnType("integer");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("API.Entities.InterestPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("RouteEndId")
                        .HasColumnType("integer");

                    b.Property<int?>("RouteStartId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("InterestPoints");
                });

            modelBuilder.Entity("API.Entities.OTP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Otp")
                        .HasColumnType("integer");

                    b.Property<string>("PassengerEmail")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OTPs");
                });

            modelBuilder.Entity("API.Entities.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FacebookToken")
                        .HasColumnType("text");

                    b.Property<string>("FcmToken")
                        .HasColumnType("text");

                    b.Property<string>("GoogleToken")
                        .HasColumnType("text");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<double>("Wallet")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("API.Entities.PaymentTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int?>("BusId")
                        .HasColumnType("integer");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("integer");

                    b.Property<int?>("RouteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("TripId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BusId");

                    b.HasIndex("DriverId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("RouteId");

                    b.HasIndex("TripId")
                        .IsUnique();

                    b.ToTable("PaymentTransactions");
                });

            modelBuilder.Entity("API.Entities.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("InterestPointId")
                        .HasColumnType("integer");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("PredefinedStopsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PredefinedStopsId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("API.Entities.PredefinedStops", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IsActive")
                        .HasColumnType("integer");

                    b.Property<int?>("RouteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RouteId")
                        .IsUnique();

                    b.ToTable("PredefinedStops");
                });

            modelBuilder.Entity("API.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EndingPointId")
                        .HasColumnType("integer");

                    b.Property<double>("Fee")
                        .HasColumnType("double precision");

                    b.Property<int>("IsActive")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PredefinedStopsId")
                        .HasColumnType("integer");

                    b.Property<int?>("StartingPointId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("WaypointsGoing")
                        .HasColumnType("text");

                    b.Property<string>("WaypointsReturning")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EndingPointId")
                        .IsUnique();

                    b.HasIndex("StartingPointId")
                        .IsUnique();

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("API.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DriverTripId")
                        .HasColumnType("integer");

                    b.Property<int?>("DropOffPointId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PassengerId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentTransactionId")
                        .HasColumnType("integer");

                    b.Property<int?>("PickUpPointId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DriverTripId");

                    b.HasIndex("DropOffPointId");

                    b.HasIndex("PassengerId");

                    b.HasIndex("PickUpPointId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Admin", b =>
                {
                    b.HasOne("API.Entities.User", "User")
                        .WithOne("Admin")
                        .HasForeignKey("API.Entities.Admin", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.Bus", b =>
                {
                    b.HasOne("API.Entities.Driver", "Driver")
                        .WithOne("Bus")
                        .HasForeignKey("API.Entities.Bus", "DriverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API.Entities.Route", "Route")
                        .WithMany("Buses")
                        .HasForeignKey("RouteId");

                    b.Navigation("Driver");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("API.Entities.ChargingTransaction", b =>
                {
                    b.HasOne("API.Entities.Passenger", "Passenger")
                        .WithMany("ChargingTransactions")
                        .HasForeignKey("PassengerId");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("API.Entities.Driver", b =>
                {
                    b.HasOne("API.Entities.User", "User")
                        .WithOne("Driver")
                        .HasForeignKey("API.Entities.Driver", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.DriverTrip", b =>
                {
                    b.HasOne("API.Entities.Bus", "Bus")
                        .WithMany("DriverTrips")
                        .HasForeignKey("BusId");

                    b.HasOne("API.Entities.Driver", "Driver")
                        .WithMany("DriverTrips")
                        .HasForeignKey("DriverId");

                    b.HasOne("API.Entities.Route", "Route")
                        .WithMany("DriverTrips")
                        .HasForeignKey("RouteId");

                    b.Navigation("Bus");

                    b.Navigation("Driver");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("API.Entities.FavoritePoint", b =>
                {
                    b.HasOne("API.Entities.Passenger", "Passenger")
                        .WithMany("FavoritePoints")
                        .HasForeignKey("PassengerId");

                    b.HasOne("API.Entities.Point", "Point")
                        .WithMany("FavoritePoint")
                        .HasForeignKey("PointId");

                    b.HasOne("API.Entities.Route", "Route")
                        .WithMany("FavoritePoints")
                        .HasForeignKey("RouteId");

                    b.Navigation("Passenger");

                    b.Navigation("Point");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("API.Entities.Fazaa", b =>
                {
                    b.HasOne("API.Entities.Passenger", "Creditor")
                        .WithMany("Creditors")
                        .HasForeignKey("CreditorId");

                    b.HasOne("API.Entities.Passenger", "InDebt")
                        .WithMany("InDebts")
                        .HasForeignKey("InDebtId");

                    b.Navigation("Creditor");

                    b.Navigation("InDebt");
                });

            modelBuilder.Entity("API.Entities.Friends", b =>
                {
                    b.HasOne("API.Entities.Passenger", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendId");

                    b.HasOne("API.Entities.Passenger", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId");

                    b.Navigation("Friend");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("API.Entities.InterestPoint", b =>
                {
                    b.HasOne("API.Entities.Point", "Location")
                        .WithOne("InterestPoint")
                        .HasForeignKey("API.Entities.InterestPoint", "LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Location");
                });

            modelBuilder.Entity("API.Entities.Passenger", b =>
                {
                    b.HasOne("API.Entities.User", "User")
                        .WithOne("Passenger")
                        .HasForeignKey("API.Entities.Passenger", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.PaymentTransaction", b =>
                {
                    b.HasOne("API.Entities.Bus", "Bus")
                        .WithMany()
                        .HasForeignKey("BusId");

                    b.HasOne("API.Entities.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("API.Entities.Passenger", "Passenger")
                        .WithMany("PaymentTransactions")
                        .HasForeignKey("PassengerId");

                    b.HasOne("API.Entities.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.HasOne("API.Entities.Trip", "Trip")
                        .WithOne("PaymentTransaction")
                        .HasForeignKey("API.Entities.PaymentTransaction", "TripId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Bus");

                    b.Navigation("Driver");

                    b.Navigation("Passenger");

                    b.Navigation("Route");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("API.Entities.Point", b =>
                {
                    b.HasOne("API.Entities.PredefinedStops", null)
                        .WithMany("points")
                        .HasForeignKey("PredefinedStopsId");
                });

            modelBuilder.Entity("API.Entities.PredefinedStops", b =>
                {
                    b.HasOne("API.Entities.Route", "Route")
                        .WithOne("PredefinedStops")
                        .HasForeignKey("API.Entities.PredefinedStops", "RouteId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Route");
                });

            modelBuilder.Entity("API.Entities.Route", b =>
                {
                    b.HasOne("API.Entities.InterestPoint", "EndingPoint")
                        .WithOne("RouteEnd")
                        .HasForeignKey("API.Entities.Route", "EndingPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Entities.InterestPoint", "StartingPoint")
                        .WithOne("RouteStart")
                        .HasForeignKey("API.Entities.Route", "StartingPointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("EndingPoint");

                    b.Navigation("StartingPoint");
                });

            modelBuilder.Entity("API.Entities.Trip", b =>
                {
                    b.HasOne("API.Entities.DriverTrip", "DriverTrip")
                        .WithMany("Trips")
                        .HasForeignKey("DriverTripId");

                    b.HasOne("API.Entities.Point", "DropOffPoint")
                        .WithMany("TripDropoff")
                        .HasForeignKey("DropOffPointId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API.Entities.Passenger", "Passenger")
                        .WithMany("Trips")
                        .HasForeignKey("PassengerId");

                    b.HasOne("API.Entities.Point", "PickUpPoint")
                        .WithMany("TripPickup")
                        .HasForeignKey("PickUpPointId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DriverTrip");

                    b.Navigation("DropOffPoint");

                    b.Navigation("Passenger");

                    b.Navigation("PickUpPoint");
                });

            modelBuilder.Entity("API.Entities.Bus", b =>
                {
                    b.Navigation("DriverTrips");
                });

            modelBuilder.Entity("API.Entities.Driver", b =>
                {
                    b.Navigation("Bus");

                    b.Navigation("DriverTrips");
                });

            modelBuilder.Entity("API.Entities.DriverTrip", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("API.Entities.InterestPoint", b =>
                {
                    b.Navigation("RouteEnd");

                    b.Navigation("RouteStart");
                });

            modelBuilder.Entity("API.Entities.Passenger", b =>
                {
                    b.Navigation("ChargingTransactions");

                    b.Navigation("Creditors");

                    b.Navigation("FavoritePoints");

                    b.Navigation("InDebts");

                    b.Navigation("PaymentTransactions");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("API.Entities.Point", b =>
                {
                    b.Navigation("FavoritePoint");

                    b.Navigation("InterestPoint");

                    b.Navigation("TripDropoff");

                    b.Navigation("TripPickup");
                });

            modelBuilder.Entity("API.Entities.PredefinedStops", b =>
                {
                    b.Navigation("points");
                });

            modelBuilder.Entity("API.Entities.Route", b =>
                {
                    b.Navigation("Buses");

                    b.Navigation("DriverTrips");

                    b.Navigation("FavoritePoints");

                    b.Navigation("PredefinedStops");
                });

            modelBuilder.Entity("API.Entities.Trip", b =>
                {
                    b.Navigation("PaymentTransaction");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Driver");

                    b.Navigation("Passenger");
                });
#pragma warning restore 612, 618
        }
    }
}
