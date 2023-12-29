using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [Authorize]
    public class FavoritePointController : BaseApiController
    {
        private readonly IFavoritePointRepository _favoritePointRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FavoritePointController(
            IFavoritePointRepository favoritePointRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
            _favoritePointRepository = favoritePointRepository;
            _mapper = mapper;
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
                return Unauthorized();
            return Ok(_favoritePointRepository.GetFavoritePoints(Id));
        }
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<FavoritePointDto>> DeleteFavoritePoint(int id)
        {
            return Ok(_favoritePointRepository.DeleteFavoritePoint(id));
        }
        [HttpPost("addfavoritepoint")]
        public ActionResult CreateFavoritePoint(FavoritePointCreateDto favoritePointCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized();
            if (_favoritePointRepository.InsertFavoritePoint(favoritePointCreateDto))
            {
                return Created();
            }
            return BadRequest();
        }
    }
}