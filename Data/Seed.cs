using API.Entities;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedPassenger(DataContext context)
        {
            if (await context.Passengers.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Abood Saob",
                PhoneNumber = "0785455414",
                Email = "aboodsaob1139@gmail.com",
                UserRole = User.Role.PASSENGER,
                UserGender = User.Gender.MALE,
                DateOfBirth = new DateTime(2002, 3, 26),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Passenger passenger = new()
            {
                Wallet = 1000,
                GoogleToken = "google_token_here",
                FacebookToken = "facebook_token_here",
                User = user,
            };
            user.Passenger = passenger;
            context.Users.Add(user);
            context.Passengers.Add(passenger);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDriver(DataContext context)
        {
            if (await context.Drivers.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Khader Abumallouh",
                PhoneNumber = "0790364258",
                Email = "khader.mallouh@gmail.com",
                UserRole = User.Role.DRIVER,
                UserGender = User.Gender.MALE,
                DateOfBirth = new DateTime(2000, 6, 25),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Driver driver = new()
            {
                User = user,
            };
            user.Driver = driver;
            context.Users.Add(user);
            context.Drivers.Add(driver);
            await context.SaveChangesAsync();
        }

        public static async Task SeedPoint(DataContext context)
        {
            if (await context.Points.AnyAsync()) return;
            Point point1 = new()
            {
                Name = "Custom1",
                Latitude = 118.06,
                Longitude = 118.06,
            };
            context.Points.Add(point1);
            Point point2 = new()
            {
                Name = "Custom2",
                Latitude = 118.06,
                Longitude = 118.06,
            };
            context.Points.Add(point2);
            await context.SaveChangesAsync();
        }
        public static async Task SeedInterestPoints(DataContext context)
        {
            if (await context.InterestPoints.AnyAsync()) return;
            var point1 = context.Points.Find(1);
            var point2 = context.Points.Find(2);

            InterestPoint startingPoint = new()
            {
                Name = "Starting Point",
                Logo = "5.png"
            };

            InterestPoint endingPoint = new()
            {
                Name = "Ending Point",
                Logo = "5.png"
            };
            startingPoint.LocationId = point1!.Id;
            endingPoint.LocationId = point2!.Id;
            context.InterestPoints.Add(startingPoint);
            context.InterestPoints.Add(endingPoint);

            await context.SaveChangesAsync();
        }

        public static async Task SeedRoute(DataContext context)
        {
            if (await context.Routes.AnyAsync()) return;
            var interestpoint1 = context.InterestPoints.Find(1);
            var interestpoint2 = context.InterestPoints.Find(2);

            Entities.Route route = new()
            {
                Name = "Example Route",
                WaypointsGoing = "Waypoint1, Waypoint2, Waypoint3",
                WaypointsReturning = "Waypoint3, Waypoint2, Waypoint1",
                StartingPointId = interestpoint1!.Id,
                EndingPointId = interestpoint2!.Id
            };
            context.Routes.Add(route);

            await context.SaveChangesAsync();
        }

        public static async Task SeedTrip(DataContext context)
        {
            if (await context.Trips.AnyAsync()) return;

            // Assuming you have already seeded other entities like Bus, Route, Point, Passenger, PaymentTransaction

            Trip trip = new()
            {
                CreatedAt = DateTime.UtcNow,
                FinishedAt = DateTime.UtcNow,
                Rating = 5,

                status = Trip.Status.COMPLETED,

                PassengerId = 1,
                BusId = 1,
                PaymentTransactionId = 1,
                RouteId = 1,

                PickUpPointId = 1,
                DropOffPointId = 1
            };

            context.Trips.Add(trip);

            await context.SaveChangesAsync();
        }

        public static async Task SeedFavoritePoint(DataContext context)
        {
            if (await context.FavoritePoints.AnyAsync()) return;
            var point = context.Points.Find(1);
            var route = context.Routes.Find(1);
            FavoritePoint favoritePoint = new()
            {
                PointId = point!.Id,
                PassengerId = 1,
                RouteId = route!.Id
            };
            context.FavoritePoints.Add(favoritePoint);
            point.FavoritePoint!.Add(favoritePoint);


            await context.SaveChangesAsync();
        }
        
        public static async Task SeedOTP(DataContext context)
        {
            if (await context.Passengers.AnyAsync()) return;
            var passenger = await context.Passengers.FindAsync(1);
            var user = await context.Users.FindAsync(passenger.UserId);
            if (passenger != null && user != null)
            {
                OTP otp = new()
                {
                    Otp = 1234,
                    PassengerEmail = user.Email 
                };
                context.OTPs.Add(otp);
                await context.SaveChangesAsync();
            }
        }
    }
}