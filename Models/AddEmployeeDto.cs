namespace dotnetLearn.Models;

/// <summary>
/// Data transfer object for creating or updating an employee
/// </summary>
public class AddEmployeeDto
{
    /// <summary>
    /// The full name of the employee
    /// </summary>
    /// <example>John Doe</example>
    public required string Name { get; set; }
    
    /// <summary>
    /// The email address of the employee
    /// </summary>
    /// <example>john.doe@company.com</example>
    public required string Email { get; set; }
    
    /// <summary>
    /// The phone number of the employee (optional)
    /// </summary>
    /// <example>+1-555-0123</example>
    public string? Phone { get; set; }
    
    /// <summary>
    /// The salary of the employee
    /// </summary>
    /// <example>75000.00</example>
    public decimal Salary { get; set; }
}