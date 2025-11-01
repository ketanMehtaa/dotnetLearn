using Microsoft.AspNetCore.Identity;

namespace dotnetLearn.Models;

/// <summary>
/// Data transfer object representing a ride
/// </summary>
public class RideDto
{
    /// <summary>
    /// The unique identifier of the ride
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    public Guid Id { get; set; }
    
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

    /// <summary>
    /// The unique identifier of the driver who created the ride
    /// </summary>
    /// <example>abc123-def456-ghi789</example>
    public required string DriverId { get; set; }
}