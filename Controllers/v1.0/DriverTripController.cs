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
        var driverTripDto = await _driverTripRepository.CreateDriverTrip(id);

        if (driverTripDto.Item2 == null)
            return BadRequest("Driver Trip Not Created");

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest("Bus is not active");

        if (!await _driverTripRepository.SaveChanges())
            return BadRequest("Failed to create DriverTrip");

        using var client = new HttpClient();

        var firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";
        var path = $"Route/{driverTripDto.Item1.RouteId}/{IsGoing}/Bus/{driverTripDto.Item1.BusId}/currentLocation.json";
        var content = new StringContent("\"\"", Encoding.UTF8, "application/json");
        var response = await client.PutAsync(firebaseUrl + path, content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to update Firebase Realtime Database: {response.StatusCode}");
            return BadRequest("Failed to update Firebase Realtime Database");
        }

        return Ok(driverTripDto.Item1);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateDriverTrip(DriverTripUpdateDto driverTripUpdateDto)
    {
        int driverId = _tokenHandlerService.TokenHandler();
        if (driverId == -1)
            return Unauthorized("Unauthorized");

        if (Enum.TryParse(driverTripUpdateDto.status, out Status parsedStatus))
        {
            if (parsedStatus == Status.COMPLETED)
            {
                if (driverTripUpdateDto.Rating == 0)
                    return BadRequest("Rating is required");
            }
        }
        var driverTripDto = await _driverTripRepository.updateDriverTrip(driverId, driverTripUpdateDto);

        if (driverTripDto.Item2 == "Driver not found")
            return BadRequest($"{driverTripDto.Item2}");

        if (driverTripDto.Item2 == "Driver has no trips")
            return BadRequest($"{driverTripDto.Item2}");

        if (driverTripDto.Item2 == "Driver has no active trip")
            return BadRequest($"{driverTripDto.Item2}");

        if (driverTripDto.Item2 == "Invalid status")
            return BadRequest($"{driverTripDto.Item2} {driverTripUpdateDto.status}");

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest("Bus is not active");

        if (!await _driverTripRepository.SaveChanges())
            return BadRequest("Failed to update DriverTrip");

        return Ok("Trip Updated");
    }

    [HttpPut("finishHim")]
    public async Task<ActionResult> finishDriverTrip(DriverTripUpdateDto driverTripUpdateDto)
    {
        int id = _tokenHandlerService.TokenHandler();
        if (id == -1)
            return Unauthorized("Unauthorized");

        if (Enum.TryParse(driverTripUpdateDto.status!.ToUpper(), out Status parsedStatus))
        {
            if (parsedStatus != Status.COMPLETED)
            {
                return BadRequest("Invalid status0");
            }
            else
            {
                if (driverTripUpdateDto.Rating <= 0)
                    return BadRequest("Rating is required");
            }
        }
        else
        {
            return BadRequest("Invalid status1");
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
            return BadRequest("Failed to finish DriverTrip");

        var bus = await _busRepository.GetBusById((int)driverTripDto.Item1.BusId!);

        string? IsGoing;
        if (bus!.Going!.Equals(BusStatus.Going.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            IsGoing = "going";
        }
        else if (bus.Going.Equals(BusStatus.Returning.ToString(), StringComparison.CurrentCultureIgnoreCase))
        {
            IsGoing = "returning";
        }
        else
        {
            return BadRequest("Bus is idle");
        }

        using var client = new HttpClient();

        var firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";
        var path = $"Route/{driverTripDto.Item1.RouteId}/{IsGoing}/Bus/{driverTripDto.Item1.BusId}.json";
        var response = await client.DeleteAsync(firebaseUrl + path);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to delete Firebase Realtime Database: {response.StatusCode}");
            return BadRequest("Failed to delete Firebase Realtime Database");
        }


        return Ok("Trip Finished");
    }

    private async Task<bool> IsBusActive(int? busId)
    {
        return await _busRepository.IsBusActive(busId);
    }
}