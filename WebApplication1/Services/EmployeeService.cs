using Dapper;

using HrApi.Models;
using System.Data.SqlClient;
using HrApi.Factories.Interfaces;
using HrApi.Services.Interfaces;

namespace HrApi.Services

{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public EmployeeService(ISqlConnectionFactory connectionFactory)
        {
            _sqlConnectionFactory = connectionFactory;z
        }

        public async Task<Employee> CreateEmployee(Employee employee, CancellationToken cancellationToken = default)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();

            await connection.ExecuteAsync("INSERT INTO employeeMgmt.EMPLOYEE (FirstName, LastName, BirthDate, FkOfficeId) " +
                "VALUES (@FirstName, @LastName, @BirthDate, @FkOfficeId)", employee);

            cancellationToken.ThrowIfCancellationRequested();

            Employee createdEmployee = await connection.QueryFirstAsync<Employee>("SELECT * FROM employeeMgmt.EMPLOYEE WHERE FirstName = @FirstName AND LastName = @LastName AND BirthDate = @BirthDate", employee);
            return createdEmployee;
        }

        public async Task<IEnumerable<Employee>> ReadEmployee(string? firstName, string? lastName, CancellationToken  cancellationToken = default)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            string query = "SELECT * FROM employeeMgmt.EMPLOYEE WHERE 1 = 1";
            DynamicParameters parameters = new DynamicParameters();
            if (!string.IsNullOrEmpty(firstName))
            {
                query += " AND FirstName LIKE '%' + @FirstName + '%'";
            parameters.Add("@FirstName", firstName);
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query += " AND LastName LIKE '%' + @LastName + '%'";
                parameters.Add("@LastName", lastName);
            }

            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<Employee> employees = await connection.QueryAsync<Employee>(query, parameters);
            return employees;
        }

        public async Task<Employee> UpdateEmployee(Employee employee, CancellationToken cancellationToken = default)
        {
            using SqlConnection connection = await _sqlConnectionFactory.GetDefaultConnection();
            await connection.ExecuteAsync("UPDATE employeeMgmt.EMPLOYEE SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, FkOfficeId = @FkOfficeId WHERE EmployeeId = @EmployeeId", employee);

            cancellationToken.ThrowIfCancellationRequested();

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
