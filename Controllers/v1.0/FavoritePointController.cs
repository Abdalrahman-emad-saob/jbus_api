using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("PASSENGER")]
    public class FavoritePointController : BaseApiController
    {
        private readonly IFavoritePointRepository _favoritePointRepository;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FavoritePointController(
            IFavoritePointRepository favoritePointRepository,
            ITokenHandlerService tokenHandlerService)
        {
            _favoritePointRepository = favoritePointRepository;
            _tokenHandlerService = tokenHandlerService;
        }
        [HttpGet("{id}")]
        public ActionResult<FavoritePointDto> GetFavoritePointById(int id)
        {
            return Ok(_favoritePointRepository.GetFavoritePointById(id));
        }
        [HttpGet("favoritepoints")]
        public ActionResult<IEnumerable<FavoritePointDto>> GetPassengerFavoritePoints()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            return Ok(_favoritePointRepository.GetFavoritePoints(Id));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFavoritePoint(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            if (_favoritePointRepository.DeleteFavoritePoint(id, Id))
                if (_favoritePointRepository.SaveChanges())
                    return NoContent();

            return StatusCode(500, "Server Error");
        }
        [HttpPost("addfavoritepoint")]
        public ActionResult CreateFavoritePoint(FavoritePointCreateDto favoritePointCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            try
            {
                if (_favoritePointRepository.InsertFavoritePoint(favoritePointCreateDto, Id))
                    if (_favoritePointRepository.SaveChanges())
                        return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
            }

            return BadRequest();
        }
    }
}