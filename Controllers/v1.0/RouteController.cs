using API.Controllers.v1;
using API.DTOs;
using API.Entities;
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
        [HttpPost("addRoute")]
        public ActionResult CreateRoute(RouteCreateDto routeCreateDto)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" || (role.ToUpper() != "SUPER_ADMIN" && role.ToUpper() != "ADMIN"))
                return Unauthorized("Not authorized");
                
            _routeRepository.CreateRoute(routeCreateDto);
            return Created();
        }
        [HttpGet("getRoutes")]
        public ActionResult<IEnumerable<RouteDto>> GetRoutes()
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" 
            || (role.ToUpper() != Role.SUPER_ADMIN.ToString()
            && role.ToUpper() != Role.ADMIN.ToString() 
            && role.ToUpper() != Role.PASSENGER.ToString()))
                return Unauthorized("Not authorized");
            return Ok(_routeRepository.GetRoutes());
        }

        [HttpGet("{id}")]
        public ActionResult<RouteDto> GetRouteById(int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" 
            || (role.ToUpper() != Role.SUPER_ADMIN.ToString()
            && role.ToUpper() != Role.ADMIN.ToString()))
                return Unauthorized("Not authorized");

            return _routeRepository.GetRouteById(id);
        }

        [HttpPut("{id}")]
        public ActionResult updateRoute(RouteUpdateDto routeUpdateDto, int id)
        {
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" 
            || (role.ToUpper() != Role.SUPER_ADMIN.ToString()
            && role.ToUpper() != Role.ADMIN.ToString()))
                return Unauthorized("Not authorized");

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
            string role = _tokenHandlerService.ExtractUserRole();
            if (role == "Not" 
            || role.ToUpper() != Role.PASSENGER.ToString())
                return Unauthorized("Not authorized");

            IEnumerable<FavoritePointDto> favoritePointDtos = _favoritePointRepository.GetRouteFavoritePointDtos(PassengerId, id);
            if(favoritePointDtos == null || !favoritePointDtos.Any())
                return NotFound("No favorite points found for the specified PassengerId and RouteId");
            return Ok(favoritePointDtos);
        }
    }
}