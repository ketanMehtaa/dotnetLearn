using System.Security.Claims;
using dotnetLearn.Data;
using dotnetLearn.Models;
using dotnetLearn.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetLearn.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RidesController : ControllerBase
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly UserManager<IdentityUser> _userManager;

    public RidesController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        this._dbcontext = dbContext;
        this._userManager = userManager;
    }

    [HttpGet]
    public IActionResult GetAllRides()
    {
        var allRides = _dbcontext.Rides
            .Where(r => r.DriverId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            .ToList();
        return Ok(allRides);
    }


    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetRideById(Guid id)
    {
        var ride = _dbcontext.Rides.Find(id);
        if (ride == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null || userId != ride.DriverId)
        {
            return Unauthorized();
        }

        return Ok(ride);
    }

    [HttpPost]
    public async Task<IActionResult> AddRide(AddRideDto addRideDto)
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var userId = user.Id;

        var rideEntity = new Ride()
        {
            FromLocation = addRideDto.FromLocation,
            ToLocation = addRideDto.ToLocation,
            DepartureTime = addRideDto.DepartureTime,
            DriverId = userId,
            Driver = user
        };

        _dbcontext.Rides.Add(rideEntity);
        _dbcontext.SaveChanges();

        return Ok(rideEntity);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateRide(Guid id, AddRideDto addRideDto)
    {
        var ride = _dbcontext.Rides.Find(id);
        if (ride == null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null || userId != ride.DriverId)
        {
            return Unauthorized();
        }

        ride.FromLocation = addRideDto.FromLocation;
        ride.ToLocation = addRideDto.ToLocation;
        ride.DepartureTime = addRideDto.DepartureTime;

        _dbcontext.SaveChanges();
        return Ok(ride);
    }


    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteRide(Guid id)
    {
        var ride = _dbcontext.Rides.Find(id);
        if (ride is null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null || userId != ride.DriverId)
        {
            return Unauthorized();
        }

        _dbcontext.Rides.Remove(ride);
        _dbcontext.SaveChanges();

        return Ok();
    }
}