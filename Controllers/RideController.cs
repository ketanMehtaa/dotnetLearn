using System.Security.Claims;
using dotnetLearn.Data;
using dotnetLearn.Models;
using dotnetLearn.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetLearn.Controllers;

/// <summary>
/// Manages ride sharing operations for authenticated users
/// </summary>
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

    /// <summary>
    /// Gets all rides created by the authenticated user
    /// </summary>
    /// <returns>List of rides belonging to the current user</returns>
    /// <response code="200">Returns the list of user's rides</response>
    /// <response code="401">User is not authenticated</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


    /// <summary>
    /// Gets a specific ride by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the ride</param>
    /// <returns>The ride details if found and belongs to the current user</returns>
    /// <response code="200">Returns the ride details</response>
    /// <response code="401">User is not authenticated or ride doesn't belong to user</response>
    /// <response code="404">Ride not found</response>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Creates a new ride for the authenticated user
    /// </summary>
    /// <param name="addRideDto">The ride information to create</param>
    /// <returns>The created ride details</returns>
    /// <response code="200">Ride created successfully</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="400">Invalid ride data provided</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Updates an existing ride owned by the authenticated user
    /// </summary>
    /// <param name="id">The unique identifier of the ride to update</param>
    /// <param name="addRideDto">The updated ride information</param>
    /// <returns>The updated ride details</returns>
    /// <response code="200">Ride updated successfully</response>
    /// <response code="401">User is not authenticated or ride doesn't belong to user</response>
    /// <response code="404">Ride not found</response>
    /// <response code="400">Invalid ride data provided</response>
    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


    /// <summary>
    /// Deletes a ride owned by the authenticated user
    /// </summary>
    /// <param name="id">The unique identifier of the ride to delete</param>
    /// <returns>Success confirmation</returns>
    /// <response code="200">Ride deleted successfully</response>
    /// <response code="401">User is not authenticated or ride doesn't belong to user</response>
    /// <response code="404">Ride not found</response>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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