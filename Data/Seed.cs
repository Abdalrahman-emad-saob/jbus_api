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
                PhoneNumber = "0785455414",
                Email = "aboodsaob1139@gmail.com",
                GoogleToken = "google_token_here",
                FacebookToken = "facebook_token_here",
                UserRole = User.Role.PASSENGER,
                UserGender = User.Gender.MALE,
                DateOfBirth = new DateOnly(2002, 3, 26),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            Passenger passenger = new()
            {
                Wallet = 1000,
                User = user,
            };
            user.Passenger = passenger;
            context.Users.Add(user);
            context.Passengers.Add(passenger);
            await context.SaveChangesAsync();
        }
        public static async Task SeedOTP(DataContext context)
        {
            var passenger = await context.Passengers.FindAsync(1);
            if (passenger != null)
            {
                OTP otp = new()
                {
                    Otp = 1234
                    // Passenger = passenger
                };
                // context.OTPs.Add(otp);
                passenger.OTPs.Add(otp);
                await context.SaveChangesAsync();
            }
        }
    }
}