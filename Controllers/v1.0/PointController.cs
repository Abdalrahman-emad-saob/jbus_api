using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PointController : BaseApiController
    {
        private readonly IPointRepository _pointRepository;
        private readonly ITokenHandlerService _tokenHandlerService;

        public PointController(
            IPointRepository pointRepository,
            ITokenHandlerService tokenHandlerService
            )
        {
            _pointRepository = pointRepository;
            _tokenHandlerService = tokenHandlerService;
        }
        [HttpPost]
        public ActionResult<PointDto> CreatePoint(PointCreateDto pointCreateDto)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" ||
            role.ToUpper() != Role.PASSENGER.ToString()
            )
                return Unauthorized("Not authorized");
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
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" ||
            role.ToUpper() != Role.PASSENGER.ToString()
            )
                return Unauthorized("Not authorized");

            var pointDto = _pointRepository.GetPointById(id);

            if (pointDto == null)
                return NotFound("Point Not Found");

            return pointDto;
        }
        [NonAction]
        private PointDto PointExists(double lat, double lon)
        {
            return _pointRepository.PointExists(lat, lon);
        }
    }
}