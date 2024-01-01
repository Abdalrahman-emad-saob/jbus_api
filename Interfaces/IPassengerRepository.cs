using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPassengerRepository
    {
        void Update(PassengerDto passenger);
        LoginResponseDto CreatePassenger(RegisterDto registerDto);
        IEnumerable<PassengerDto> GetPassengers();
        PassengerDto GetPassengerDtoById(int id);
        PassengerDto GetPassengerDtoByEmail(string Email);
        Passenger GetPassengerById(int id);
        Passenger GetPassengerByEmail(string? Email);
        User GetUserByEmail(string Email);
        bool SaveChanges();
    }
}