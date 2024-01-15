using API.Entities;
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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                PhoneNumber = "0785455414",
                Email = "aboodsaob1139@gmail.com",
                Role = Role.PASSENGER,
                Sex = Sex.MALE,
                DateOfBirth = new DateOnly(2002, 3, 26),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Passenger passenger = new()
            {
                Wallet = 1000,
                GoogleToken = "google_token_here",
                FacebookToken = "facebook_token_here",
                User = user
            };
            user.Passenger = passenger;
            context.Users.Add(user);
            context.Passengers.Add(passenger);
            await context.SaveChangesAsync();
        }

        public static async Task SeedAdmin(DataContext context)
        {
            if (await context.Admins.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Fadl Al masri",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                PhoneNumber = "0791012625",
                Email = "fdlmasri@gmail.com",
                Role = Role.SUPER_ADMIN,
                Sex = Sex.MALE,
                DateOfBirth = new DateOnly(2002, 6, 25),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Admin admin = new()
            {
                User = user,
            };
            user.Admin = admin;
            context.Users.Add(user);
            context.Admins.Add(admin);
            await context.SaveChangesAsync();
        }

        public static async Task SeedDriver(DataContext context)
        {
            if (await context.Drivers.AnyAsync()) return;
            var passwordHasher = new PasswordHasher<User>();

            User user = new()
            {
                Name = "Khader Abumallouh",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastActive = DateTime.UtcNow,
                PhoneNumber = "0790364258",
                Email = "khader.mallouh@gmail.com",
                Role = Role.DRIVER,
                Sex = Sex.MALE,
                DateOfBirth = new DateOnly(2000, 6, 25),
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
                Name = "JUST Bus Station",
                Latitude = 32.49512209286742,
                Longitude = 35.98597417188871,
            };
            context.Points.Add(point1);
            Point point2 = new()
            {
                Name = "North Bus Station",
                Latitude = 31.9957018434082,
                Longitude = 35.91987132195803,
            };
            Point point3 = new()
            {
                Name = "حي الجامعة الاردنية",
                Latitude = 32.02511248566629,
                Longitude = 35.89201395463377,
            };
            context.Points.Add(point1);
            context.Points.Add(point2);
            context.Points.Add(point3);
            await context.SaveChangesAsync();
        }
        public static async Task SeedInterestPoints(DataContext context)
        {
            if (await context.InterestPoints.AnyAsync()) return;
            var point1 = context.Points.Find(1);
            var point2 = context.Points.Find(2);

            InterestPoint startingPoint = new()
            {
                Name = "North Bus Station",
                Logo = "5.png"
            };

            InterestPoint endingPoint = new()
            {
                Name = "Ending Point",
                Logo = "5.png"
            };
            startingPoint.LocationId = point2!.Id;
            endingPoint.LocationId = point1!.Id;
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
                Name = "Amman-JUST",
                WaypointsGoing = @"[
                    {
                        ""Location"": {
                            ""Latitude"": 31.9957018434082,
                            ""Longitude"": 35.91987132195803
                        }
                    },
                    {
                        ""Location"": {
                            ""Latitude"": 32.21854548546048,
                            ""Longitude"": 35.89057887311955
                        }
                    },
                    {
                        ""Location"": {
                            ""Latitude"": 32.49512209286742,
                            ""Longitude"": 35.98597417188871
                        }
                    }
                ]",
                WaypointsReturning = @"[
                    [
                        {""Location"": {""Latitude"": 32.49512209286742,
                                ""Longitude"": 35.98597417188871}}
                    ],
                    [
                        {""Location"": {
                                ""Latitude"": 32.21854548546048,
                                ""Longitude"": 35.89057887311955}}
                                ],
                    [
                        {""Location"": {""Latitude"": 31.9957018434082,
                                ""Longitude"": 35.91987132195803}}]
                                ]",
                Fee = 115,
                StartingPointId = interestpoint1!.Id,
                EndingPointId = interestpoint2!.Id
            };
            context.Routes.Add(route);

            await context.SaveChangesAsync();
        }

        public static async Task SeedTrip(DataContext context)
        {
            if (await context.Trips.AnyAsync()) return;

            Trip trip = new()
            {
                StartedAt = DateTime.UtcNow,
                FinishedAt = DateTime.UtcNow,
                status = TripStatus.COMPLETED,
                PassengerId = 1,
                PaymentTransactionId = 1,
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
            if (await context.OTPs.AnyAsync()) return;
            var passenger = await context.Passengers.FindAsync(1);
            var user = await context.Users.FindAsync(passenger!.UserId);
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
        public static async Task SeedCreditCards(DataContext context)
        {
            if (await context.CreditCards.AnyAsync()) return;
            CreditCard creditCard1 = new()
            {
                CardType = "VISA",
                CardNumber = 1234123412341234,
                CVC = 123,
                ExpirationDate = new DateOnly(2025, 1, 16),
                Balance = 999999999999999
            };
            CreditCard creditCard2 = new()
            {
                CardType = "MASTER_CARD",
                CardNumber = 4321432143214321,
                CVC = 321,
                ExpirationDate = new DateOnly(2025, 1, 16),
                Balance = 999999999999999
            };
            CreditCard creditCard3 = new()
            {
                CardType = "VISA",
                CardNumber = 1234567812345678,
                CVC = 123,
                ExpirationDate = new DateOnly(2025, 1, 16),
                Balance = 0
            };
            CreditCard creditCard4 = new()
            {
                CardType = "VISA",
                CardNumber = 1234561234561234,
                CVC = 123,
                ExpirationDate = new DateOnly(2023, 1, 16),
                Balance = 999999999999999
            };
            // CreditCard creditCard5 = new()
            // {
            //     CardType = "VISA",
            //     CardNumber = 1234123412341234,
            //     CVC = 123,
            //     ExpirationDate = new DateOnly(2025, 1, 16),
            //     Balance = 200
            // };
            context.CreditCards.Add(creditCard1);
            context.CreditCards.Add(creditCard2);
            context.CreditCards.Add(creditCard3);
            context.CreditCards.Add(creditCard4);
            // context.CreditCards.Add(creditCard5);

            await context.SaveChangesAsync();
        }
    }
}