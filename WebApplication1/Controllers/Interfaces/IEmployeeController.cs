using HrApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers.Interfaces
{
    public interface IEmployeeController
    {
        Task<ActionResult<List<Employee>>> GetEmployee(string firstName);
        Task<ActionResult<List<Employee>>> GetEmployee(string firstName, string? lastName);
        Task<ActionResult<Employee>> CreateEmployee(Employee employee);
        Task<ActionResult<Employee>> UpdateEmployee(Employee employee);
        Task<ActionResult<int>> DeleteEmployee(int employeeId);
    }
}
