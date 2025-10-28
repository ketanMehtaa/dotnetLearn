using dotnetLearn.Data;
using dotnetLearn.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dotnetLearn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreateUserController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CreateUserController> _logger;

    public CreateUserController(ILogger<CreateUserController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult AddNewUser([FromBody] User user)
    {
        try
        {
            // ✅ Add user entity to the database
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            _logger.LogInformation("User created successfully: {Email}", user.Email);

            return Ok(new { message = "User created successfully", user.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
}