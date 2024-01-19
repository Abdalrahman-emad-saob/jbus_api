using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
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

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("addRoute")]
        public ActionResult CreateRoute(RouteCreateDto routeCreateDto)
        {
            if(_routeRepository.CreateRoute(routeCreateDto))
                if(_routeRepository.SaveChanges())
                    return StatusCode(201);
            return StatusCode(500, "Server Error");
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("getRoutes")]
        public ActionResult<IEnumerable<RouteDto>> GetRoutes()
        {
            return Ok(_routeRepository.GetRoutes());
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("{id}")]
        public ActionResult<RouteDto> GetRouteById(int id)
        {
            return _routeRepository.GetRouteById(id);
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public ActionResult updateRoute(RouteUpdateDto routeUpdateDto, int id)
        {
            var route = _routeRepository.GetRouteById(id);

            if (route == null) 
                return NotFound("Route Not Found");

            _mapper.Map(routeUpdateDto, route);

            if (_routeRepository.SaveChanges()) 
                return NoContent();

            return BadRequest("Failed to Update Route");
        }
        [CustomAuthorize("PASSENGER")]
        [HttpGet("{id}/favoritepoints")]
        public ActionResult<IEnumerable<FavoritePointDto>> GetRouteFavoritePoints(int id)
        {
            int PassengerId = _tokenHandlerService.TokenHandler();
            if (PassengerId == -1)
                return Unauthorized("Not authorized1");

            IEnumerable<FavoritePointDto> favoritePointDtos = _favoritePointRepository.GetRouteFavoritePointDtos(PassengerId, id);
                
            return Ok(favoritePointDtos);
        }
    }
}