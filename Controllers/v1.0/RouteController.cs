using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [Authorize]
    public class RouteController : BaseApiController
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;
        private readonly IFavoritePointRepository _favoritePointRepository;

        public RouteController(
            IRouteRepository routeRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService,
            IFavoritePointRepository favoritePointRepository)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
            _tokenHandlerService = tokenHandlerService;
            _favoritePointRepository = favoritePointRepository;
        }
        [HttpPost("addroute")]
        public ActionResult CreateRoute(RouteCreateDto routeCreateDto)
        {
            _routeRepository.CreateRoute(routeCreateDto);
            return Created();
        }
        [HttpGet("getroutes")]
        public ActionResult<IEnumerable<RouteDto>> GetRoutes() => Ok(_routeRepository.GetRoutes());

        [HttpGet("{id}")]
        public ActionResult<RouteDto> GetRouteById(int id) => _routeRepository.GetRouteById(id);

        [HttpPut("{id}")]
        public ActionResult updateRoute(RouteUpdateDto routeUpdateDto, int id)
        {
            var route = _routeRepository.GetRouteById(id);

            if (route == null) return NotFound();
            _mapper.Map(routeUpdateDto, route);

            if (_routeRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to Update Route");
        }
        [HttpGet("{id}/favoritepoints")]
        public ActionResult<IEnumerable<FavoritePointDto>> GetRouteFavoritePoints(int id)
        {
            int PassengerId = _tokenHandlerService.TokenHandler();
            if (PassengerId == -1)
                return Unauthorized("Not authorized");
            IEnumerable<FavoritePointDto> favoritePointDtos = _favoritePointRepository.GetRouteFavoritePointDtos(PassengerId, id);
            if(favoritePointDtos == null || !favoritePointDtos.Any())
                return NotFound("No favorite points found for the specified PassengerId and RouteId");
            return Ok(favoritePointDtos);
        }
    }
}