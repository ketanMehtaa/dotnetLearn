using dotnetLearn.Data;
using dotnetLearn.Models;
using dotnetLearn.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dotnetLearn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext dbcontext;

    public EmployeesController(ApplicationDbContext dbContext)
    {
        this.dbcontext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        var allEmployees = dbcontext.Employees.ToList();
        return Ok(allEmployees);
    }


    // ---------------------------------------------------
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetEmployeeById(Guid id)
    {
        var employee = dbcontext.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
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

    [HttpPut]
    [Route("{id:guid}")]
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

    [HttpDelete]
    [Route("{id:guid}")]
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
}