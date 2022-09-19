namespace Workout.Core.Querying;

// TODO: add total item count and calculate page count from it
/// <summary>
/// Represents paginated list.
/// </summary>
/// <typeparam name="T">Type of data.</typeparam>
public interface IPagedList<out T>
{
    /// <summary>
    /// Page index.
    /// </summary>
    int PageIndex { get; }

    /// <summary>
    /// Page size.
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// Data.
    /// </summary>
    IEnumerable<T> Items { get; }
}
