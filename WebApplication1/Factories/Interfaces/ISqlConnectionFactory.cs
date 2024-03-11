using System.Data.SqlClient;

namespace HrApi.Factories.Interfaces;

public interface ISqlConnectionFactory
{
    Task<SqlConnection> GetDefaultConnection();

}
