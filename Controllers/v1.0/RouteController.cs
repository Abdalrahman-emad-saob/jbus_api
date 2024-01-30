using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    public class RouteController(
        IRouteRepository routeRepository,
        ITokenHandlerService tokenHandlerService,
        IFavoritePointRepository favoritePointRepository) : BaseApiController
    {
        private readonly IRouteRepository _routeRepository = routeRepository;
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
        private readonly IFavoritePointRepository _favoritePointRepository = favoritePointRepository;

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPost("addRoute")]
        public async Task<ActionResult> CreateRoute(RouteCreateDto routeCreateDto)
        {
            if (await _routeRepository.CreateRoute(routeCreateDto))
                if (await _routeRepository.SaveChanges())
                    return StatusCode(201);
            return StatusCode(500, new { Error = "Server Error" });
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
        [HttpGet("getRoutes")]
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutes()
        {
            return Ok(await _routeRepository.GetRoutes());
        }

        [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN", "DRIVER")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDto?>> GetRouteById(int id)
        {
            return await _routeRepository.GetRouteById(id);
        }

        [CustomAuthorize("DRIVER")]
        [HttpGet("GetMyRoute")]
        public async Task<ActionResult<RouteDto?>> GetMyRoute()
        {
            int DriverId = _tokenHandlerService.TokenHandler();
            if (DriverId == -1)
                return Unauthorized(new { Error = "Not authorized" });

            return await _routeRepository.GetDriverRoute(DriverId);
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult> updateRoute(RouteUpdateDto routeUpdateDto, int id)
        {
            var route = await _routeRepository.GetRouteById(id);

            if (route == null)
                return NotFound(new { Error = "Route Not Found" });


            if (await _routeRepository.Update(routeUpdateDto, id))
                if (await _routeRepository.SaveChanges())
                    return NoContent();

            return BadRequest(new { Error = "Failed to Update Route" });
        }

        [CustomAuthorize("SUPER_ADMIN", "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteRoute(int id)
        {
            var route = await _routeRepository.GetRouteById(id);

            if (route == null)
                return NotFound(new { Error = "Route Not Found" });


            if (await _routeRepository.Delete(id))
                if (await _routeRepository.SaveChanges())
                    return NoContent();

            return BadRequest(new { Error = "Failed to Delete Route" });
        }
        [CustomAuthorize("PASSENGER")]
        [HttpGet("{id}/favoritepoints")]
        public async Task<ActionResult<IEnumerable<FavoritePointDto?>>> GetRouteFavoritePoints(int id)
        {
            int PassengerId = _tokenHandlerService.TokenHandler();
            if (PassengerId == -1)
                return Unauthorized(new { Error = "Not authorized1" });

            IEnumerable<FavoritePointDto?> favoritePointDtos = await _favoritePointRepository.GetRouteFavoritePointDtos(PassengerId, id);

            return Ok(favoritePointDtos);
        }
    }
}