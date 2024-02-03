using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Services;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[CustomAuthorize("PASSENGER")]
public class FazaaController(
    IFazaaRepository fazaaRepository,
    IPassengerRepository passengerRepository,
    IFriendsRepository friendsRepository,
    ITokenHandlerService tokenHandlerService,
    INotisTokenRepository notisTokenRepository,
    NotificationService notificationService,
    FirebaseService firebaseService
        ) : BaseApiController
{
    private readonly IFazaaRepository _fazaaRepository = fazaaRepository;
    private readonly IPassengerRepository _passengerRepository = passengerRepository;
    private readonly IFriendsRepository _friendsRepository = friendsRepository;
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
    private readonly INotisTokenRepository _notisTokenRepository = notisTokenRepository;
    private readonly NotificationService _notificationService = notificationService;
    private readonly FirebaseService _firebaseService = firebaseService;

    [CustomAuthorize("PASSENGER")]
    [HttpPost("requestFazaa/{amount}")]
    public async Task<ActionResult> RequestFazaa(double amount)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized" });
        var friends = await _friendsRepository.GetFriends(Id);
        if (friends == null)
            return BadRequest(new { Error = "You dont have friends" });
        List<FriendDto> friendss = [];
        foreach (var fri in friends)
        {
            if (fri!.Passenger!.Id == Id)
                friendss.Add(fri.Friend!);
            if (fri.Friend!.Id == Id)
                friendss.Add(fri.Passenger!);
        }

        // if (amount <= 0)
        //     return BadRequest(new { Error = "Why? are you giving or taking?" });
        if ((await _fazaaRepository.GetFazaas(Id)).Any())
            return BadRequest(new { Error = "You already have a Fazaa" });
        // try
        // {
        foreach (var friend in friendss)
        {
            var FcmToken = await _notisTokenRepository.GetDeviceToken(friend.Id);
            // TODO : check fcm token validity
            if (FcmToken != null)
            {
                try
                {
                    await _notificationService.SendNotificationAsync(FcmToken, new NotificationDto
                    {
                        Title = "Request Fazaa",
                        Body = "Your Friend Requested a Fazaa",
                        Type = "RequestFazaa",
                        Value = Id.ToString()
                    });
                }
                catch (Exception)
                {

                }

            }
        }
        return Ok(new { Success = "Request Sent" });
        // }
        // catch (Exception)
        // {
        //     return StatusCode(500, "Server Error");
        // }
    }
    [HttpPost("storeFazaas")]
    public async Task<ActionResult> CreateFazaa(FazaaCreateDto fazaaCreateDto)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized" });
        if (fazaaCreateDto.InDebtId == Id)
            return BadRequest(new { Error = "Really?! Seriously?!" });
        var friends = await _friendsRepository.GetFriends(Id);
        if (friends == null)
            return BadRequest(new { Error = "You are not friends1" });
        List<FriendDto> friendss = [];
        foreach (var fri in friends)
        {
            if (fri!.Passenger!.Id == Id)
                friendss.Add(fri.Friend!);
            if (fri.Friend!.Id == Id)
                friendss.Add(fri.Passenger!);
        }

        if (friendss.Any(f => f.Id == fazaaCreateDto.InDebtId)) { }
        else return BadRequest(new { Error = "You are not friends2" });
        if (fazaaCreateDto.Amount <= 0)
            return BadRequest(new { Error = "Why? are you giving or taking?" });
        if (await _passengerRepository.GetPassengerDtoById(fazaaCreateDto.InDebtId) == null)
            return NotFound(new { Error = "Passenger Not Found" });
        if ((await _passengerRepository.GetPassengerDtoById(Id))!.Wallet < fazaaCreateDto.Amount)
            return BadRequest(new { Error = "You are officially broke!" });
        if ((await _fazaaRepository.GetFazaas(fazaaCreateDto.InDebtId)).Any())
            return BadRequest(new { Error = "You already have a Fazaa" });
        try
        {
            await _fazaaRepository.StoreFazaas(fazaaCreateDto, Id);

            if (await _fazaaRepository.SaveChanges())
            {
                var FcmToken = await _notisTokenRepository.GetDeviceToken(fazaaCreateDto.InDebtId);
                // TODO : check fcm token validity
                if (FcmToken != null)
                {
                    await _notificationService.SendNotificationAsync(FcmToken, new NotificationDto
                    {
                        Title = "Fazaa Confirmed",
                        Body = "Your Friend has Fazaaed you",
                        Type = "ConfirmFazaa",
                        Value = Id.ToString()
                    });
                }
                var path = $"Faza/{fazaaCreateDto.InDebtId}.json";

                if(await _firebaseService.DeleteAsync(path))
                    return StatusCode(201);
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Server Error");
        }

        return BadRequest(new { Error = "Duplicated Record" });
    }
    [HttpGet("getFazaas")]
    public async Task<ActionResult<IEnumerable<FazaaDto>>> getFazaas()
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized(new { Error = "Not authorized1" });

        var fazaas = await _fazaaRepository.GetFazaas(Id);
        if (fazaas == null)
            return NotFound(new { Error = "No Fazaa Found" });

        return Ok(fazaas);
    }
    [HttpGet("getFazaaById/{id}")]
    public async Task<ActionResult<FazaaDto>> getFazaaById(int id)
    {
        var fazaa = await _fazaaRepository.GetFazaaById(id);
        if (fazaa == null)
            return NotFound(new { Error = "Fazaa Not Found" });

        return Ok(fazaa);
    }
}
