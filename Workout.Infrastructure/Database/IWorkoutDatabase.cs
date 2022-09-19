using System.Data.Common;

namespace Workout.Infrastructure.Database;

/// <summary>
/// Represents the bridge to the database that service will perform operations on.
/// </summary>
public interface IWorkoutDatabase : IDisposable
{
    /// <summary>
    /// Base object for database-specific connections.
    /// </summary>
    DbConnection Connection { get; }

    /// <summary>
    /// Calls stored procedure with the given name.
    /// </summary>
    /// <param name="spName">Name of the Stored Procedure to call.</param>
    /// <param name="parameters">Parameters to pass to the Stored Procedure.</param>
    /// <returns>The first column of the first row in the result set returned by the query.</returns>
    Task<object?> CallScalarStoredProcedureAsync(string spName, IEnumerable<DbParameter> parameters);

    /// <summary>
    /// Calls stored procedure with the given name.
    /// </summary>
    /// <param name="spName">Name of the Stored Procedure to call.</param>
    /// <param name="parameters">Parameters to pass to the Stored Procedure.</param>
    /// <returns>DbDataReader object to read values from.</returns>
    Task<DbDataReader?> CallStoredProcedureAsync(string spName, IEnumerable<DbParameter> parameters);
}
