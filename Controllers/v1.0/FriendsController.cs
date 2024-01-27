using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [CustomAuthorize("PASSENGER")]
    public class FriendsController(
        IFriendsRepository friendsRepository,
        IPassengerRepository passengerRepository,
        ITokenHandlerService tokenHandlerService
            ) : BaseApiController
    {
        private readonly IFriendsRepository _friendsRepository = friendsRepository;
        private readonly IPassengerRepository _passengerRepository = passengerRepository;
        private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;

        [HttpPost("sendFriendRequest")]
        public async Task<ActionResult> sendFriendRequest(FriendsCreateDto friendsCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            var passenger = await _passengerRepository.GetPassengerDtoById(friendsCreateDto.FriendId);
            if (passenger == null)
                return NotFound("Friend Not Found");

            if (passenger.Id == Id)
                return BadRequest("WHY??? why sending friend reqeust to yourself? Are you this lonely?");

            bool friendReqeustExists = await _friendsRepository.FriendRequestExists(friendsCreateDto.FriendId, Id);
            if (friendReqeustExists)
                return BadRequest("Friend Request Exists");

            if (await _friendsRepository.SendFriendRequest(friendsCreateDto, Id))
            {
                if (!await _friendsRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
                return StatusCode(201);
            }

            return StatusCode(500, "Server Error2");
        }

        [HttpPut("confirmFriendRequest/{id}")]
        public async Task<ActionResult> confirmFriendRequest(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");

            if (await _friendsRepository.ConfirmFriendRequest(id, Id))
            {
                await _friendsRepository.SaveChanges();
                return NoContent();
            }
            return StatusCode(500, "Server Error");
        }

        [HttpGet("getFriendById/{id}")]
        public async Task<ActionResult<FriendDto>> getFriendById(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            var friend = await _friendsRepository.GetFriendById(id, Id);
            if (friend == null || friend.Friend == null)
                return NotFound("Friend Not Found");
            FriendDto trueFriend;

            if (friend.Passenger!.Id == Id)
                trueFriend = friend.Friend;
            else
                trueFriend = friend.Passenger;

            return Ok(trueFriend);
        }

        [HttpGet("getFriends")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> getFriends()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            var friends = await _friendsRepository.GetFriends(Id);
            List<FriendDto> friendss = [];
            foreach (var fri in friends)
            {
                if (fri!.Passenger!.Id == Id)
                    friendss.Add(fri.Friend!);
                if (fri.Friend!.Id == Id)
                    friendss.Add(fri.Passenger!);
            }
            return Ok(friendss);
        }

        [HttpGet("getFriendRequests")]
        public async Task<ActionResult<IEnumerable<FriendsDto>>> GetFriendsRequests()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            var friendsRequests = await _friendsRepository.GetFriendRequests(Id);

            return Ok(friendsRequests);
        }

        [HttpDelete("deleteFriend/{id}")]
        public async Task<ActionResult> deleteFriend(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            bool result = await _friendsRepository.DeleteFriend(id, Id);
            if (!result)
                return NotFound("Friend Not Found");
            if (!await _friendsRepository.SaveChanges())
                return StatusCode(500, "Server Error");
            return NoContent();
        }
    }
}