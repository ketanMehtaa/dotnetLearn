using dotnetLearn.Data;
using dotnetLearn.Models;
using dotnetLearn.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dotnetLearn.Controllers;

/// <summary>
/// Manages employee operations (CRUD operations for employees)
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext dbcontext;

    public EmployeesController(ApplicationDbContext dbContext)
    {
        this.dbcontext = dbContext;
    }

    /// <summary>
    /// Gets all employees in the system
    /// </summary>
    /// <returns>List of all employees</returns>
    /// <response code="200">Returns the list of all employees</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllEmployees()
    {
        var allEmployees = dbcontext.Employees.ToList();
        return Ok(allEmployees);
    }


    /// <summary>
    /// Gets a specific employee by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the employee</param>
    /// <returns>The employee details if found</returns>
    /// <response code="200">Returns the employee details</response>
    /// <response code="404">Employee not found</response>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetEmployeeById(Guid id)
    {
        var employee = dbcontext.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    /// <summary>
    /// Creates a new employee
    /// </summary>
    /// <param name="addEmployeeDto">The employee information to create</param>
    /// <returns>The created employee details</returns>
    /// <response code="200">Employee created successfully</response>
    /// <response code="400">Invalid employee data provided</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
    {
        var employeeEntity = new Employee()
        {
            Name = addEmployeeDto.Name,
            Email = addEmployeeDto.Email,
            Phone = addEmployeeDto.Phone,
            Salary = addEmployeeDto.Salary,
        };

        dbcontext.Employees.Add(employeeEntity);
        dbcontext.SaveChanges();

        return Ok(employeeEntity);
    }

    /// <summary>
    /// Updates an existing employee
    /// </summary>
    /// <param name="id">The unique identifier of the employee to update</param>
    /// <param name="addEmployeeDto">The updated employee information</param>
    /// <returns>The updated employee details</returns>
    /// <response code="200">Employee updated successfully</response>
    /// <response code="404">Employee not found</response>
    /// <response code="400">Invalid employee data provided</response>
    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateEmployee(Guid id, AddEmployeeDto addEmployeeDto)
    {
        var employee = dbcontext.Employees.Find(id);
        if (employee == null) return NotFound();
        employee.Name = addEmployeeDto.Name;
        employee.Email = addEmployeeDto.Email;
        employee.Phone = addEmployeeDto.Phone;
        employee.Salary = addEmployeeDto.Salary;
        dbcontext.SaveChanges();
        return Ok(employee);
    }

    /// <summary>
    /// Deletes an employee
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete</param>
    /// <returns>Success confirmation</returns>
    /// <response code="200">Employee deleted successfully</response>
    /// <response code="404">Employee not found</response>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteEmployee(Guid id)
    {
        var employee = dbcontext.Employees.Find(id);
        if (employee is null)
        {
            return NotFound();
        }

        dbcontext.Employees.Remove(employee);
        dbcontext.SaveChanges();
        return Ok();
    }

    /// <summary>
    /// Test endpoint to trigger the global exception handler
    /// </summary>
    /// <returns>Always throws an exception for testing purposes</returns>
    /// <response code="500">Internal server error (for testing)</response>
    [HttpGet("test-error")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult TestError()
    {
        throw new Exception("Testing global exception handler");
    }
}