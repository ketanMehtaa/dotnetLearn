using Microsoft.AspNetCore.Identity;

namespace dotnetLearn.Models;

public class AddRideDto
{
    public required string FromLocation { get; set; }
    public required string ToLocation { get; set; }
    public required DateTime DepartureTime { get; set; }
}