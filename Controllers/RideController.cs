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
    public async Task<IActionResult> GetAllRides()
    {
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Unauthorized();
        }

        var allRides = _dbcontext.Rides
            .Where(r => r.DriverId == user.Id)
            .ToList()
            .Select(rideEntity => new RideDto
            {
                Id = rideEntity.Id,
                FromLocation = rideEntity.FromLocation,
                ToLocation = rideEntity.ToLocation,
                DepartureTime = rideEntity.DepartureTime,
                DriverId = rideEntity.DriverId
            })
            .ToList();

        

        return Ok(allRides);
    }


    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetRideById(Guid id)
    {
        var ride = _dbcontext.Rides.Find(id);
        if (ride == null)
        {
            return NotFound();
        }

        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByEmailAsync(email);
        var userId = user.Id;
        if (userId == null || userId != ride.DriverId)
        {
            return Unauthorized();
        }

        return Ok(new RideDto
        {
            Id = ride.Id,
            FromLocation = ride.FromLocation,
            ToLocation = ride.ToLocation,
            DepartureTime = ride.DepartureTime,
            DriverId = ride.DriverId
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddRide(AddRideDto addRideDto)
    {       
        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Unauthorized();
        }

        var rideEntity = new Ride()
        {
            FromLocation = addRideDto.FromLocation,
            ToLocation = addRideDto.ToLocation,
            DepartureTime = addRideDto.DepartureTime,
            DriverId = user.Id
        };

        _dbcontext.Rides.Add(rideEntity);
        _dbcontext.SaveChanges();

        return Ok(new RideDto
        {
            Id = rideEntity.Id,
            FromLocation = rideEntity.FromLocation,
            ToLocation = rideEntity.ToLocation,
            DepartureTime = rideEntity.DepartureTime,
            DriverId = rideEntity.DriverId
        });
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> UpdateRide(Guid id, AddRideDto addRideDto)
    {
        var ride = _dbcontext.Rides.Find(id);
        if (ride == null) return NotFound();

        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByEmailAsync(email);
        var userId = user.Id;
        if (userId == null || userId != ride.DriverId)
        {
            return Unauthorized();
        }

        ride.FromLocation = addRideDto.FromLocation;
        ride.ToLocation = addRideDto.ToLocation;
        ride.DepartureTime = addRideDto.DepartureTime;

        _dbcontext.SaveChanges();
        return Ok(new RideDto
        {
            Id = ride.Id,
            FromLocation = ride.FromLocation,
            ToLocation = ride.ToLocation,
            DepartureTime = ride.DepartureTime,
            DriverId = ride.DriverId
        });
    }


    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteRide(Guid id)
    {
        var ride = _dbcontext.Rides.Find(id);
        if (ride is null)
        {
            return NotFound();
        }

        var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByEmailAsync(email);
        var userId = user.Id;
        if (userId == null || userId != ride.DriverId)
        {
            return Unauthorized();
        }

        _dbcontext.Rides.Remove(ride);
        _dbcontext.SaveChanges();

        return Ok();
    }
}