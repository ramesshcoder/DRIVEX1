using System.Data;
using Microsoft.Data.SqlClient;

namespace Drivex.Repositories.Base;

public abstract class BaseSqlRepository
{
    private readonly string _connectionString;

    protected BaseSqlRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(
                "ConnectionStrings:DefaultConnection is not configured.");
    }

    protected SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    protected async Task<List<T>> QueryStoredProcedureAsync<T>(string storedProcedureName, Func<SqlDataReader, T> map,
        Action<SqlCommand>? configure = null, CancellationToken cancellationToken = default)
    {
        var results = new List<T>();

        await using var connection = CreateConnection();
        await using var command = new SqlCommand(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        configure?.Invoke(command);

        await connection.OpenAsync(cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            results.Add(map(reader));
        }

        return results;
    }

    protected async Task<T?> QueryStoredProcedureSingleAsync<T>(string storedProcedureName, Func<SqlDataReader, T> map, Action<SqlCommand>? configure = null,
        CancellationToken cancellationToken = default) where T : class
    {
        var results = await QueryStoredProcedureAsync(storedProcedureName, map, configure, cancellationToken);

        return results.FirstOrDefault();
    }

    protected static T GetRequired<T>(SqlDataReader reader, string column)
    {
        var ordinal = reader.GetOrdinal(column);
        return reader.GetFieldValue<T>(ordinal);
    }

    protected static T? GetOptional<T>(SqlDataReader reader, string column)
    {
        var ordinal = reader.GetOrdinal(column);
        return reader.IsDBNull(ordinal) ? default : reader.GetFieldValue<T>(ordinal);
    }
}
