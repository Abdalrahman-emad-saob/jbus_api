using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPassengerRepository
    {
        bool Update(PassengerUpdateDto passengerUpdateDto, Passenger passenger, User user);
        Task<RegisterResponseDto?> CreatePassenger(RegisterDto registerDto);
        Task<IEnumerable<PassengerDto?>> GetPassengers();
        Task<PassengerDto?> GetPassengerDtoById(int id);
        Task<PassengerDto?> GetPassengerDtoByEmail(string Email);
        Task<Passenger?> GetPassengerById(int id);
        Task<Passenger?> GetPassengerByEmail(string? Email);
        Task UpdateRewardPoints(int rp, int id);
        Task UpdateRewardPointsToAll(int rp);
        Task<bool> SaveChanges();
    }
}