using API.Controllers.v1;
using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    public class RouteController : BaseApiController
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IInterestPointRepository _interestPointRepository;
        private readonly IMapper _mapper;

        public RouteController(IRouteRepository routeRepository, IInterestPointRepository interestPointRepository, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _interestPointRepository = interestPointRepository;
            _mapper = mapper;
        }
        // TODO Create Route
        [HttpGet("GetRoutes")]
        public ActionResult<IEnumerable<RouteDto>> GetRoutes() => Ok(_routeRepository.GetRoutes());

        [HttpGet("{id}")]
        public ActionResult<RouteDto> GetRouteById(int id) => _routeRepository.GetRouteById(id);

        [HttpPut("{id}")]
        public ActionResult updateRoute(RouteUpdateDto routeUpdateDto, int id)
        {
            var route = _routeRepository.GetRouteById(id);
            // var InterestPointStart = _interestPointRepository.GetInterestPointById(routeUpdateDto.StartingPointId);
            // var InterestPointEnd = _interestPointRepository.GetInterestPointById(routeUpdateDto.EndingPointId);

            if (route == null) return NotFound();
            _mapper.Map(routeUpdateDto, route);
            // _mapper.Map(routeUpdateDto.StartingPoint, InterestPointStart);
            // _mapper.Map(routeUpdateDto.EndingPoint, InterestPointEnd);
            if (_routeRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to Update Route");
        }
    }
}