using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class FazaaController : BaseApiController
    {
        private readonly IFazaaRepository _fazaaRepository;
        private readonly IFriendsRepository _friendsRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FazaaController(
            IFazaaRepository fazaaRepository,
            IFriendsRepository friendsRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
            _fazaaRepository = fazaaRepository;
            _friendsRepository = friendsRepository;
            _mapper = mapper;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost("sendFriendRequest")]
        public ActionResult sendFriendRequest(FriendsCreateDto friendsCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            _friendsRepository.SendFriendRequest(friendsCreateDto, Id);
            return Created();
        }

        [HttpPut("confirmFriendRequest")]
        public ActionResult confirmFriendRequest(int friendId)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            _friendsRepository.ConfirmFriendRequest(friendId, Id);
            return NoContent();
        }

        [HttpGet("getFriendById")]
        public ActionResult<FriendsDto> getFriendById(int FriendId)
        {
            var friend = _friendsRepository.GetFriendById(FriendId);
            if (friend == null)
                return NotFound("Friend Not Found");
            return Ok(friend);
        }

        [HttpGet("getFriendsById")]
        public ActionResult<IEnumerable<FriendsDto>> getFriendsById()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var friends = _friendsRepository.GetFriendsById(Id);
            if (friends == null)
                return NotFound("No Friends, forever alone!");

            return Ok(friends);
        }

        [HttpDelete("DeleteFriend")]
        public ActionResult deleteFriend(int friendId)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            bool result = _friendsRepository.DeleteFriend(friendId, Id);
            if (!result)
                return NotFound("Friend Not Found");

            return NoContent();
        }

        [HttpPost("createFazaas")]
        public ActionResult CreateFazaas(IEnumerable<FazaaCreateDto> fazaaCreateDtos)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            bool result = _fazaaRepository.CreateFazaas(fazaaCreateDtos, Id);
            if (!result)
                return StatusCode(500, "Internal Server Error");

            return NoContent();
        }

        [HttpGet("getFazaas")]
        public ActionResult<IEnumerable<FazaaDto>> getFazaas()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var fazaas = _fazaaRepository.GetFazaas(Id);
            if (fazaas == null)
                return NotFound("No Fazaa Found");

            return Ok(fazaas);
        }

        [HttpGet("getFazaaById")]
        public ActionResult<FazaaDto> getFazaaById(int Id)
        {
            var fazaa = _fazaaRepository.GetFazaaById(Id);
            if (fazaa == null)
                return NotFound("Fazaa Not Found");

            return Ok(fazaa);
        }
    }
}