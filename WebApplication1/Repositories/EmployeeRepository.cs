using Dapper;

using HrApi.Models;
using System.Data.SqlClient;
using HrApi.Factories.Interfaces;
using HrApi.Services.Interfaces;
using Microsoft.AspNetCore.Routing.Template;
using System.Diagnostics.Tracing;

namespace HrApi.Services

{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public EmployeeRepository(ISqlConnectionFactory connectionFactory)
        {
            _sqlConnectionFactory = connectionFactory;
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
            AddConditionIfNotEmpty("FirstName", firstName, parameters, ref query);
            AddConditionIfNotEmpty("LastName", lastName, parameters, ref query);

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

        void AddConditionIfNotEmpty(string fieldName, string? value, DynamicParameters parameters, ref string query)
        {
            if (!string.IsNullOrEmpty(value))
            {
                query += $" AND {fieldName} LIKE '%' + @{fieldName} + '%'";
                parameters.Add($"@{fieldName}", value);
            }
        }
    }
}
