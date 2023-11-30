using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPassengerRepository
    {
        void Update(PassengerDto passenger);
        IEnumerable<PassengerDto> GetPassengers();
        PassengerDto GetPassengerById(int id);
        PassengerDto GetPassengerByEmail(string Email);
        UserDto GetUserById(int id);
        Passenger GetPassengerOnlyById(int id);
        Passenger GetPassengerOnlyByEmail(string Email);
        bool SaveChanges();
    }
}