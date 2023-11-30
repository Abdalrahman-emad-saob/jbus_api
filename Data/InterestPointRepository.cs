using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class InterestPointRepository : IInterestPointRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InterestPointRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public InterestPointDto GetInterestPointById(int id)
        {
            return _context.InterestPoints.Where(ip => ip.Id == id).ProjectTo<InterestPointDto>(_mapper.ConfigurationProvider).SingleOrDefault();
        }

        public IEnumerable<InterestPointDto> GetInterestPoints()
        {
            return _context.InterestPoints.ProjectTo<InterestPointDto>(_mapper.ConfigurationProvider);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(InterestPointDto interestPoint)
        {
            _context.Entry(interestPoint).State = EntityState.Modified;
        }
    }
}