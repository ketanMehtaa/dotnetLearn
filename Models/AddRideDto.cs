using Microsoft.AspNetCore.Identity;

namespace dotnetLearn.Models;

/// <summary>
/// Data transfer object for creating or updating a ride
/// </summary>
public class AddRideDto
{
    /// <summary>
    /// The starting location of the ride
    /// </summary>
    /// <example>New York, NY</example>
    public required string FromLocation { get; set; }
    
    /// <summary>
    /// The destination location of the ride
    /// </summary>
    /// <example>Boston, MA</example>
    public required string ToLocation { get; set; }
    
    /// <summary>
    /// The scheduled departure date and time
    /// </summary>
    /// <example>2025-12-01T14:30:00</example>
    public required DateTime DepartureTime { get; set; }
}