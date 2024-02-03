using API.Controllers.v1;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using API.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers;

[CustomAuthorize("DRIVER")]
public class DriverTripController(
    IDriverTripRepository driverTripRepository,
    IBusRepository busRepository,
    ITokenHandlerService tokenHandlerService,
    FirebaseService firebaseService
    ) : BaseApiController
{
    private readonly IDriverTripRepository _driverTripRepository = driverTripRepository;
    private readonly IBusRepository _busRepository = busRepository;
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
    private readonly FirebaseService _firebaseService = firebaseService;

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
            return NotFound(new { Error = "Driver Trip Not Found" });

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
            return BadRequest(new { Error = "Bad Status" });
        }
        var driverTripDto = await _driverTripRepository.CreateDriverTrip(id, IsGoing);

        if (driverTripDto.Item2 == "Driver not found")
            return BadRequest(new { Error = driverTripDto.Item2 });

        if (driverTripDto.Item2 == "Driver has no bus")
            return BadRequest(new { Error = driverTripDto.Item2 });

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest(new { Error = "Bus is not active" });

        if (!await _driverTripRepository.SaveChanges())
            return BadRequest(new { Error = "Failed to create DriverTrip" });

        var path = $"Route/{driverTripDto.Item1.RouteId}/{IsGoing}/Bus/{driverTripDto.Item1.BusId}/currentLocation.json";
        var response = await _firebaseService.PutAsync(path, "\"\"");
        if (!response)
        {
            Console.WriteLine($"Failed to update Firebase Realtime Database");
            return StatusCode(500, new { Error = "Failed to update Firebase Realtime Database" });
        }
        var pathR = $"/Route/{driverTripDto.Item1.RouteId}/{IsGoing}/Bus/{driverTripDto.Item1.BusId}";
        driverTripDto.Item1.firebasePath = pathR;
        driverTripDto.Item1.IsGoing = driverTripCreateDto.IsGoing.Equals(BusStatus.Going.ToString(), StringComparison.CurrentCultureIgnoreCase);
        return Ok(driverTripDto.Item1);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateDriverTrip(DriverTripUpdateDto driverTripUpdateDto)
    {
        int driverId = _tokenHandlerService.TokenHandler();
        if (driverId == -1)
            return Unauthorized(new { Error = "Unauthorized" });

        var driverTripDto = await _driverTripRepository.updateDriverTrip(driverId, driverTripUpdateDto);

        if (driverTripDto.Item2 == "Driver not found")
            return BadRequest(new { Error = $"{driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Driver has no trips")
            return BadRequest(new { Error = $"{driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Driver has no active trip")
            return BadRequest(new { Error = $"{driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Invalid status")
            return BadRequest(new { Error = $"{driverTripDto.Item2} {driverTripUpdateDto.status}" });

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest(new { Error = "Bus is not active" });

        if (!await _driverTripRepository.SaveChanges())
            return BadRequest(new { Error = "Failed to update DriverTrip" });

        return Ok(new { Success = "Trip Updated" });
    }

    [HttpPut("finishHim")]
    public async Task<ActionResult> finishDriverTrip()
    {
        int id = _tokenHandlerService.TokenHandler();
        if (id == -1)
            return Unauthorized(new { Error = "Unauthorized" });

        var driverTripUpdateDto = new DriverTripUpdateDto
        {
            status = Status.COMPLETED.ToString(),
        };

        var driverTripDto = await _driverTripRepository.updateDriverTrip(id, driverTripUpdateDto);

        if (driverTripDto.Item2 == "Driver trip not found")
            return BadRequest(new { Error = $"{driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Driver has no active trip")
            return BadRequest(new { Error = $"{driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Trip is Completed")
            return BadRequest(new { Error = $"Can not Update trip, {driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Trip is Canceled")
            return BadRequest(new { Error = $"Can not Update trip, {driverTripDto.Item2}" });

        if (driverTripDto.Item2 == "Invalid status")
            return BadRequest(new { Error = $"{driverTripDto.Item2} {driverTripUpdateDto.status}" });

        if (!await IsBusActive(driverTripDto.Item1.BusId))
            return BadRequest(new { Error = "Bus is not active" });

        if (!await _driverTripRepository.SaveChanges())
            return BadRequest(new { Error = "Failed to finish DriverTrip" });

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
            return BadRequest(new { Error = "Bus is idle" });
        }
        await _busRepository.UpdateBusStatus((int)driverTripDto.Item1.BusId, BusStatus.Idle.ToString());
        await _busRepository.SaveChanges();

        var path = $"Route/{driverTripDto.Item1.RouteId}/{IsGoing}/Bus/{driverTripDto.Item1.BusId}.json";
        var response = await _firebaseService.DeleteAsync(path);
        if (!response)
        {
            Console.WriteLine($"Failed to delete Firebase Realtime Database");
            return StatusCode(500, new { Error = "Failed to delete Firebase Realtime Database" });
        }

        return Ok(new { Success = "Trip Finished" });
    }

    private async Task<bool> IsBusActive(int? busId)
    {
        return await _busRepository.IsBusActive(busId);
    }
}