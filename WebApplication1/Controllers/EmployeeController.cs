using HrApi.Models;
using HrApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers
{
    [Route("Employee/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee(string? firstName, string? lastName, CancellationToken cancellationToken)
        {
            return Ok(await _employeeService.ReadEmployee(firstName, lastName, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee, CancellationToken cancellationToken)
        {
            return Ok(await _employeeService.CreateEmployee(employee, cancellationToken));
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee, CancellationToken cancellationToken)
        {
          return Ok(await _employeeService.UpdateEmployee(employee, cancellationToken));
        }

        [HttpDelete]
        public async Task<ActionResult<int>> DeleteEmployee(int employeeId)
        {
            return Ok(await _employeeService.DeleteEmployee(employeeId));
        }

    }
}
