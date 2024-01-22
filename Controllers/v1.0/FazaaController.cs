using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [CustomAuthorize("PASSENGER")]
    public class FazaaController : BaseApiController
    {
        private readonly IFazaaRepository _fazaaRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFriendsRepository _friendsRepository;
        private readonly ITokenHandlerService _tokenHandlerService;

        public FazaaController(
            IFazaaRepository fazaaRepository,
            IPassengerRepository passengerRepository,
            IFriendsRepository friendsRepository,
            ITokenHandlerService tokenHandlerService
            )
        {
            _fazaaRepository = fazaaRepository;
            _passengerRepository = passengerRepository;
            _friendsRepository = friendsRepository;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost("storeFazaas")]
        public ActionResult CreateFazaas(FazaaCreateDto fazaaCreateDtos)
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized");
            if (fazaaCreateDtos.InDebtId == Id)
                return BadRequest("Really?! Seriously?!");
            var friends = _friendsRepository.GetFriends(Id);
            if (friends == null)
                return BadRequest("You are not friends1");
            List<FriendDto> friendss = [];
            foreach (var fri in friends)
            {
                if (fri.Passenger!.Id == Id)
                    friendss.Add(fri.Friend!);
                if (fri.Friend!.Id == Id)
                    friendss.Add(fri.Passenger!);
            }

            if (friendss.Any(f => f.Id == fazaaCreateDtos.InDebtId)) { }
            else return BadRequest("You are not friends2");
            if (fazaaCreateDtos.Amount <= 0)
                return BadRequest("Why? are you giving or taking?");
            if (_passengerRepository.GetPassengerDtoById(fazaaCreateDtos.InDebtId) == null)
                return NotFound("Passenger Not Found");
            if (_passengerRepository.GetPassengerDtoById(Id).Wallet < fazaaCreateDtos.Amount)
                return BadRequest("You are officially broke!");
            if (_fazaaRepository.GetFazaas(fazaaCreateDtos.InDebtId).Count() > 0)
                return BadRequest("You already have a Fazaa");
            try
            {
                _fazaaRepository.StoreFazaas(fazaaCreateDtos, Id);
                if (_fazaaRepository.SaveChanges())
                    return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return BadRequest("Duplicated Record");
        }
        [HttpGet("getFazaas")]
        public ActionResult<IEnumerable<FazaaDto>> getFazaas()
        {
            int Id = _tokenHandlerService.TokenHandler();
            if (Id == -1)
                return Unauthorized("Not authorized1");

            var fazaas = _fazaaRepository.GetFazaas(Id);
            if (fazaas == null)
                return NotFound("No Fazaa Found");

            return Ok(fazaas);
        }
        [HttpGet("getFazaaById/{id}")]
        public ActionResult<FazaaDto> getFazaaById(int id)
        {
            var fazaa = _fazaaRepository.GetFazaaById(id);
            if (fazaa == null)
                return NotFound("Fazaa Not Found");

            return Ok(fazaa);
        }
    }
}