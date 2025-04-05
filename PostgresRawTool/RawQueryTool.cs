using System.ComponentModel;
using ModelContextProtocol.Server;
using Npgsql;

namespace PostgresRawTool;

[McpServerToolType]
public static class RawQueryTool
{
    private static readonly string ConnectionString = System.Environment.GetEnvironmentVariable("PG_CONNECTION_STRING") ?? "Host=localhost:5432;Username=postgres;Password=postgres;Database=postgres";

    [McpServerTool, Description("Executes a raw SQL query and returns the result as a value list.")]
    public static async Task<List<object>> ExecuteRawQuery(string query)
    {
        await using var dataSource = NpgsqlDataSource.Create(ConnectionString);

        var connection = await dataSource.OpenConnectionAsync();

        await using var command = dataSource.CreateCommand(query);
        await using var reader = await command.ExecuteReaderAsync();

        var result = new List<object>();
        while (await reader.ReadAsync())
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                result.Add(reader.GetValue(i));
            }
        }
        return result;
    }
}