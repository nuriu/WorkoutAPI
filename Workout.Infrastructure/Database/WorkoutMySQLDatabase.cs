using System.Data;
using System.Data.Common;
using MySqlConnector;

namespace Workout.Infrastructure.Database;

public sealed class WorkoutMySQLDatabase : IWorkoutDatabase
{
    public DbConnection Connection { get; }

    public WorkoutMySQLDatabase(string connectionString)
    {
        Connection = new MySqlConnection(connectionString);
    }

    public void Dispose() => Connection.Dispose();

    public async Task<object?> CallScalarStoredProcedureAsync(string spName, IEnumerable<DbParameter> parameters)
    {
        await Connection.OpenAsync();

        var cmd = new MySqlCommand(spName, Connection as MySqlConnection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddRange(parameters.ToArray());
        var result = await cmd.ExecuteScalarAsync();

        await Connection.CloseAsync();

        return result;
    }
}
