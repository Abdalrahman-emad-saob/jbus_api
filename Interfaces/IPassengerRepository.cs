using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPassengerRepository
    {
        void Update(PassengerUpdateDto passengerUpdateDto, Passenger passenger, User user);
        RegisterResponseDto CreatePassenger(RegisterDto registerDto);
        IEnumerable<PassengerDto> GetPassengers();
        PassengerDto GetPassengerDtoById(int id);
        PassengerDto GetPassengerDtoByEmail(string Email);
        Passenger GetPassengerById(int id);
        Passenger GetPassengerByEmail(string? Email);
        bool SaveChanges();
    }
}