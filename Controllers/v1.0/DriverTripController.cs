using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers;

[CustomAuthorize("DRIVER")]
public class DriverTripController(
    IDriverTripRepository driverTripRepository,
    IBusRepository busRepository,
    ITokenHandlerService tokenHandlerService
    ) : BaseApiController
{
    private readonly IDriverTripRepository _driverTripRepository = driverTripRepository;
    private readonly IBusRepository _busRepository = busRepository;
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;

    [HttpGet]
    public ActionResult<IEnumerable<DriverTripDto>> GetDriverTrips()
    {
        return Ok(_driverTripRepository.GetDriverTrips());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DriverTripDto>> GetDriverTripById(int id)
    {
        var driverTripDto = await _driverTripRepository.GetDriverTripById(id);

        if (driverTripDto == null)
            return NotFound("Driver Trip Not Found");

        return driverTripDto;
    }

    [HttpPost("startTrip")]
    public async Task<ActionResult<DriverTripDto>> CreateDriverTrip(DriverTripCreateDto driverTripCreateDto)
    {
        int id = _tokenHandlerService.TokenHandler();
        var driverTripDto = await _driverTripRepository.CreateDriverTrip(id);

        if (driverTripDto.Item2 == null)
            return BadRequest("Driver Trip Not Created");

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest("Bus is not active");

        if (! await _driverTripRepository.SaveChanges())
            return BadRequest("Failed to create DriverTrip");

        using var client = new HttpClient();
        string? IsGoing;
        if (driverTripCreateDto.IsGoing!.Equals(BusStatus.Going.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            IsGoing = "going";
        }
        else if (driverTripCreateDto.IsGoing.Equals(BusStatus.Returning.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            IsGoing = "returning";
        }
        else
        {
            return BadRequest("Bus is idle");
        }

        var firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";
        var path = $"Route/{driverTripDto.Item1.RouteId}/{IsGoing}/Bus/{driverTripDto.Item1.BusId}/currentLocation.json";
        var content = new StringContent("\"\"", Encoding.UTF8, "application/json");
        var response = await client.PutAsync(firebaseUrl + path, content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to update Firebase Realtime Database: {response.StatusCode}");
            return BadRequest("Failed to update Firebase Realtime Database");
        }

        return Ok(driverTripDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDriverTrip(int id, DriverTripUpdateDto driverTripUpdateDto)
    {
        if (Enum.TryParse(driverTripUpdateDto.status, out Status parsedStatus))
        {
            if (parsedStatus == Status.COMPLETED)
            {
                if (driverTripUpdateDto.Rating == 0)
                    return BadRequest("Rating is required");
            }
        }
        var driverTripDto = await _driverTripRepository.updateDriverTrip(id, driverTripUpdateDto);

        if (driverTripDto.Item2 == "Driver trip not found")
            return BadRequest($"{driverTripDto.Item2}");

        if (driverTripDto.Item2 == "Trip is Completed")
            return BadRequest($"Can not Update trip, {driverTripDto.Item2}");

        if (driverTripDto.Item2 == "Trip is Canceled")
            return BadRequest($"Can not Update trip, {driverTripDto.Item2}");

        if (driverTripDto.Item2 == "Invalid status")
            return BadRequest($"{driverTripDto.Item2} {driverTripUpdateDto.status}");

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest("Bus is not active");

        if (!await _driverTripRepository.SaveChanges())
            return BadRequest("Failed to update DriverTrip");
            
        return Ok("Trip Updated");
    }

    private async Task<bool> IsBusActive(int? busId)
    {
        return await _busRepository.IsBusActive(busId);
    }
}