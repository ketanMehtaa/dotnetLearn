using dotnetLearn.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly JwtTokenService _jwtService;

    public AuthController(JwtTokenService jwtService, ApplicationDbContext dbContext)
    {
        _jwtService = jwtService;
        _dbContext = dbContext;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        // Validate credentials against database

        // Step 1. Check if user exists
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == login.Email);

        if (user == null)
            return Unauthorized("Invalid email or password");

        // Step 2. Verify password (compare hash)
        var isPasswordValid = login.Password == user.Password;

        if (!isPasswordValid)
            return Unauthorized("Invalid email or password");

        // Step 3. Generate JWT
        var token = _jwtService.GenerateToken(user.Email);

        return Ok(new { token, user.Email });
    }
}

public record LoginRequest(string Email, string Password);