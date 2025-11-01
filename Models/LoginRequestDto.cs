namespace dotnetLearn.Models;

/// <summary>
/// Data transfer object for user login
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// The username for authentication
    /// </summary>
    /// <example>john_doe</example>
    public required string Username { get; set; }
    
    /// <summary>
    /// The password for authentication
    /// </summary>
    /// <example>StrongPassword123!</example>
    public required string Password { get; set; }
}