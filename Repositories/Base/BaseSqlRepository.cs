using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Drivex.Repositories.Base;

public abstract class BaseSqlRepository
{
    private readonly string _connectionString;

    protected BaseSqlRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string is not configured.");
    }

    protected SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}