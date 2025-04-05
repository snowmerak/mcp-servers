using ModelContextProtocol.Server;
using Npgsql;

namespace PostgresRawTool;

[McpServerToolType]
public class RawQueryTool
{
    private readonly NpgsqlConnection _connection;
    private readonly string _connectionString = System.Environment.GetEnvironmentVariable("PG_CONNECTION_STRING") ?? "Host=localhost:5432;Username=postgres;Password=postgres;Database=postgres";
}