using HrApi.Models;

namespace HrApi.Services.Interfaces;

public interface IEmployeeCrudService
{
    Task<Employee> CreateEmployee(Employee employee);
    Task<IEnumerable<Employee>> ReadEmployee(string firstName);
    Task<IEnumerable<Employee>> ReadEmployee(string firstName, string? lastName);
    Task<Employee> UpdateEmployee(Employee employee);
    Task<int> DeleteEmployee(int employeeId);
}
