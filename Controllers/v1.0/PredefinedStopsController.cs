using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [CustomAuthorize("PASSENGER")]
    public class PredefinedStopsController : BaseApiController
    {
        private readonly IPredefinedStopsRepository _predefinedStopsRepository;

        public PredefinedStopsController(
            IPredefinedStopsRepository predefinedStopsRepository
            )
        {
            _predefinedStopsRepository = predefinedStopsRepository;
        }

        [HttpPost]
        public ActionResult<PointDto> CreatePredefinedStops(PredefinedStopsCreateDto predefinedStopsCreateDto)
        {
            var predefinedStops = _predefinedStopsRepository.CreatePredefinedStops(predefinedStopsCreateDto);
            if(!_predefinedStopsRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
            return Ok(predefinedStops);
        }

        [HttpGet("{id}")]
        public ActionResult<PredefinedStopsDto> getPredefinedStopsById(int id)
        {
            PredefinedStopsDto predefinedStops = _predefinedStopsRepository.GetPredefinedStopById(id);
            
            if(predefinedStops == null)
                return NotFound("No Predefined Stops Defined");

            return Ok(predefinedStops);
        }
        // private PointDto PointExists(double lat, double lon)
        // {
        //     return _pointRepository.PointExists(lat, lon);
        // }
    }
}