using HrApi.Controllers.Interfaces;
using HrApi.Models;
using HrApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers
{
    [Route("Employee/")]
    [ApiController]
    public class EmployeeController : ControllerBase, IEmployeeController
    {
        private readonly IEmployeeCrudService _employeeCrudService;

        public EmployeeController(IEmployeeCrudService employeeCrudService)
        {
            _employeeCrudService = employeeCrudService;
        }


        [HttpGet]
        [Route("Firstname")]
        public async Task<ActionResult<List<Employee>>> GetEmployee(string firstName)
        {
            return Ok(await _employeeCrudService.ReadEmployee(firstName));
        }

        [HttpGet]
        [Route("Fullname")]
        public async Task<ActionResult<List<Employee>>> GetEmployee(string firstName, string? lastName)
        {
            return Ok(await _employeeCrudService.ReadEmployee(firstName, lastName));
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            return Ok(await _employeeCrudService.CreateEmployee(employee));
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
          return Ok(await _employeeCrudService.UpdateEmployee(employee));
        }

        [HttpDelete]
        public async Task<ActionResult<int>> DeleteEmployee(int employeeId)
        {
            return Ok(await _employeeCrudService.DeleteEmployee(employeeId));
        }

    }
}
