using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employees.Models;
using Employees.Models.DTOs;
using Employees.Data;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeesController(EmployeeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        { 
            var response = _context.Employees.Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                JobTitle = e.JobTitle,
            }).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            var someEmployee = await _context.Employees.FindAsync(id);
            if(someEmployee == null)
            {
                return NotFound();
            }

            var response = new EmployeeResponseDto();
            response.Id = someEmployee.Id;
            response.Name = someEmployee.Name;
            response.Email = someEmployee.Email;
            response.JobTitle = someEmployee.JobTitle;
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] NewEmployeeDto employee)
        {
            var employeeObj = new Employee();
            
            employeeObj.Name = employee.Name;
            employeeObj.Salary = employee.Salary;
            employeeObj.Email = employee.Email;
            employeeObj.JobTitle = employee.JobTitle;
             _context.Employees.Add(employeeObj);
            await _context.SaveChangesAsync();

            var response = new EmployeeResponseDto();
            response.Id = employeeObj.Id;
            response.Name = employeeObj.Name;
            response.JobTitle = employeeObj.JobTitle;
            response.Email= employeeObj.Email;

            return CreatedAtAction(
                nameof(GetEmployeeByID),
                new { id = response.Id }, 
                response
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody]  EmployeeUpdateDto employeeUpdate)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if(existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employeeUpdate.Name;
            existingEmployee.Email = employeeUpdate.Email;
            existingEmployee.JobTitle = employeeUpdate.JobTitle;
            existingEmployee.Salary = employeeUpdate.Salary;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var someEmployee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (someEmployee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(someEmployee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }       
}
