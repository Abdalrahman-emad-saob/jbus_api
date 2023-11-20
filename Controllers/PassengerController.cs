using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Authorize]
    public class PassengerController : BaseApiController
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;

        public PassengerController(IPassengerRepository passengerRepository, IMapper mapper)
        {
            _passengerRepository = passengerRepository;
            _mapper = mapper;
        }
        [HttpGet("GetPassengers")]
        public ActionResult<IEnumerable<PassengerDto>> GetPassengers()
        {
            var passengers = _passengerRepository.GetPassengers();

            return Ok(passengers);
        }
        [HttpGet("{id}")]
        public ActionResult<PassengerDto> GetPassengerById(int id)
        {
            var passenger = _passengerRepository.GetPassengerById(id);
            return passenger;
        }
        // [HttpGet("GetOTPs")]
        // public ActionResult<List<OTP>> GetOTPs()
        // {
        //     var otps = _context.Passengers.Include(p => p.OTPs).FirstOrDefault(p => p.Id == 1).OTPs;
        //     return otps;
        // }

    

    }
}