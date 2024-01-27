using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [CustomAuthorize("PASSENGER")]
    public class PointController(
        IPointRepository pointRepository
            ) : BaseApiController
    {
        private readonly IPointRepository _pointRepository = pointRepository;

        [HttpPost]
        public async Task<ActionResult<PointDto>> CreatePoint(PointCreateDto pointCreateDto)
        {
            PointDto? point = await PointExists(pointCreateDto.Latitude, pointCreateDto.Longitude);
            if (point == null)
            {
                await _pointRepository.CreatePoint(pointCreateDto);
               if(!await _pointRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
                return StatusCode(201, "Point Created");
            }
            return Ok(point);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PointDto>> GetPointById(int id)
        {
            var pointDto = await _pointRepository.GetPointById(id);

            if (pointDto == null)
                return NotFound("Point Not Found");

            return pointDto;
        }
        private async Task<PointDto?> PointExists(double lat, double lon)
        {
            return await _pointRepository.PointExists(lat, lon);
        }
    }
}