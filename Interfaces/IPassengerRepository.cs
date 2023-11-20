using API.DTOs;

namespace API.Interfaces
{
    public interface IPassengerRepository
    {
        void Update(PassengerDto passenger);
        IEnumerable<PassengerDto> GetPassengers();
        PassengerDto GetPassengerById(int id);
        PassengerDto GetPassengerByEmail(string Email);
        bool SaveChanges();
    }
}