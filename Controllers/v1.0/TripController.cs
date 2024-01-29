using System.Text;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers.v1;

[CustomAuthorize("PASSENGER")]
public class TripController(
    ITokenHandlerService tokenHandlerService,
    ITripRepository tripRepository,
    IBusRepository busRepository
        ) : BaseApiController
{
    private readonly ITokenHandlerService _tokenHandlerService = tokenHandlerService;
    private readonly ITripRepository _tripRepository = tripRepository;
    private readonly IBusRepository _busRepository = busRepository;

    [HttpGet("getTrips")]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized("Not authorized");

        return Ok(await _tripRepository.GetTrips(Id));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto?>> GetTripById(int id)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized("Not authorized");

        string role = _tokenHandlerService.ExtractUserRole();
        if (
        role == "Not" ||
        (
        role.ToUpper() != Role.PASSENGER.ToString()
        ))
            return Forbid("Not authorized");

        return await _tripRepository.GetTripById(id, Id);
    }
    [HttpPut]
    public async Task<IActionResult> updateTrip(TripUpdateDto tripUpdateDto)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized("Not authorized");

        var tripDtos = (await _tripRepository.GetTrips(Id)).ToList();

        if (tripDtos.Count == 0)
            return NotFound(new { Error = "Trips Not Found" });

        var tripDto = tripDtos.Find(t => t!.Status!.Equals(TripStatus.ONGOING.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
         t!.Status.Equals(TripStatus.PENDING.ToString(), StringComparison.CurrentCultureIgnoreCase));

        if (tripDto == null)
            return NotFound("Trip Not Found");

        if (tripDto.DriverTrip == null)
            return BadRequest("DriverTrip");
        if (tripDto.DriverTrip.BusId == null)
            return BadRequest("BusId");
        var bus = await _busRepository.GetBusById((int)tripDto.DriverTrip.BusId);
        if (bus == null)
            return BadRequest("Bus");
        if (bus.Going == null)
            return BadRequest("Going");
        using var client = new HttpClient();
        var firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";
        var DropOffPoint = tripUpdateDto.DropOffPoint;
        var PickUpPoint = tripUpdateDto.PickUpPoint;

        if (DropOffPoint != null)
        {
            using var clientD = new HttpClient();
            var path = $"Route/{bus.RouteId}/{bus.Going.ToString().ToLower()}/Bus/{bus.Id}/dropoffs/{Id}.json";
            var jsonData = JsonConvert.SerializeObject(new Dictionary<string, double>() { { "latitude", DropOffPoint.Latitude }, { "longitude", DropOffPoint.Longitude } });
            var contentD = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseD = await client.PutAsync(firebaseUrl + path, contentD);
            if (!responseD.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to update Dropoff Point Firebase Realtime Database: {responseD.StatusCode}");
                return BadRequest("Failed to update Dropoff Point Firebase Realtime Database");
            }
        }

        var pathP = $"Route/{bus.RouteId}/{bus.Going.ToString().ToLower()}/Bus/{bus.Id}/pickups/{Id}.json";

        var jsonDataP = JsonConvert.SerializeObject(new Dictionary<string, double>() { { "latitude", PickUpPoint!.Latitude }, { "longitude", PickUpPoint.Longitude } });
        var content = new StringContent(jsonDataP, Encoding.UTF8, "application/json");
        var response = await client.PutAsync(firebaseUrl + pathP, content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to update Pickup Point Firebase Realtime Database: {response.StatusCode}");
            return BadRequest("Failed to update Pickup Point Firebase Realtime Database");
        }

        await _tripRepository.Update(tripUpdateDto, tripDto.Id);

        if (await _tripRepository.SaveChanges())
            return NoContent();

        return BadRequest("No Changes Made");
    }
    [HttpPost("{id}")]
    public async Task<ActionResult> CreateTrip(int id, TripCreateDto tripCreateDto)
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized("Not authorized");

        var tripDto = await _tripRepository.CreateTrip(tripCreateDto, Id, id);
        if (tripDto == null)
            return BadRequest("Bad Request");
        var tripDtos = (await _tripRepository.GetTrips(Id)).ToList();
        var tripDtot = tripDtos.Find(t => t!.Status!.Equals(TripStatus.ONGOING.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
        t!.Status.Equals(TripStatus.PENDING.ToString(), StringComparison.CurrentCultureIgnoreCase));

        if (tripDtot != null)
            return BadRequest(new { Error = "Passenger Already Has Trip" });

        var DropOffPoint = tripDto.DropOffPoint;
        var PickUpPoint = tripDto.PickUpPoint;
        // if (tripDto.DriverTrip == null)
        //     return BadRequest("DriverTrip");

        // if (tripDto.DriverTrip.BusId == null)
        //     return BadRequest("BusId");
        var bus = await _busRepository.GetBusById(id);

        if (bus == null)
            return BadRequest("Bus");

        if (bus.Going == null)
            return BadRequest("Going");


        using var client = new HttpClient();
        var firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";

        if (DropOffPoint != null)
        {
            using var clientD = new HttpClient();
            var path = $"Route/{bus.RouteId}/{bus.Going.ToString().ToLower()}/Bus/{bus.Id}/dropoffs/{Id}.json";
            var jsonData = JsonConvert.SerializeObject(new Dictionary<string, double>() { { "latitude", DropOffPoint.Latitude }, { "longitude", DropOffPoint.Longitude } });
            var contentD = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseD = await client.PutAsync(firebaseUrl + path, contentD);
            if (!responseD.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to update Dropoff Point Firebase Realtime Database: {responseD.StatusCode}");
                return BadRequest("Failed to update Dropoff Point Firebase Realtime Database");
            }
        }

        var pathP = $"Route/{bus.RouteId}/{bus.Going.ToString().ToLower()}/Bus/{bus.Id}/pickups/{Id}.json";

        var jsonDataP = JsonConvert.SerializeObject(new Dictionary<string, double>() { { "latitude", PickUpPoint!.Latitude }, { "longitude", PickUpPoint.Longitude } });
        var content = new StringContent(jsonDataP, Encoding.UTF8, "application/json");
        var response = await client.PutAsync(firebaseUrl + pathP, content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to update Pickup Point Firebase Realtime Database: {response.StatusCode}");
            return BadRequest("Failed to update Pickup Point Firebase Realtime Database");
        }
        if (!await _tripRepository.SaveChanges())
            return StatusCode(500, "Server Error1");

        return Created("", tripDto);
    }
    [HttpPut("finishHim")]
    public async Task<IActionResult> finishTrip()
    {
        int Id = _tokenHandlerService.TokenHandler();
        if (Id == -1)
            return Unauthorized("Not authorized");

        var tripDtos = (await _tripRepository.GetTrips(Id)).ToList();

        if (tripDtos.Count == 0)
            return NotFound(new { Error = "Trips Not Found" });

        var tripDto = tripDtos.Find(t => t!.Status!.Equals(TripStatus.ONGOING.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
         t!.Status.Equals(TripStatus.PENDING.ToString(), StringComparison.CurrentCultureIgnoreCase));

        if (tripDto == null)
            return NotFound("Trip Not Found");

        if (tripDto.DriverTrip == null)
            return BadRequest("DriverTrip");
        if (tripDto.DriverTrip.BusId == null)
            return BadRequest("BusId");
        var bus = await _busRepository.GetBusById((int)tripDto.DriverTrip.BusId);
        if (bus == null)
            return BadRequest("Bus");
        if (bus.Going == null)
            return BadRequest("Going");
        using var client = new HttpClient();
        var firebaseUrl = "https://jbus-8f9bf-default-rtdb.europe-west1.firebasedatabase.app/";
        // var DropOffPoint = tripUpdateDto.DropOffPoint;
        // var PickUpPoint = tripUpdateDto.PickUpPoint;

        // if (DropOffPoint != null)
        // {
        using var clientD = new HttpClient();
        var path = $"Route/{bus.RouteId}/{bus.Going.ToString().ToLower()}/Bus/{bus.Id}/dropoffs/{Id}.json";
        // var jsonData = JsonConvert.SerializeObject(new Dictionary<string, double>() { { "latitude", DropOffPoint.Latitude }, { "longitude", DropOffPoint.Longitude } });
        // var contentD = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseD = await client.DeleteAsync(firebaseUrl + path);
        if (!responseD.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to delete Dropoff Point Firebase Realtime Database: {responseD.StatusCode}");
            return BadRequest("Failed to delete Dropoff Point Firebase Realtime Database");
        }
        // }

        var pathP = $"Route/{bus.RouteId}/{bus.Going.ToString().ToLower()}/Bus/{bus.Id}/pickups/{Id}.json";

        // var jsonDataP = JsonConvert.SerializeObject(new Dictionary<string, double>() { { "latitude", PickUpPoint!.Latitude }, { "longitude", PickUpPoint.Longitude } });
        // var content = new StringContent(jsonDataP, Encoding.UTF8, "application/json");
        var response = await client.DeleteAsync(firebaseUrl + pathP);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to delete Pickup Point Firebase Realtime Database: {response.StatusCode}");
            return BadRequest("Failed to delete Pickup Point Firebase Realtime Database");
        }

        await _tripRepository.finishTrip(tripDto.Id);

        if (await _tripRepository.SaveChanges())
            return NoContent();

        return BadRequest("No Changes Made");
    }
}

