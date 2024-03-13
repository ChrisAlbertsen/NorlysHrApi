using HrApi.Models;

namespace HrApi.Services.Interfaces;

public interface IEmployeeService
{
    Task<Employee> CreateEmployee(Employee employee, CancellationToken cancellationToken);
    Task<IEnumerable<Employee>> ReadEmployee(string? firstName, string? lastName, CancellationToken cancellationToken);
    Task<Employee> UpdateEmployee(Employee employee, CancellationToken cancellationToken);
    Task<int> DeleteEmployee(int employeeId);
}
