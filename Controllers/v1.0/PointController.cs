using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [CustomAuthorize("PASSENGER")]
    public class PointController : BaseApiController
    {
        private readonly IPointRepository _pointRepository;

        public PointController(
            IPointRepository pointRepository
            )
        {
            _pointRepository = pointRepository;
        }
        [HttpPost]
        public ActionResult<PointDto> CreatePoint(PointCreateDto pointCreateDto)
        {
            PointDto point = PointExists(pointCreateDto.Latitude, pointCreateDto.Longitude);
            if (point == null)
            {
                _pointRepository.CreatePoint(pointCreateDto);
               if(!_pointRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
                return StatusCode(201, "Point Created");
            }
            return Ok(point);
        }

        [HttpGet("{id}")]
        public ActionResult<PointDto> GetPointById(int id)
        {
            var pointDto = _pointRepository.GetPointById(id);

            if (pointDto == null)
                return NotFound("Point Not Found");

            return pointDto;
        }
        private PointDto PointExists(double lat, double lon)
        {
            return _pointRepository.PointExists(lat, lon);
        }
    }
}