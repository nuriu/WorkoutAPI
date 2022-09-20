using System.Data.Common;

namespace Workout.Infrastructure.Database;

public static class DbDataReaderExtensions
{
    public static string? GetSafeString(this DbDataReader reader, int ordinal)
    {
        if (reader.GetValue(ordinal) != DBNull.Value)
            return reader.GetString(ordinal);

        return null;
    }

    public static bool? GetSafeBoolean(this DbDataReader reader, int ordinal)
    {
        if (reader.GetValue(ordinal) != DBNull.Value)
            return reader.GetBoolean(ordinal);

        return null;
    }

    public static DateTime? GetSafeDateTime(this DbDataReader reader, int ordinal)
    {
        if (reader.GetValue(ordinal) != DBNull.Value)
            return reader.GetDateTime(ordinal);

        return null;
    }

    public static T? GetSafeFieldValue<T>(this DbDataReader reader, int ordinal)
    {
        if (reader.GetValue(ordinal) != DBNull.Value)
            return reader.GetFieldValue<T>(ordinal);

        return default;
    }
}
