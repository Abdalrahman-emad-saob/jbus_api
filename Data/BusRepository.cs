using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class BusRepository : IBusRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BusRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateBus(BusCreateDto busCreateDto)
        {
            Bus bus = new()
            {
                BusNumber = busCreateDto.BusNumber,
                RouteId = busCreateDto.RouteId,
                DriverId = busCreateDto.DriverId
            };
            _context.Buses.Add(bus);

            return SaveChanges();
        }

        public BusDto GetBusById(int id)
        {
            return _context
           .Buses
           .Where(d => d.Id == id)
           .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<BusDto> GetBuses()
        {
            return _context
                .Buses
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(BusDto busDto)
        {
            _context.Entry(busDto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}