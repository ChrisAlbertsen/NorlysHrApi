using Dapper;

using HrApi.Models;
using System.Data.SqlClient;
using HrApi.Factories.Interfaces;
using HrApi.Services.Interfaces;

namespace HrApi.Services

{
    public class EmployeeCrudService : IEmployeeCrudService
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public EmployeeCrudService(ISqlConnectionFactory connectionFactory)
        {
            _sqlConnectionFactory = connectionFactory;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            await connection.ExecuteAsync("INSERT INTO employeeMgmt.EMPLOYEE (FirstName, LastName, BirthDate, FkOfficeId) " +
                "VALUES (@FirstName, @LastName, @BirthDate, @FkOfficeId)", employee);

            var createdEmployee = await connection.QueryFirstAsync<Employee>("SELECT * FROM employeeMgmt.EMPLOYEE WHERE FirstName = @FirstName AND LastName = @LastName AND BirthDate = @BirthDate", employee);
            return createdEmployee;
        }

        public async Task<IEnumerable<Employee>> ReadEmployee(string firstName)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            var employee = await connection.QueryAsync<Employee>("SELECT * FROM employeeMgmt.EMPLOYEE WHERE FirstName LIKE '%' + @FirstName + '%'",
                new { FirstName = firstName });
            return employee;
        }

        public async Task<IEnumerable<Employee>> ReadEmployee(string firstName, string? lastName)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            var employee = await connection.QueryAsync<Employee>("SELECT * FROM employeeMgmt.EMPLOYEE WHERE FirstName + ' ' + LastName LIKE '%' + @FirstName + '% %' + @LastName + '%'",
                new { FirstName = firstName, LastName = lastName });
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            await connection.ExecuteAsync("UPDATE employeeMgmt.EMPLOYEE SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, FkOfficeId = @FkOfficeId WHERE EmployeeId = @EmployeeId", employee);

            Employee updatedEmployee = await connection.QueryFirstAsync<Employee>("SELECT * FROM employeeMgmt.EMPLOYEE WHERE FirstName = @FirstName AND LastName = @LastName AND BirthDate = @BirthDate", employee);
            return updatedEmployee;
        }

        public async Task<int> DeleteEmployee(int employeeId)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            int rowsAffected = await connection.ExecuteAsync("DELETE FROM employeeMgmt.EMPLOYEE WHERE EmployeeId = @EmployeeId", new { EmployeeId = employeeId });
            return rowsAffected;
        }
    }
}
