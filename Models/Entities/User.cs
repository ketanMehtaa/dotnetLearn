using Microsoft.EntityFrameworkCore;

namespace dotnetLearn.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Phone), IsUnique = true)]
public class User
{
    public Guid Id { get; init; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public string? DrivingLicenseNumber { get; set; }
    public DateTime? LicenseValidFrom { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalRidesAsDriver { get; set; }
    public int TotalRidesAsPassenger { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    // public ICollection<Vehicle> Vehicles { get; set; } = [];
    // public ICollection<Ride> PublishedRides { get; set; } = [];
    // public ICollection<Booking> Bookings { get; set; } = [];
    // public ICollection<Review> WrittenReviews { get; set; } = [];
    // public ICollection<Review> ReceivedReviews { get; set; } = [];
    // public UserPreference? Preferences { get; set; }
    // public ICollection<Payment> Payments { get; set; } = [];
    // public ICollection<Notification> Notifications { get; set; } = [];
}