namespace Workout.Core.Querying;

// TODO: add total item count and calculate page count from it
/// <summary>
/// Represents paginated list.
/// </summary>
/// <typeparam name="T">Type of data.</typeparam>
public interface IPagedList<out T>
{
    /// <summary>
    /// Index of the data page. Starts from 1.
    /// </summary>
    uint PageIndex { get; }

    /// <summary>
    /// Size of the data page.
    /// </summary>
    uint PageSize { get; }

    /// <summary>
    /// Count of total data items.
    /// </summary>
    uint TotalItemCount { get; }

    /// <summary>
    /// Count of total data pages.
    /// </summary>
    uint TotalPageCount { get; }

    /// <summary>
    /// Does previous data page exists.
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Does next data page exists.
    /// </summary>
    bool HasNextPage { get; }

    /// <summary>
    /// Data items.
    /// </summary>
    IEnumerable<T> Items { get; }
}
