using API.Controllers.v1;
using API.DTOs;
using API.Entities;
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
        private readonly IPassengerRepository _passengerRepository;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FriendsController(
            IFriendsRepository friendsRepository,
            IPassengerRepository passengerRepository,
            ITokenHandlerService tokenHandlerService)
        {
            _friendsRepository = friendsRepository;
            _passengerRepository = passengerRepository;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost("sendFriendRequest")]
        public ActionResult sendFriendRequest(FriendsCreateDto friendsCreateDto)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            var passenger = _passengerRepository.GetPassengerDtoById(friendsCreateDto.FriendId);
            if (passenger == null)
                return NotFound("Friend Not Found");

            if (passenger.Id == Id)
                return BadRequest("WHY??? why sending friend reqeust to yourself? Are you this lonely?");

            bool friendReqeustExists = _friendsRepository.FriendRequestExists(friendsCreateDto.FriendId, Id);
            if (friendReqeustExists)
                return BadRequest("Friend Request Exists");
            
            if (_friendsRepository.SendFriendRequest(friendsCreateDto, Id))
            {
                if(!_friendsRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
                return StatusCode(201);
            }
            
            return StatusCode(500, "Internal Server Error2");
        }

        [HttpPut("confirmFriendRequest/{id}")]
        public ActionResult confirmFriendRequest(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            if(_friendsRepository.ConfirmFriendRequest(id, Id))
            {
            _friendsRepository.SaveChanges();
            return NoContent();
            }
            return StatusCode(500, "Server Error");
        }
        [HttpGet("getFriendById/{id}")]
        public ActionResult<FriendDto> getFriendById(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            var friend = _friendsRepository.GetFriendById(id, Id);
            if (friend == null || friend.Friend == null)
                return NotFound("Friend Not Found");
            FriendDto trueFriend;

                if(friend.Passenger!.Id == Id)
                    trueFriend = friend.Friend;
                else
                    trueFriend = friend.Passenger;

            return Ok(trueFriend);
        }

        [HttpGet("getFriends")]
        public ActionResult<IEnumerable<FriendDto>> getFriends()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            var friends = _friendsRepository.GetFriends(Id);
            if (friends == null || !friends.Any())
                return NotFound("No Friends, forever alone!");
            List<FriendDto> friendss = [];
            foreach(var fri in friends)
            {
                if(fri.Passenger!.Id == Id)
                    friendss.Add(fri.Friend!);
                if(fri.Friend!.Id == Id)
                    friendss.Add(fri.Passenger!);
            }
            return Ok(friendss);
        }

        [HttpGet("getFriendRequests")]
        public ActionResult<IEnumerable<FriendsDto>> GetFriendsRequests()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            var friendsRequests = _friendsRepository.GetFriendRequests(Id);
            if (friendsRequests == null || !friendsRequests.Any())
                return NotFound("No Friends Requests, No one loves you");

            return Ok(friendsRequests);
        }

        [HttpDelete("deleteFriend/{id}")]
        public ActionResult deleteFriend(int id)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            string role = _tokenHandlerService.ExtractUserRole();
            if (
            role == "Not" ||
            (
            role.ToUpper() != Role.PASSENGER.ToString()
            ))
                return Unauthorized("Not authorized2");

            bool result = _friendsRepository.DeleteFriend(id, Id);
            if (!result)
                return NotFound("Friend Not Found");
            if(!_friendsRepository.SaveChanges())
                return StatusCode(500, "Server Error1");
            return NoContent();
        }
    }
}