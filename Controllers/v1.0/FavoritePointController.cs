using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    public class FavoritePointController : BaseApiController
    {
        private readonly IFavoritePointRepository _favoritePointRepository;
        private readonly IMapper _mapper;

        public FavoritePointController(IFavoritePointRepository favoritePointRepository, IMapper mapper)
        {
            _favoritePointRepository = favoritePointRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public ActionResult<FavoritePointDto> GetFavoritePointById(int id)
        {
            return Ok(_favoritePointRepository.GetFavoritePointById(id));
        }
        [HttpGet("FavoritePoints/{id}")]
        public ActionResult<IEnumerable<FavoritePointDto>> GetPassengerFavoritePoints(int id)
        {
            return Ok(_favoritePointRepository.GetFavoritePoints(id));
        }
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<FavoritePointDto>> DeleteFavoritePoint(int id)
        {
            return Ok(_favoritePointRepository.DeleteFavoritePoint(id));
        }
        [HttpPost("AddFavoritePoint")]
        public ActionResult CreateFavoritePoint(FavoritePointCreateDto favoritePointCreateDto)
        {
            _favoritePointRepository.InsertFavoritePoint(favoritePointCreateDto);
            return Ok();
        }
    }
}