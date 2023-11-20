using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PassengerRepository(DataContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PassengerDto GetPassengerByEmail(string Email)
        {
            return _context.Users
                .Where(u => u.Email == Email)
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
        }

        public PassengerDto GetPassengerById(int id)
        {
            return _context.Passengers
                .Where(p => p.Id == id)
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .SingleOrDefault();
        }

        public IEnumerable<PassengerDto> GetPassengers()
        {
            return _context.Passengers
                .ProjectTo<PassengerDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(PassengerDto passenger)
        {
            _context.Entry(passenger).State = EntityState.Modified;
        }
    }
}