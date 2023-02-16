using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P_4_BonusManagement.Data.Entity;
using P_4_BonusManagement.Models.Requests;
using P_4_BonusManagement.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace P_4_BonusManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("All-Employees")]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
               return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Giorgi, from Emoloyee Contriller, retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeEntity>> GetEmployee(int id)
        {
            //try
            //{
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null) return NotFound();

                return Ok(result);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        "Giorgi Error (Get Employee by Id) retrieving data from the database");
            //}
        }

        [HttpPost("create-employee")]
        public async Task<ActionResult<EmployeeEntity>> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                var createdEmployee = await _employeeRepository.AddEmployee(request);
                await _employeeRepository.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.EmployeeEntityId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPut("{id:int}/update")]
        // FromBody ჭირდება თუ ApiController ატრიბუტი არ აქვსო. Id შესამოწმებლად რექვესთსაც უნდა ჰქონდეს?
        public async Task<ActionResult<EmployeeEntity>> UpdateEmployee(int id, [FromBody] UpdateEmployeeRequest request)
        {
            try
            {
                // აქაც მიმართავს სერვერს და რეპოზიტორშიც შესამოწმებლად, რომელია სწორი???
                var employeeToUpdate = await _employeeRepository.GetEmployee(id);

                if (employeeToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                var result = await _employeeRepository.UpdateEmployee(id, request);
                await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<ActionResult<EmployeeEntity>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await _employeeRepository.GetEmployee(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                var result = await _employeeRepository.DeleteEmployee(id);
                await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpGet("/search={name}")]
        public async Task<ActionResult<IEnumerable<EmployeeEntity>>> Search(string name)
        {
            try
            {
                var result = await _employeeRepository.Search(name);

                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}