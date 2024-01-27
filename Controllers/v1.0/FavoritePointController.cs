using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [CustomAuthorize("PASSENGER")]
    public class FavoritePointController(
        IFavoritePointRepository favoritePointRepository,
        ITokenHandlerService tokenHandlerService) : BaseApiController
    {
        private readonly IFavoritePointRepository _favoritePointRepository = favoritePointRepository;
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoritePointDto>> GetFavoritePointById(int id)
        {
            return Ok(await _favoritePointRepository.GetFavoritePointById(id));
        }
        [HttpGet("favoritepoints")]
        public async Task<ActionResult<IEnumerable<FavoritePointDto>>> GetPassengerFavoritePoints()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            return Ok(await _favoritePointRepository.GetFavoritePoints(Id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFavoritePoint(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            if (await _favoritePointRepository.DeleteFavoritePoint(id, Id))
                if (await _favoritePointRepository.SaveChanges())
                    return NoContent();

            return StatusCode(500, "Server Error");
        }
        [HttpPost("addfavoritepoint")]
        public async Task<ActionResult> CreateFavoritePoint(FavoritePointCreateDto favoritePointCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            try
            {
                if (await _favoritePointRepository.InsertFavoritePoint(favoritePointCreateDto, Id))
                    if (await _favoritePointRepository.SaveChanges())
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