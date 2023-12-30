using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class FriendsController : BaseApiController
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FriendsController(
            IFriendsRepository friendsRepository,
            IMapper mapper,
            ITokenHandlerService tokenHandlerService)
        {
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

        [HttpGet("getFriends")]
        public ActionResult<IEnumerable<FriendsDto>> getFriends()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var friends = _friendsRepository.GetFriends(Id);
            if (friends == null)
                return NotFound("No Friends, forever alone!");

            return Ok(friends);
        }

        [HttpGet("getFriendRequests")]
        public ActionResult<IEnumerable<FriendsDto>> GetFriendsRequests()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            var friendsRequests = _friendsRepository.GetFriendRequests(Id);
            if (friendsRequests == null)
                return NotFound("No Friends Requests, No one loves you");
            return Ok(friendsRequests);
        }

        [HttpDelete("deleteFriend")]
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
    }
}