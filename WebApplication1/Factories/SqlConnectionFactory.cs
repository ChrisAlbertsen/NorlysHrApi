using HrApi.Factories.Interfaces;
using System.Data.SqlClient;

namespace HrApi.Factories
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _config;

        public SqlConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public async Task<SqlConnection> GetDefaultConnection()
        {
            var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();
            return connection;
        }
    }
}
