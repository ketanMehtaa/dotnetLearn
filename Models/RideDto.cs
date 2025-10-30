using Microsoft.AspNetCore.Identity;

namespace dotnetLearn.Models;

public class RideDto
{
    public Guid Id { get; set; }
    public required string FromLocation { get; set; }
    public required string ToLocation { get; set; }
    public required DateTime DepartureTime { get; set; }

    public required string DriverId { get; set; }
}