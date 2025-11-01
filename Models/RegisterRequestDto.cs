namespace dotnetLearn.Models;

/// <summary>
/// Data transfer object for user registration
/// </summary>
public class RegisterRequestDto
{
    /// <summary>
    /// The desired username for the new account
    /// </summary>
    /// <example>john_doe</example>
    public required string UserName { get; set; }
    
    /// <summary>
    /// The email address for the new account
    /// </summary>
    /// <example>john.doe@example.com</example>
    public required string Email { get; set; }
    
    /// <summary>
    /// The password for the new account (must meet complexity requirements)
    /// </summary>
    /// <example>StrongPassword123!</example>
    public required string Password { get; set; }                                                                                                           
}